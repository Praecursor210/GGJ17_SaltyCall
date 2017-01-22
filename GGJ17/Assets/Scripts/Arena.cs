using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
	public float _waveStrength = 2f;
	public float _pitchSpeed;

	private float _pitchX = 0f;
	private float _pitchZ = 0f;

	private float _waveIntensityX = 0f;
	private float _waveIntensityZ = 0f;

	private const float MAX_PITCH = 6f;


	void Update()
	{
		_waveIntensityX = ( Mathf.Sin( Time.time * _pitchSpeed ) + 1f ) * 0.5f;
		_waveIntensityZ = ( Mathf.Sin( Time.time * _pitchSpeed + 0.568f ) + 1f ) * 0.5f;

		//_pitchX += waveIntensityX * _pitchSpeed;
		//_pitchX = Mathf.Clamp( _pitchX, -MAX_PITCH, MAX_PITCH );

		_pitchX = Mathf.Lerp( -MAX_PITCH, MAX_PITCH, _waveIntensityX );
		_pitchZ = Mathf.Lerp( -MAX_PITCH, MAX_PITCH, _waveIntensityZ );

		//_pitchZ += waveIntensityZ * _pitchSpeed;
		//_pitchZ = Mathf.Clamp( _pitchZ, -MAX_PITCH, MAX_PITCH );

		transform.localRotation = Quaternion.Euler( _pitchX, 0f, _pitchZ );
	}
}
