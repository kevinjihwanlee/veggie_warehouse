using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{

	private Button _startGame;
	
	// Use this for initialization
	void Start ()
	{
		_startGame = GameObject.Find("StartGame").GetComponent<Button>();
		_startGame.onClick.AddListener(GoToGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}

	void GoToGame()
	{
		SceneManager.LoadScene("warehouse_screen");
	}
}
