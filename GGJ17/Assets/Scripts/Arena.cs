using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
	public float _waveStrength = 2f;
	public float _pitchSpeed = 0.05f;

	private float _pitch = 0f;

	private const float MAX_PITCH = 15f;

	void Start()
	{

	}


	void Update()
	{
		float waveIntensity = Mathf.Sin( Time.time ) * _waveStrength;

		_pitch += waveIntensity * _pitchSpeed;
		_pitch = Mathf.Clamp( _pitch, -MAX_PITCH, MAX_PITCH );

		transform.localRotation = Quaternion.Euler( _pitch, 0f, 0f );
	}
}
