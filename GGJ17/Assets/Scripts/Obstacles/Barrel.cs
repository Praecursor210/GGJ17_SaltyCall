using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	public float _baseStun;
	public float _bumpMultiplier;

	private Rigidbody _rigidbody;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if( transform.position.y < -10f )
		{
			DestroyImmediate( gameObject );
		}
	}

	private void OnCollisionEnter( Collision collision )
	{
		if( collision.gameObject.tag == "Player" )
		{
			Player otherPlayer = collision.gameObject.GetComponent<Player>();
			Vector3 dir = ( otherPlayer.transform.position - transform.position ).normalized;
			otherPlayer._rigidbody.AddForce( new Vector3( dir.x, 0f, dir.z ) * _rigidbody.velocity.sqrMagnitude * _bumpMultiplier );
			otherPlayer.Stun( _baseStun );
		}
	}
}
