using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public enum WeaponState
{
	NoWeapon,
	Inactive,
	Slam,
	Cooldown,
}

public class Player : MonoBehaviour
{
	public int _id;
	public GameObject _weapon;
	public Transform _weaponPivot;

	[Header( "Data" )]
	public float _speed;

	// XInput stuff
	private PlayerIndex _playerIndex;
	private GamePadState _state;
	private GamePadState _prevState;

	private Rigidbody _rigidbody;

	private bool _isDead = false;

	private float _weaponAngle = 0f;
	private WeaponState _weaponState = WeaponState.Inactive;

	private const float MIN_ANGLE = -70f;
	private const float WEAPON_SPEED = 10f;

	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_playerIndex = (PlayerIndex)_id;

		if( _weapon != null )
		{
			Physics.IgnoreCollision( GetComponent<Collider>(), _weapon.GetComponent<Collider>(), true );
		}
	}

	void Update()
	{
		if( transform.position.y <= -2f && !_isDead )
		{
			_isDead = true;
			Debug.Log( "Player " + _id + " is dead" );
		}
	}

	private void FixedUpdate()
	{
		GamePadState state = GamePad.GetState( _playerIndex );

		if( !state.IsConnected )
		{
			return;
		}

		_prevState = _state;
		_state = state;

		Vector3 move = new Vector3( Joystick.GetAxis( XInputKey.LStickX, _state ), 0f, -Joystick.GetAxis( XInputKey.LStickY, _state ) );
		_rigidbody.AddForce( move * _speed );
		//_rigidbody.position += move * 0.5f;
		//_rigidbody.velocity = move;

		if( _rigidbody.velocity.y > 0 )
		{
			_rigidbody.velocity = new Vector3( _rigidbody.velocity.x, 0f, _rigidbody.velocity.z );
		}

		float rightX = - Joystick.GetAxis( XInputKey.RStickX, _state );
		float rightY = - Joystick.GetAxis( XInputKey.RStickY, _state );

		if( rightX != 0f || rightY != 0f )
		{
			float rotate = Mathf.Atan2( rightY, rightX );
			_rigidbody.rotation = Quaternion.Euler( 0f, Mathf.Rad2Deg * rotate, 0f );
		}

		if( _weaponState == WeaponState.Inactive && Joystick.GetButtonDown( XInputKey.RT, _state, _prevState ) )
		{
			_weaponState = WeaponState.Slam;
		}

		if( _weaponState == WeaponState.Cooldown )
		{
			_weaponAngle += WEAPON_SPEED;
			_weaponAngle = Mathf.Clamp( _weaponAngle, MIN_ANGLE, 0f );
			_weaponPivot.localRotation = Quaternion.Euler( 0f, _weaponAngle, 0f );

			if( _weaponAngle == 0f )
			{
				_weaponState = WeaponState.Inactive;
			}
		}
		else if( _weaponState == WeaponState.Slam )
		{
			_weaponAngle -= WEAPON_SPEED;
			_weaponAngle = Mathf.Clamp( _weaponAngle, MIN_ANGLE, 0f );
			_weaponPivot.localRotation = Quaternion.Euler( 0f, _weaponAngle, 0f );

			if( _weaponAngle == MIN_ANGLE )
			{
				_weaponState = WeaponState.Cooldown;
			}
		}
	}

	/*void OnCollisionEnter( Collision collision )
	{
		Debug.Log( "Collider " + collision.gameObject.name );
		if( collision.gameObject.tag == "Weapon" )
		{
			Debug.Log( "Collide" );
			Vector3 tDir = ( collision.gameObject.GetComponent<Player>()._weapon.transform.position - transform.position ).normalized;
			_rigidbody.AddForce( new Vector3( tDir.x, 0f, tDir.z ) * 100f );
		}
	}*/
}
