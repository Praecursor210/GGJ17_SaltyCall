using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int _nbPlayers { get; private set; }
	public List<PlayerStatus> _players = new List<PlayerStatus>();

	public const int MAX_PLAYERS = 4;

	void Awake()
	{
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
	}

	public void StartGame()
	{
		_nbPlayers = _players.Count( c => c._state == PlayerState.InGame );
	}

	public PlayerStatus LoadPlayer( int id, Player obj )
	{
		_players[id]._playerObject = obj;
		return _players[id];
	}

	public void SetNbPlayers( int players )
	{
		_nbPlayers = players;
	}

	public void Endgame()
	{
		_players.Clear();
	}
}
