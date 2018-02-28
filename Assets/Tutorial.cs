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
		tutorialText = new string[9];
		tutorialText[0] = "At the top right, you can see what day we are on, how many lives you have, and the button that allows you to go to the next day.";
		tutorialText[1] = "To the left of that, you can see how much money you have spent and earned the previous day.";
		tutorialText[2] = "At the top left, you can see how much of each vegetable you have.";
		tutorialText[3] = "Notice that there is a spoilage rate and a limit - my storage racks aren't the best. You should invest in upgrading storage later down the road, when you start making money.";
		tutorialText[4] = "To the left, there are orders that you need to fulfill. You can stage an order and prepare it for shipment by clicking on it.";
		tutorialText[5] =
			"The order shows the buyer name, revenue made from completing it, how much you need of each vegetable, and when you need to fulfill the order by.";
		tutorialText[6] = "This is the buy menu for replenishing your stock. Staged orders and buying for your stock both happen at end of day, when you click on the Next Day button.";
		tutorialText[7] = "I invested my life savings into this warehouse,  so I have very high expectations. Miss 3 orders, and you're fired. I'll check up on you in 15 days, so you better impress me by then.";
		tutorialText[8] = "I will be heading out now. Press Next and then Start to get going.";
		
		
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
		if (index == 9)
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
