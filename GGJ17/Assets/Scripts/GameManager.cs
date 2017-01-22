using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int _nbPlayers { get; private set; }
	public List<PlayerStatus> _players = new List<PlayerStatus>();

	private bool _finished = false;

	public const int MAX_PLAYERS = 4;

	void Awake()
	{
		if( Instance != null )
		{
			Debug.Log( "DELETE" );
			DestroyImmediate( gameObject );
			return;
		}

		Instance = this;
		DontDestroyOnLoad( gameObject );
		for( int i = 0; i < MAX_PLAYERS; i++ )
		{
			_players.Add( new PlayerStatus( i ) );
		}
	}

	private void Update()
	{
		for( int i = 0; i < _players.Count; i++ )
		{
			_players[i].Update();
		}

		if( SceneManager.GetActiveScene().name != "Main" || _finished )
		{
			return;
		}

		PlayerStatus[] alive = _players.Where( c => c._state == PlayerState.InGame && c._playerObject != null && !c._playerObject._isDead ).ToArray();

		if( alive.Length == 1 )
		{
			WinText.Instance.DisplayWinner( alive[0]._id + 1 );
			_finished = true;
			EndGame();
		}
	}

	public void StartGame()
	{
		_nbPlayers = _players.Count( c => c._state == PlayerState.InGame );
		_finished = false;
	}

	public PlayerStatus LoadPlayer( int id, Player obj )
	{
		if( id >= _nbPlayers )
		{
			return null;
		}

		_players[id]._playerObject = obj;
		return _players[id];
	}

	public void SetNbPlayers( int players )
	{
		_nbPlayers = players;
	}

	public void EndGame()
	{
		for( int i = 0; i < _players.Count; i++ )
		{
			_players[i]._state = PlayerState.None;
			_players[i]._playerObject = null;
		}
		StartCoroutine( BackMenu() );
	}

	IEnumerator BackMenu()
	{
		yield return new WaitForSeconds( 4f );
		SceneManager.LoadScene( "MainMenu" );
	}
}
