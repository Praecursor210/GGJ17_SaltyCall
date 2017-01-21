using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class Player : MonoBehaviour
{
	public int _id;
	public Weapon _weapon;

	[Header( "Data" )]
	[Range( 0f, 100f)]
	public float _speed;

	[Range( 0f, 0.25f )]
	public float _stunCooldown;

	// XInput stuff
	private PlayerIndex _playerIndex;
	private GamePadState _state;
	private GamePadState _prevState;

	[HideInInspector]
	public Rigidbody _rigidbody;
	private Animator _animator;

	private bool _isDead = false;

	private float _stun = 0f;
	private bool _isStun = false;

	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();

		_playerIndex = (PlayerIndex)_id;

		if( _weapon != null )
		{
			Physics.IgnoreCollision( GetComponent<Collider>(), _weapon.GetComponent<Collider>(), true );
		}

		StartCoroutine( StunCooldown() );
	}

	void Update()
	{
		if( transform.position.y <= -2f && !_isDead )
		{
			_isDead = true;
			Debug.Log( "Player " + _id + " is dead" );
		}
	}

	private IEnumerator StunCooldown()
	{
		while( !_isDead )
		{
			Stun( -_stunCooldown );
			yield return new WaitForSeconds( 1f );
		}
	}

	private void FixedUpdate()
	{
		GamePadState state = GamePad.GetState( _playerIndex );

		if( !state.IsConnected )
		{
			if( _rigidbody.velocity.y > 0 )
			{
				_rigidbody.velocity = new Vector3( _rigidbody.velocity.x, 0f, _rigidbody.velocity.z );
			}
			return;
		}

		_prevState = _state;
		_state = state;

		Vector3 move = new Vector3( Joystick.GetAxis( XInputKey.LStickX, _state ), 0f, -Joystick.GetAxis( XInputKey.LStickY, _state ) );
		_rigidbody.AddForce( move * _speed );

		if( _animator != null )
		{
			_animator.SetBool( "run", ( move.x != 0f || move.y != 0f ) );
		}

		if( _rigidbody.velocity.y > 0 )
		{
			_rigidbody.velocity = new Vector3( _rigidbody.velocity.x, 0f, _rigidbody.velocity.z );
		}

		float rightX = - Joystick.GetAxis( XInputKey.RStickX, _state );
		float rightY = - Joystick.GetAxis( XInputKey.RStickY, _state );

		if( rightX != 0f || rightY != 0f )
		{
			float rotate = Mathf.Atan2( rightY, rightX ) - Mathf.PI * 0.5f;
			_rigidbody.rotation = Quaternion.Euler( 0f, Mathf.Rad2Deg * rotate, 0f );
		}

		if( _weapon != null && Joystick.GetButtonDown( XInputKey.RT, _state, _prevState ) )
		{
			_weapon.TriggerWeapon( _animator );
		}
	}

	public void Stun( float stun )
	{
		_stun += stun;
		_stun = Mathf.Clamp01( _stun );
		
		if( _stun == 1f && !_isStun )
		{
			_isStun = true;
		}
		else if( _stun == 0f && _isStun )
		{
			_isStun = false;
		}
	}
}
