using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
	public RectTransform _cursor;

	private int _selector;

	private void Update()
	{
		if( Joystick.GetButtonDown( XInputKey.A, GameManager.Instance._players[0]._padState, GameManager.Instance._players[0]._padPrevState ) )
		{
			switch( _selector )
			{
				case 0:
					Play();
					return;

				case 1:
					Credits();
					return;

				case 2:
					Quit();
					return;
			}
		}

		if( Joystick.GetButtonDown( XInputKey.DPDown, GameManager.Instance._players[0]._padState, GameManager.Instance._players[0]._padPrevState ) )
		{
			_selector++;
			if( _selector == 3 )
			{
				_selector = 0;
			}
			Select( _selector );
		}
		else if( Joystick.GetButtonDown( XInputKey.DPUp, GameManager.Instance._players[0]._padState, GameManager.Instance._players[0]._padPrevState ) )
		{
			_selector--;
			if( _selector == -1 )
			{
				_selector = 2;
			}
			Select( _selector );
		}
	}

	public void Play()
	{
		SceneManager.LoadScene( "PlayerSelection" );
	}

	public void Credits()
	{
		SceneManager.LoadScene( "Credits" );
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Select( int id )
	{
		_selector = id;

		_cursor.SetParent( transform.GetChild( _selector ), false );
		_cursor.localPosition = new Vector3( -225f, 0f, 0f );
	}
}
