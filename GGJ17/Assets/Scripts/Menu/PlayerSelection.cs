using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelection : MonoBehaviour
{
	public List<GameObject> _playerPanel = new List<GameObject>();


	void Start()
	{
#if UNITY_EDITOR
		SetIngame( 1 );
		GameManager.Instance._players[1]._state = PlayerState.InGame;
		_playerPanel[1].GetComponentInChildren<Text>().text = "Ready!";
#endif
	}

	void Update()
	{
		for( int i = 0; i < _playerPanel.Count; i++ )
		{
			PlayerStatus status = GameManager.Instance._players[i];
			if( status._state == PlayerState.PadConnected && Joystick.GetButtonDown( XInputKey.A, status._padState, status._padPrevState ) )
			{
				status._state = PlayerState.InGame;
				SetIngame( i );
			}
			else if( status._state == PlayerState.PadConnected )
			{
				SetConnected( i );
			}
			else if( status._state == PlayerState.None )
			{
				SetDisconnected( i );
			}
		}

		if( Joystick.GetButtonDown( XInputKey.Start, GameManager.Instance._players[0]._padState, GameManager.Instance._players[0]._padPrevState ) )
		{
			SceneManager.LoadScene( "Main" );
			GameManager.Instance.StartGame();
		}
	}

	void SetConnected( int id )
	{
		_playerPanel[id].transform.GetChild( 1 ).GetComponent<Text>().text = "Press A to join!";
		_playerPanel[id].GetComponent<Image>().color = new Color( 150f / 255f, 150f / 255f, 150f / 255f, 100f / 255f );
		_playerPanel[id].transform.GetChild( 0 ).GetComponent<Image>().color = new Color( 1f, 1f, 1f, 150f / 255f );
	}

	void SetDisconnected( int id )
	{
		_playerPanel[id].transform.GetChild( 1 ).GetComponent<Text>().text = "Pad not connected!";
		_playerPanel[id].GetComponent<Image>().color = new Color( 100f / 255f, 100f / 255f, 100f / 255f, 100f / 255f );
		_playerPanel[id].transform.GetChild( 0 ).GetComponent<Image>().color = new Color( 1f, 1f, 1f, 50f / 255f );
	}

	void SetIngame( int id )
	{
		_playerPanel[id].transform.GetChild( 1 ).GetComponent<Text>().text = "Press A to join!";
		_playerPanel[id].GetComponent<Image>().color = new Color( 200f / 255f, 200f / 255f, 200f / 255f, 100f / 255f );
		_playerPanel[id].transform.GetChild( 0 ).GetComponent<Image>().color = new Color( 1f, 1f, 1f, 1f );
	}
}
