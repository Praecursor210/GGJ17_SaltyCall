using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public static CameraShake Instance { get; private set; }
	private Vector3 _startPos;

	public Vector3 _shake;

	void Start()
	{
		Instance = this;
	}

	public IEnumerator Shake( float duration, float magnitude )
	{
		float elapsed = 0.0f;

		Vector3 originalCamPos = transform.position;

		while( elapsed < duration )
		{

			elapsed += Time.deltaTime;

			float percentComplete = elapsed / duration;
			float damper = 1.0f - Mathf.Clamp( 4.0f * percentComplete - 3.0f, 0.0f, 1.0f );

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			transform.position = originalCamPos + new Vector3( x, y, 0f );

			yield return null;
		}

		transform.position = originalCamPos;
	}
}
