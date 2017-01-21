using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
	public Transform _pivot;
	public AnimationClip _smashClip;

	[Header( "Data" )]
	[Range( 0f, 1f )]
	public float _stunAdd;

	private Animator _animator;

	private bool _useWeapon = false;

	private float _smashDuration;
	private float _smashTimer = 0f;

	private const float MIN_ANGLE = -70f;
	private const float WEAPON_SPEED = 10f;

	void Start()
	{
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
				_animator.SetBool( "smash", false );
			}
		}
	}

	public void TriggerWeapon( Animator animator )
	{
		_animator = animator;

		if( !_useWeapon )
		{
			_useWeapon = true;
			_smashTimer = 0f;
			_animator.SetBool( "smash", true );
		}
	}

	void OnTriggerStay( Collider collision )
	{
		if( _useWeapon && collision.gameObject.tag == "Player" )
		{
			Player otherPlayer = collision.gameObject.GetComponent<Player>();
			Vector3 dir = ( otherPlayer.transform.position - transform.position ).normalized;
			Debug.Log( dir );
			otherPlayer._rigidbody.AddForce( new Vector3( dir.x, 0f, dir.z ) * 2000f );
			otherPlayer.Stun( _stunAdd );
		}
	}
}
