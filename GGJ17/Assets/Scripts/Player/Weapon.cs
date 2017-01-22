using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
	public AnimationClip _smashClip;

	[Header( "Data" )]
	[Range( 0f, 2f )]
	public float _stunAdd;

	[Range( 0f, 1f )]
	public float _powerLoadSpeed;

	[Range( 0f, 1f )]
	public float _minPower;

	private Animator _animator;

	private bool _useWeapon = false;

	private float _smashDuration;
	private float _smashTimer = 0f;

	private float _smashPower = 0f;

	private const float MIN_ANGLE = -70f;
	private const float WEAPON_SPEED = 10f;

	private List<GameObject> _hits = new List<GameObject>();

	void Start()
	{
		_animator = GetComponentInParent<Animator>();
		Physics.IgnoreCollision( GetComponent<Collider>(), GetComponent<Collider>(), true );
		if( _smashClip != null )
		{
			_smashDuration = _smashClip.length;
		}
	}

	void Update()
	{
		if( _useWeapon )
		{
			_smashTimer += Time.deltaTime;
			if( _smashTimer >= _smashDuration )
			{
				_useWeapon = false;
				_smashPower = 0f;
				_animator.SetBool( "smash", false );
				_hits.Clear();
			}
		}
	}

	public void LoadWeapon()
	{
		_smashPower += Time.deltaTime * _powerLoadSpeed;
		_smashPower = Mathf.Clamp01( _smashPower );

		if( !_animator.GetBool( "prepareSmash" ) )
		{
			_animator.SetBool( "prepareSmash", true );
		}
	}


	public void TriggerWeapon()
	{
		if( !_useWeapon )
		{
			_smashPower = Mathf.Clamp( _smashPower, _minPower, 1f );
			_useWeapon = true;
			_smashTimer = 0f;
			_animator.SetBool( "prepareSmash", false );
			_animator.SetBool( "smash", true );
		}
	}

	void OnTriggerStay( Collider collision )
	{
		if( !_useWeapon || _hits.Contains( collision.gameObject ) )
		{
			return;
		}
		_hits.Add( collision.gameObject );

		if( collision.gameObject.tag == "Player" )
		{
			Player otherPlayer = collision.gameObject.GetComponent<Player>();
			Vector3 dir = transform.forward; //( otherPlayer.transform.position - transform.position ).normalized;
			//otherPlayer._rigidbody.velocity = new Vector3( dir.x, 0f, dir.z ) * 2500f;
			otherPlayer._rigidbody.AddForce( new Vector3( dir.x, 0f, dir.z ) * 2000f * _smashPower );
			otherPlayer.Stun( _stunAdd * _smashPower );
		}
		else if( collision.gameObject.tag == "Barrel" )
		{
			Vector3 dir = ( collision.transform.position - transform.position ).normalized;
			collision.GetComponent<Rigidbody>().velocity = new Vector3( dir.x, 0f, dir.z ) * 50f * _smashPower;
			//collision.GetComponent<Rigidbody>().AddForce( new Vector3( dir.x, 0f, dir.z ) * 4000f );
		}
	}
}
