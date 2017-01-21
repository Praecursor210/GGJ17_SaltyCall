using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[Header( "Data" )]
	public bool _stun;
	[Range( 0f, 1f )]
	public float _stunAdd;

	private Rigidbody _rigidbody;

	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{

	}
}
