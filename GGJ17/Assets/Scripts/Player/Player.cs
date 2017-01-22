using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class Player : MonoBehaviour
{
	public int _id;

	[Header( "VFX" )]
	public ParticleSystem _particleSmoke;
	public ParticleSystem _particleSplash;
	public ParticleSystem _particlePaf;
	public ParticleSystem _particleStun;

	[Header( "Data" )]
	[Range( 0f, 100f)]
	public float _speed;

	[Range( 0f, 0.75f )]
	public float _stunCooldown;

	[Range( 0f, 10f )]
	public float _stunSpeedMultipier;

	[Range( 0f, 1f )]
	public float _stunFrictionDecrease;

	[Range( 0f, 100f )]
	public float _maxSpeed;

	[HideInInspector]
	public Rigidbody _rigidbody;
	private Weapon _weapon;
	private Animator _animator;

	private PlayerStatus _status;

	public bool _isDead { get; private set; }

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
		_weapon = GetComponentInChildren<Weapon>();
		_status = GameManager.Instance.LoadPlayer( _id, this );
		if( _status == null )
		{
			gameObject.SetActive( false );
			return;
		}

		_isDead = false;
		_particleSmoke.Stop();
		_particleSplash.Stop();
		_particlePaf.Stop();
		_particleStun.Stop();

		_rigidbody = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();

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
			Debug.Log( "Player " + _status._id + " is dead" );
		}

		if( transform.position.y <= -5f && !_particleSplash.isPlaying )
		{
			_particleSplash.Play();
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
		if( _status == null || _isStun )
		{
			return;
		}

		if( !_status._padState.IsConnected )
		{
			if( _rigidbody.velocity.y > 0 )
			{
				_rigidbody.velocity = new Vector3( _rigidbody.velocity.x, 0f, _rigidbody.velocity.z );
			}
			return;
		}

		Vector3 move = new Vector3( Joystick.GetAxis( XInputKey.LStickX, _status._padState ), 0f, -Joystick.GetAxis( XInputKey.LStickY, _status._padPrevState ) ) * Time.fixedDeltaTime * 50f;

		float stunMultiplier = 1f + _stun * _stunSpeedMultipier;
		_rigidbody.AddForce( move * ( _speed * stunMultiplier ) );
		_rigidbody.velocity = Vector3.ClampMagnitude( _rigidbody.velocity, _maxSpeed * stunMultiplier );

		bool run = ( move.x != 0f || move.z != 0f );
		if( _animator != null && _animator.enabled )
		{
			_animator.SetBool( "run", run );
		}

		if( run && !_particleSmoke.isPlaying )
		{
			_particleSmoke.Play();
		}
		else if( !run && _particleSmoke.isPlaying )
		{
			_particleSmoke.Stop();
		}

		/*if( _rigidbody.velocity.y > 0 )
		{
			_rigidbody.velocity = new Vector3( _rigidbody.velocity.x, 0f, _rigidbody.velocity.z );
		}*/

		if( move.x != 0f || move.z != 0f )
		{
			float rotate = Mathf.Atan2( move.z, -move.x ) - Mathf.PI * 0.5f;
			_rigidbody.rotation = Quaternion.Euler( 0f, Mathf.Rad2Deg * rotate, 0f );
		}

		if( _weapon != null && Joystick.GetButtonUp( XInputKey.RT, _status._padState, _status._padPrevState ) )
		{
			_weapon.TriggerWeapon();
		}
		else if( _weapon != null && Joystick.GetButton( XInputKey.RT, _status._padState ) )
		{
			_weapon.LoadWeapon();
		}
	}

	public void Stun( float stun )
	{
		_stun += stun;
		_stun = Mathf.Clamp01( _stun );
		
		if( _stun == 1f && !_isStun )
		{
			_isStun = true;
			if( _animator != null && _animator.enabled )
			{
				_animator.SetBool( "stun", true );
				_particleStun.Play();
			}
		}
		else if( _stun == 0f && _isStun )
		{
			_isStun = false;
			if( _animator != null && _animator.enabled )
			{
				_animator.SetBool( "stun", false );
				_particleStun.Stop();
			}
		}

		float fStunDecrease = 1f - ( _stun * _stunFrictionDecrease );
		_collider.material.staticFriction = _baseStaticFriction * fStunDecrease;
		_collider.material.dynamicFriction = _baseDynamicFriction * fStunDecrease;
	}
}
