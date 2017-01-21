using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
	public void Play()
	{
		SceneManager.LoadScene( "PlayerSelection" );
	}
}
