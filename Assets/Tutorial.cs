using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

	private Button _yesButton;
	private Button _noButton;
	private Button _nextButton;
    private WarehouseManager wm;

	private GameObject bossman;

	private int index;

	private string[] tutorialText;

	// Use this for initialization
	void Start ()
	{
		index = 0;
		tutorialText = new string[10];
		tutorialText[0] = "At the top right, you can see what day we are on, how many lives you have, and the button that allows you to go to the next day.";
		tutorialText[1] = "To the left of that, you can see how much money you have spent and earned the previous day.";
		tutorialText[2] = "At the top left, you can see how much of each vegetable you have.";
		tutorialText[3] = "Notice that there is a spoilage rate and a limit - my storage racks aren't the best. You should invest in upgrading storage later down the road, when you start making money.";
		tutorialText[4] = "To the left, there are orders that you need to fulfill. You can stage an order and prepare it for shipment by clicking on it.";
		tutorialText[5] =
			"The order shows the buyer name, revenue made from completing it, how much you need of each vegetable, and when you need to fulfill the order by.";
		tutorialText[6] = "This is the buy menu for replenishing your stock. Staged orders and buying for your stock both happen at end of day, when you click on the Next Day button.";
		tutorialText[7] =
			"You can use this computer to communicate with your suppliers and buyers. Click on it to see what actions you can do.";
		tutorialText[8] = "I invested my life savings into this warehouse,  so I have very high expectations. Miss 3 orders, and you're fired. I'll check up on you in 15 days, so you better impress me by then.";
		tutorialText[9] = "I will be heading out now. Press Next and then Start to get going.";
		
		
		_yesButton = GameObject.Find("YesButton").GetComponent<Button>();
		_noButton = GameObject.Find("NoButton").GetComponent<Button>();
		_nextButton = GameObject.Find("NextButton").GetComponent<Button>();

		_nextButton.gameObject.transform.localScale = new Vector3(0, 0, 0);
		
		_yesButton.onClick.AddListener(Stage1);
		_noButton.onClick.AddListener(PlayGame);
        _nextButton.onClick.AddListener(nextStage);
        wm = GameObject.FindObjectOfType<WarehouseManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void nextStage()
	{
		switch (index)
		{
			case 0: // highlight top right
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				GameObject.Find("Money").gameObject.transform.localScale = new Vector3(1, 1, 1);
				GameObject.Find("Day").gameObject.transform.localScale = new Vector3(1, 1, 1);
				GameObject.Find("Lives").gameObject.transform.localScale = new Vector3(1, 1, 1);
				GameObject.Find("Progress Button").gameObject.transform.localScale = new Vector3(1, 1, 1);
				GameObject.Find("Progress Button").GetComponent<Button>().interactable = false; 
				index++;
				break;
			case 1: // highlight recap panel
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				GameObject.Find("Receipt").gameObject.transform.localScale = new Vector3(1, 1, 1);
				index++;
				break;
			case 2: // highlight supply
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				//GameObject.Find("Inventory").gameObject.transform.localScale = new Vector3(1, 1, 2);
				foreach (Transform child in GameObject.Find("Inventory").gameObject.transform)
				{
					//child.localScale = new Vector3(1, 1, 1);
					child.gameObject.SetActive(true);
                }
                foreach (Transform child in GameObject.Find("ProjectedInventory").gameObject.transform)
                {
                    //child.localScale = new Vector3(1, 1, 1);
                    child.gameObject.SetActive(true);
                }
                GameObject.FindObjectOfType<Panels>().UpdateProjected();
				index++;
				break;
			case 3: // highlight spoilage rate and limit
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				GameObject.Find("Upgrade Storage").gameObject.transform.localScale = new Vector3(1, 1, 1);
				index++;
				break;
			case 4: // show order
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				index++;
                wm.GenerateNewOrder(wm.Orders[0]);
				GameObject.Find("IncomingOrdersTitle").gameObject.transform.localScale = new Vector3(1, 1, 1);
				break;
			case 5: // highlight attributes of an order
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				index++;
				break;
            case 6: // highlight buy menu
                if (wm.Orders[0].Fulfilled)
                    wm.UnstageOrder(wm.Orders[0]);
                wm.Orders[0].active = false;
	            wm.Orders[0].transform.localScale = new Vector3(0, 0, 0);
				GameObject.Find("OrderSupplyMenu").gameObject.transform.localScale = new Vector3(1, 1, 1);
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				index++;
				break;
	        case 7:
		        GameObject.Find("Laptop").gameObject.transform.localScale = new Vector3(1,1,1);
                gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
                GameObject.FindObjectOfType<Supply>().spoilRate = .2;
                GameObject.FindObjectOfType<Panels>().UpdateSpoilRate();
		        index++;
		        break;
			case 10:
				PlayGame();
				break;
			default: // otherwise just show tutorial text
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				index++;
				break;
		}

		
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
		GameObject.Find("Progress Button").GetComponent<Button>().interactable = true;
		gameObject.transform.localScale = new Vector3(0, 0, 0);
		bossman = GameObject.Find("Boss");
        bossman.gameObject.transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Money").gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Day").gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Lives").gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Progress Button").gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Receipt").gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Upgrade Storage").gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("IncomingOrdersTitle").gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("OrderSupplyMenu").gameObject.transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Laptop").gameObject.transform.localScale = new Vector3(1, 1, 1);
        foreach (Transform child in GameObject.Find("Inventory").gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        foreach (Transform child in GameObject.Find("ProjectedInventory").gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        GameObject.FindObjectOfType<Supply>().spoilRate = .2;
        GameObject.FindObjectOfType<Panels>().UpdateSpoilRate();
        GameObject.FindObjectOfType<Panels>().UpdateProjected();
	}
}
