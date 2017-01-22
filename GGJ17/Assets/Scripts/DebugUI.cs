using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
	private Text _text;

	void Start()
	{
		_text = GetComponent<Text>();
	}

	void Update()
	{
		string stunData = "";
		for( int i = 0; i < GameManager.Instance._players.Count; i++ )
		{
			PlayerStatus player = GameManager.Instance._players[i];
			if( player._state == PlayerState.InGame && player._playerObject != null )
			{
				stunData += "Stun P" + ( player._id + 1 ).ToString( "0.00" ) + ": " + player._playerObject._stun + "\n";
			}
		}
		_text.text = stunData;
	}
}
