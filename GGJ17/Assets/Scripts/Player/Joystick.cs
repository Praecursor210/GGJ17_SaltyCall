using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public enum XInputKey
{
    A,
    B,
    X,
    Y,
    Start,
    Back,
    LB,
    RB,
    LT,
    RT,
    LThumb,
    RThumb,
    DPUp,
    DPDown,
    DPRight,
    DPLeft,
    LStickX,
    LStickY,
    RStickX,
    RStickY
}


public class Joystick
{
    public static float GetAxis( XInputKey key, GamePadState state )
    {
        switch( key )
        {
            case XInputKey.LStickX:
                return state.ThumbSticks.Left.X;
            case XInputKey.LStickY:
                return - state.ThumbSticks.Left.Y;
            case XInputKey.RStickX:
                return state.ThumbSticks.Right.X;
            case XInputKey.RStickY:
                return - state.ThumbSticks.Right.Y;
            case XInputKey.LT:
                return state.Triggers.Left;
            case XInputKey.RT:
                return state.Triggers.Right;
        }
        return 0f;
    }

    public static bool GetButtonDown( XInputKey key, GamePadState state, GamePadState prevState )
    {
        switch( key )
        {
            case XInputKey.A:
                return ( prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed );
            case XInputKey.B:
                return ( prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed );
            case XInputKey.X:
                return ( prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed );
            case XInputKey.Y:
                return ( prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed );
            case XInputKey.Start:
                return ( prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed );
            case XInputKey.Back:
                return ( prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed );
            case XInputKey.LB:
                return ( prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed );
            case XInputKey.RB:
                return ( prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed );
            case XInputKey.LThumb:
                return ( prevState.Buttons.LeftStick == ButtonState.Released && state.Buttons.LeftStick == ButtonState.Pressed );
            case XInputKey.RThumb:
                return ( prevState.Buttons.RightStick == ButtonState.Released && state.Buttons.RightStick == ButtonState.Pressed );
            case XInputKey.DPUp:
                return ( prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed );
            case XInputKey.DPDown:
                return ( prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed );
            case XInputKey.DPLeft:
                return ( prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed );
            case XInputKey.DPRight:
                return ( prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed );
            case XInputKey.LT:
                return ( prevState.Triggers.Left < 0.05f && state.Triggers.Left > 0.05f );
            case XInputKey.RT:
                return ( prevState.Triggers.Right < 0.05f && state.Triggers.Right > 0.05f );
        }
        return false;
    }

    public static bool GetButton( XInputKey key, GamePadState state )
    {
        switch( key )
        {
            case XInputKey.A:
                return ( state.Buttons.A == ButtonState.Pressed );
            case XInputKey.B:
                return ( state.Buttons.B == ButtonState.Pressed );
            case XInputKey.X:
                return ( state.Buttons.X == ButtonState.Pressed );
            case XInputKey.Y:
                return ( state.Buttons.Y == ButtonState.Pressed );
            case XInputKey.Start:
                return ( state.Buttons.Start == ButtonState.Pressed );
            case XInputKey.Back:
                return ( state.Buttons.Back == ButtonState.Pressed );
            case XInputKey.LB:
                return ( state.Buttons.LeftShoulder == ButtonState.Pressed );
            case XInputKey.RB:
                return ( state.Buttons.RightShoulder == ButtonState.Pressed );
            case XInputKey.LThumb:
                return ( state.Buttons.LeftStick == ButtonState.Pressed );
            case XInputKey.RThumb:
                return ( state.Buttons.RightStick == ButtonState.Pressed );
            case XInputKey.DPUp:
                return ( state.DPad.Up == ButtonState.Pressed );
            case XInputKey.DPDown:
                return ( state.DPad.Down == ButtonState.Pressed );
            case XInputKey.DPLeft:
                return ( state.DPad.Left == ButtonState.Pressed );
            case XInputKey.DPRight:
                return ( state.DPad.Right == ButtonState.Pressed );
            case XInputKey.LT:
                return ( state.Triggers.Left > 0.05f );
            case XInputKey.RT:
                return ( state.Triggers.Right > 0.05f );
        }
        return false;
    }

    public static bool GetButtonUp( XInputKey key, GamePadState state, GamePadState prevState )
    {
        switch( key )
        {
            case XInputKey.A:
                return ( prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released );
            case XInputKey.B:
                return ( prevState.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released );
            case XInputKey.X:
                return ( prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released );
            case XInputKey.Y:
                return ( prevState.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released );
            case XInputKey.Start:
                return ( prevState.Buttons.Start == ButtonState.Pressed && state.Buttons.Start == ButtonState.Released );
            case XInputKey.Back:
                return ( prevState.Buttons.Back == ButtonState.Pressed && state.Buttons.Back == ButtonState.Released );
            case XInputKey.LB:
                return ( prevState.Buttons.LeftShoulder == ButtonState.Pressed && state.Buttons.LeftShoulder == ButtonState.Released );
            case XInputKey.RB:
                return ( prevState.Buttons.RightShoulder == ButtonState.Pressed && state.Buttons.RightShoulder == ButtonState.Released );
            case XInputKey.LThumb:
                return ( prevState.Buttons.LeftStick == ButtonState.Pressed && state.Buttons.LeftStick == ButtonState.Released );
            case XInputKey.RThumb:
                return ( prevState.Buttons.RightStick == ButtonState.Pressed && state.Buttons.RightStick == ButtonState.Released );
            case XInputKey.DPUp:
                return ( prevState.DPad.Up == ButtonState.Pressed && state.DPad.Up == ButtonState.Released );
            case XInputKey.DPDown:
                return ( prevState.DPad.Down == ButtonState.Pressed && state.DPad.Down == ButtonState.Released );
            case XInputKey.DPLeft:
                return ( prevState.DPad.Left == ButtonState.Pressed && state.DPad.Left == ButtonState.Released );
            case XInputKey.DPRight:
                return ( prevState.DPad.Right == ButtonState.Pressed && state.DPad.Right == ButtonState.Released );
            case XInputKey.LT:
                return ( prevState.Triggers.Left > 0.05f && state.Triggers.Left < 0.05f );
            case XInputKey.RT:
                return ( prevState.Triggers.Right > 0.05f && state.Triggers.Right < 0.05f );
        }
        return false;
    }
}
