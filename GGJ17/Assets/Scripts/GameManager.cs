using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	private static readonly GameManager _instance = new GameManager();
	public static GameManager Instance { get { return _instance; } }

	public int _nbPlayers { get; private set; }
	public List<Player> _players = new List<Player>();

	private GameManager()
	{
		_nbPlayers = 2;
	}

	public void RegisterPlayer( Player player )
	{
		if( !_players.Contains( player ) )
		{
			_players.Add( player );
		}
	}

	public void Endgame()
	{
		_players.Clear();
	}
}
