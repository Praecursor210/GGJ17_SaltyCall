using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelection : MonoBehaviour
{
	public GameObject _selectorPrefab;

	private List<GameObject> _playerPanel = new List<GameObject>();

	void Start()
	{
		for( int i = 0; i < GameManager.MAX_PLAYERS; i++ )
		{
			GameObject obj = Instantiate( _selectorPrefab, transform, false );
			_playerPanel.Add( obj );
		}
	}

	void Update()
	{
		for( int i = 0; i < _playerPanel.Count; i++ )
		{
			PlayerStatus status = GameManager.Instance._players[i];
			if( status._state == PlayerState.PadConnected && Joystick.GetButtonDown( XInputKey.A, status._padState, status._padPrevState ) )
			{
				status._state = PlayerState.InGame;
				_playerPanel[i].GetComponentInChildren<Text>().text = "Hello player " + ( i + 1 );
			}
			else if( status._state == PlayerState.PadConnected )
			{
				_playerPanel[i].GetComponentInChildren<Text>().text = "Pad connected! \n Press A to join.";
			}
			else if( status._state == PlayerState.None )
			{
				_playerPanel[i].GetComponentInChildren<Text>().text = "No pad";
			}
		}

		if( Joystick.GetButtonDown( XInputKey.Start, GameManager.Instance._players[0]._padState, GameManager.Instance._players[0]._padPrevState ) )
		{
			SceneManager.LoadScene( "Main" );
			GameManager.Instance.StartGame();
		}
	}
}
