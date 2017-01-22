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

	private float _cooldownTimer = 0f;
	private bool _cooldown = false;

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
			_smashDuration = _smashClip.length * 0.95f;
		}
	}

	void Update()
	{
		if( _cooldown )
		{
			_cooldownTimer += Time.deltaTime;
			if( _cooldownTimer >= 0.5f )
			{
				_cooldown = false;
			}
		}
		else if( _useWeapon )
		{
			_smashTimer += Time.deltaTime;
			if( _smashTimer >= _smashDuration )
			{
				_useWeapon = false;
				_smashPower = 0f;
				_cooldown = true;
				_cooldownTimer = 0f;
				_animator.SetBool( "smash", false );
				_hits.Clear();
			}
		}
	}

	public void LoadWeapon()
	{
		if( _cooldown )
		{
			return;
		}

		_smashPower += Time.deltaTime * _powerLoadSpeed;
		_smashPower = Mathf.Clamp01( _smashPower );

		if( !_animator.GetBool( "prepareSmash" ) )
		{
			_animator.SetBool( "prepareSmash", true );
		}
	}


	public void TriggerWeapon()
	{
		if( _cooldown )
		{
			return;
		}

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
			//otherPlayer._rigidbody.velocity = new Vector3( dir.x, 0f, dir.z ) * 2200f * ( _smashPower * 0.5f + 0.5f );
			otherPlayer._rigidbody.AddForce( new Vector3( dir.x, 0f, dir.z ) * 2000f * ( _smashPower * 0.5f + 0.5f ) );
			Debug.Log( _smashPower );
			otherPlayer.Stun( _stunAdd * _smashPower );
			otherPlayer._particlePaf.Play();
			otherPlayer.StartCoroutine( otherPlayer.ControllerVibration( 0.2f, 0.4f ) );
			otherPlayer.SmashSound();
			StartCoroutine( CameraShake.Instance.Shake( _smashPower * 0.5f + 0.2f, _smashPower * 0.5f + 0.75f ) );
		}
		else if( collision.gameObject.tag == "Barrel" )
		{
			Vector3 dir = ( collision.transform.position - transform.position ).normalized;
			collision.GetComponent<Rigidbody>().velocity = new Vector3( dir.x, 0f, dir.z ) * 50f * _smashPower;
			StartCoroutine( CameraShake.Instance.Shake( _smashPower * 0.5f, _smashPower * 0.5f + 0.5f ) );
			//collision.GetComponent<Rigidbody>().AddForce( new Vector3( dir.x, 0f, dir.z ) * 4000f );
		}
	}
}
