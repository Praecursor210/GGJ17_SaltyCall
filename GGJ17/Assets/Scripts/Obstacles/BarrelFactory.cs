using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelFactory : MonoBehaviour
{
	public GameObject _barrelPrefab;

	[Header( "Data" )]
	public float _cooldownAverage;
	public float _cooldownRandom;
	public float _initialSpeed;

	private List<Transform> _spawners = new List<Transform>();

	private float _spawnCooldown = 0f;
	private float _spawnTimer = 0f;

	void Start()
	{
		for( int i = 0; i < transform.childCount; i++ )
		{
			_spawners.Add( transform.GetChild( i ) );
		}
		SetCooldown();
	}

	void Update()
	{
		_spawnTimer += Time.deltaTime;
		if( _spawnTimer >= _spawnCooldown )
		{
			LaunchBarrel();
			SetCooldown();
		}
	}

	void SetCooldown()
	{
		_spawnTimer = 0f;
		_spawnCooldown = _cooldownAverage + Random.Range( -_cooldownRandom, _cooldownRandom );
	}

	void LaunchBarrel()
	{
		int spawner = Random.Range( 0, _spawners.Count - 1 );
		GameObject barrelObj = Instantiate( _barrelPrefab, _spawners[spawner] );
		barrelObj.transform.position = _spawners[spawner].position;
		barrelObj.GetComponent<Rigidbody>().velocity = _spawners[spawner].right * _initialSpeed;

		Debug.Log( _spawners[spawner].right );

		//Barrel barrelSc = barrelObj.GetComponent<Barrel>();
		//barrelSc.SetImpulse( _spawners[spawner].right * 1000f );

	}
}
