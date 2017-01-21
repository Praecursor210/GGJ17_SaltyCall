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
		_text.text = @"Stun P1: " + GameManager.Instance._players[0]._stun + "\n" +
			"Stun P2: " + GameManager.Instance._players[1]._stun + "\n";
	}
}
