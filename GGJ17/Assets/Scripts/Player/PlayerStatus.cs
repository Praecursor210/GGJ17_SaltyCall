using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public enum PlayerState
{
	None,
	PadConnected,
	InGame,
	PadDisconnected
}

public class PlayerStatus
{
	public int _id;

	public Player _playerObject;

	public PlayerState _state = PlayerState.None;

	// XInput stuff
	public PlayerIndex _playerIndex;
	public GamePadState _padState;
	public GamePadState _padPrevState;

	public PlayerStatus( int id )
	{
		_id = id;
		_playerIndex = (PlayerIndex)_id;

		Update();
	}

	public void Update()
	{
		GamePadState state = GamePad.GetState( _playerIndex );

		if( _state == PlayerState.None )
		{
			if( !state.IsConnected )
			{
				return;
			}
			_state = PlayerState.PadConnected;
		}
		else if( _state == PlayerState.InGame && !state.IsConnected )
		{
			// PAD FAILURE
		}

		if( !state.IsConnected )
		{
			return;
		}

		_padPrevState = _padState;
		_padState = state;
	}
}
