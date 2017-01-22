using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
	void Update()
	{
		if( Input.GetKeyDown( KeyCode.Escape ) || Joystick.GetButtonDown( XInputKey.B, GameManager.Instance._players[0]._padState, GameManager.Instance._players[0]._padPrevState ) )
		{
			SceneManager.LoadScene( "MainMenu" );
		}
	}
}
