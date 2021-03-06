﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WinText : MonoBehaviour
{
	public static WinText Instance { get; private set; }

	void Start()
	{
		Instance = this;
		GetComponent<Text>().text = "";
	}

	public void DisplayWinner( int winner )
	{
		GetComponent<Text>().text = "Player " + winner + " win! ";
		transform.GetChild( winner - 1 ).gameObject.SetActive( true );
	}
}
