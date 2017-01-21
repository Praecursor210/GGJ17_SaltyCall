using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
	private void Update()
	{
		if( Joystick.GetButtonDown( XInputKey.A, GameManager.Instance._players[0]._padState, GameManager.Instance._players[0]._padPrevState ) )
		{
			Play();
		}
	}

	public void Play()
	{
		SceneManager.LoadScene( "PlayerSelection" );
	}
}
