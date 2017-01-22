using UnityEngine;
using System.Collections.Generic;

public class Water : MonoBehaviour 
{
	public List<Vector2> _uvAnimationRate = new List<Vector2>();
	public string _textureName = "_MainTex";

	private List<Vector2> _uvOffset = new List<Vector2>();

	private Renderer _renderer;

	void Start()
	{
		_renderer = GetComponent<Renderer>();

		for( int i = 0; i < _uvAnimationRate.Count; i++ )
		{
			_uvOffset.Add( Vector2.zero );
		}
	}

	void LateUpdate() 
	{
		for( int i = 0; i < _uvAnimationRate.Count; i++ )
		{
			//_uvOffset[i] += ( _uvAnimationRate[i] * Time.deltaTime );
			_uvOffset[i] = new Vector2( _uvOffset[i].x + ( _uvAnimationRate[i].x * Time.deltaTime ), Mathf.Sin( Time.time * _uvAnimationRate[i].y ) * 0.5f );
			if( _renderer.enabled )
			{
				_renderer.materials[i].SetTextureOffset( _textureName, _uvOffset[i] );
			}
		}
	}
}