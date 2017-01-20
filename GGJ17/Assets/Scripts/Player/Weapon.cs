using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponState
{
	Inactive,
	Slam,
	Cooldown,
}

public class Weapon : MonoBehaviour
{
	public Transform _pivot;

	[Header( "Data" )]
	[Range( 0f, 1f )]
	public float _stunAdd;

	private float _weaponAngle = 0f;
	private WeaponState _weaponState = WeaponState.Inactive;

	private const float MIN_ANGLE = -70f;
	private const float WEAPON_SPEED = 10f;


	void FixedUpdate()
	{
		if( _weaponState == WeaponState.Cooldown )
		{
			_weaponAngle += WEAPON_SPEED;
			_weaponAngle = Mathf.Clamp( _weaponAngle, MIN_ANGLE, 0f );
			_pivot.localRotation = Quaternion.Euler( 0f, _weaponAngle, 0f );

			if( _weaponAngle == 0f )
			{
				_weaponState = WeaponState.Inactive;
			}
		}
		else if( _weaponState == WeaponState.Slam )
		{
			_weaponAngle -= WEAPON_SPEED;
			_weaponAngle = Mathf.Clamp( _weaponAngle, MIN_ANGLE, 0f );
			_pivot.localRotation = Quaternion.Euler( 0f, _weaponAngle, 0f );

			if( _weaponAngle == MIN_ANGLE )
			{
				_weaponState = WeaponState.Cooldown;
			}
		}
	}

	public void TriggerWeapon()
	{
		if( _weaponState == WeaponState.Inactive )
		{
			_weaponState = WeaponState.Slam;
		}
	}

	void OnTriggerEnter( Collider collision )
	{
		if( collision.gameObject.tag == "Player" )
		{
			Player otherPlayer = collision.gameObject.GetComponent<Player>();
			Vector3 dir = ( otherPlayer.transform.position - transform.position ).normalized;
			otherPlayer._rigidbody.AddForce( new Vector3( dir.x, 0f, dir.z ) * 500f );
			otherPlayer.Stun( _stunAdd );
		}
	}
}
