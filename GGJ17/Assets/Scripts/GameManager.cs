using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public GameManager Instance { get; private set; }

	private int _nbPlayers = 2;

	//private List<Player> _players;

	void Awake()
	{
		Instance = this;
	}
}
