using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

	private Button _yesButton;
	private Button _noButton;
	private Button _nextButton;

	private GameObject bossman;

	private int index;

	private string[] tutorialText;

	// Use this for initialization
	void Start ()
	{
		index = 0;
		tutorialText = new string[7];
		tutorialText[0] = "Look at the top.";
		tutorialText[1] = "Look at the top left.";
		tutorialText[2] = "Look at the left.";
		tutorialText[3] = "Look at the buy menu.";
		tutorialText[4] = "Click things for menus.";
		tutorialText[5] = "Click next and then start once you're ready!";
		tutorialText[6] = "Win conditions.";
		
		
		_yesButton = GameObject.Find("YesButton").GetComponent<Button>();
		_noButton = GameObject.Find("NoButton").GetComponent<Button>();
		_nextButton = GameObject.Find("NextButton").GetComponent<Button>();

		_nextButton.gameObject.transform.localScale = new Vector3(0, 0, 0);

		_yesButton.onClick.AddListener(Stage1);
		_noButton.onClick.AddListener(PlayGame);
		_nextButton.onClick.AddListener(nextStage);
		
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void nextStage()
	{
		if (index == 7)
		{
			
			PlayGame();
		}
		gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
		Debug.Log(index);
		index++;
		
	}

	void Stage1()
	{
		gameObject.GetComponentInChildren<Text>().text = "I own a vegetable warehouse, and you will be managing it for me. ";
		_yesButton.gameObject.transform.localScale = new Vector3(0, 0, 0);
		_noButton.gameObject.transform.localScale = new Vector3(0, 0, 0);
		_nextButton.gameObject.transform.localScale = new Vector3(1, 1, 1);

	}

	void PlayGame()
	{
		gameObject.transform.localScale = new Vector3(0, 0, 0);
		bossman = GameObject.Find("Boss");
		bossman.gameObject.transform.localScale = new Vector3(0, 0, 0);
	}
}
