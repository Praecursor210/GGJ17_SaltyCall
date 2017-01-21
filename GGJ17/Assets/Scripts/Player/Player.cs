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

	[Range( 0f, 1f )]
	public float _stunSpeedDecrease;

	[Range( 0f, 1f )]
	public float _stunFrictionDecrease;

	// XInput stuff
	private PlayerIndex _playerIndex;
	private GamePadState _state;
	private GamePadState _prevState;

	[HideInInspector]
	public Rigidbody _rigidbody;
	private Animator _animator;

	private bool _isDead = false;

	public float _stun { get; private set; }
	private bool _isStun = false;
	private bool _stopStunCd = false;

	// Physics
	private Collider _collider;
	//private float _baseBounce;
	private float _baseStaticFriction;
	private float _baseDynamicFriction;

	void Start()
	{
		GameManager.Instance.RegisterPlayer( this );

		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();

		_playerIndex = (PlayerIndex)_id;

		if( _weapon != null )
		{
			Physics.IgnoreCollision( GetComponent<Collider>(), _weapon.GetComponent<Collider>(), true );
		}

		_collider = GetComponent<Collider>();
		_baseStaticFriction = _collider.material.staticFriction;
		_baseDynamicFriction = _collider.material.dynamicFriction;

		_stun = 0f;
		StartCoroutine( StunCooldown() );

	}

	void Update()
	{
		if( transform.position.y <= -10f && !_isDead )
		{
			_isDead = true;
			Debug.Log( "Player " + _id + " is dead" );
		}

		if( Input.GetKeyDown( KeyCode.C ) )
		{
			_stopStunCd = !_stopStunCd;
		}

		if( Input.GetKeyDown( KeyCode.S ) )
		{
			Stun( 0.1f );
		}
	}

	private IEnumerator StunCooldown()
	{
		while( !_isDead )
		{
			if( !_stopStunCd )
			{
				Stun( -_stunCooldown );
			}
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
		_rigidbody.AddForce( move * ( _speed * ( 1f - ( _stun * _stunSpeedDecrease ) ) ) );

		if( _animator != null )
		{
			_animator.SetBool( "run", ( move.x != 0f || move.y != 0f ) );
		}

		if( _rigidbody.velocity.y > 0 )
		{
			_rigidbody.velocity = new Vector3( _rigidbody.velocity.x, 0f, _rigidbody.velocity.z );
		}

		if( move.x != 0f || move.z != 0f )
		{
			float rotate = Mathf.Atan2( move.z, -move.x ) - Mathf.PI * 0.5f;
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
			if( _animator != null )
			{
				_animator.SetBool( "stun", true );
			}
		}
		else if( _stun == 0f && _isStun )
		{
			_isStun = false;
			if( _animator != null )
			{
				_animator.SetBool( "stun", false );
			}
		}

		float fStunDecrease = 1f - ( _stun * _stunFrictionDecrease );
		_collider.material.staticFriction = _baseStaticFriction * fStunDecrease;
		_collider.material.dynamicFriction = _baseDynamicFriction * fStunDecrease;
	}
}
