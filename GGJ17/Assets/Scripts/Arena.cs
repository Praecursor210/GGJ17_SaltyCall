using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
	public float _waveStrength = 2f;
	public float _pitchSpeed;

	private float _pitchX = 0f;
	private float _pitchZ = 0f;

	private const float MAX_PITCH = 15f;

	void Start()
	{

	}


	void Update()
	{
		float waveIntensityX = Mathf.Sin( Time.time ) * _waveStrength;
		float waveIntensityZ = Mathf.Sin( Time.time + 0.568f ) * _waveStrength;

		_pitchX += waveIntensityX * _pitchSpeed;
		_pitchX = Mathf.Clamp( _pitchX, -MAX_PITCH, MAX_PITCH );

		_pitchZ += waveIntensityZ * _pitchSpeed;
		_pitchZ = Mathf.Clamp( _pitchZ, -MAX_PITCH, MAX_PITCH );

		transform.localRotation = Quaternion.Euler( _pitchX, 0f, _pitchZ );
	}
}
