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
		tutorialText = new string[13];
		tutorialText[0] = "At the top right, you can see what day we are on, how many lives you have, and the button that allows you to go to the next day.";
		tutorialText[1] = "The warehouse operates on a day-by-day cycle: orders that you stage and supply that you buy all happen at the end of day. I will explain later what that means. ";
		tutorialText[2] = "Moving left, you can see how much money you have spent and earned the previous day.";
		tutorialText[3] = "At the very left of that, you can see how much of each vegetable you have, divided into Current and Tomorrow's Storage.";
		tutorialText[4] = "Notice that there is a spoilage rate and a limit - my storage racks aren't the best. You should invest in upgrading storage later down the road. ";
		tutorialText[5] = "To the left, there are orders that you need to fulfill. You can stage an order and prepare it for shipment by clicking on it. Try clicking on it now!";
		tutorialText[6] =
			"The order shows the buyer name, revenue made from completing it, how much you need of each vegetable, and when you need to fulfill the order by.";
		tutorialText[7] = "This is the buy menu for replenishing your stock. Staged orders and buying for your stock both happen at end of day, when you click on the Next Day button.";
		tutorialText[8] =
			"You can use this computer to communicate with your suppliers and buyers. Click on it to see what actions you can do. ";
		tutorialText[9] =
			"This book contains an inventory recap that shows how much of each vegetable you have gained or lost in the past day. Click on it to see.";
		tutorialText[10] = "I invested my life savings into this warehouse, so I have very high expectations. Miss 3 orders, and you're fired. I'll check up on you in 15 days, so you better impress me by then.";
		tutorialText[11] = "I will be heading out now. Press Next and then Start to get going.";
		
		
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
			case 1:
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				GameObject.Find("Money").gameObject.transform.localScale = new Vector3(1, 1, 1);
				GameObject.Find("Day").gameObject.transform.localScale = new Vector3(1, 1, 1);
				GameObject.Find("Lives").gameObject.transform.localScale = new Vector3(1, 1, 1);
				GameObject.Find("Progress Button").gameObject.transform.localScale = new Vector3(1, 1, 1);
				GameObject.Find("Progress Button").GetComponent<Button>().interactable = false;
				//GameObject.Find("DayHighlight").gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
				index++;
				break;
			case 2: // highlight recap panel
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				GameObject.Find("Receipt").gameObject.transform.localScale = new Vector3(1, 1, 1);
				//GameObject.Find("DayHighlight").gameObject.transform.localScale = new Vector3(0, 0, 0);
				//GameObject.Find("RecapHighlight").gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
				index++;
				break;
			case 3: // highlight supply
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				//GameObject.Find("RecapHighlight").gameObject.transform.localScale = new Vector3(0, 0, 0);
				//GameObject.Find("StorageHighlight").gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
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
			case 4: // highlight spoilage rate and limit
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				//GameObject.Find("StorageHighlight").gameObject.transform.localScale = new Vector3(0, 0, 0);
				//GameObject.Find("SpoilageHighlight").gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
				GameObject.Find("Upgrade Storage").gameObject.transform.localScale = new Vector3(1, 1, 1);
				index++;
				break;
			case 5: // show order
				//GameObject.Find("SpoilageHighlight").gameObject.transform.localScale = new Vector3(0, 0, 0);
				//GameObject.Find("OrderHighlight").gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				index++;
                wm.GenerateNewOrder(wm.Orders[0]);
				GameObject.Find("IncomingOrdersTitle").gameObject.transform.localScale = new Vector3(1, 1, 1);
				break;
			case 6: // highlight attributes of an order
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				index++;
				break;
            case 7: // highlight buy menu
	            //GameObject.Find("OrderHighlight").gameObject.transform.localScale = new Vector3(0, 0, 0);
	            //GameObject.Find("BuyHighlight").gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                if (wm.Orders[0].Fulfilled)
                    wm.UnstageOrder(wm.Orders[0]);
                wm.Orders[0].active = false;
	            wm.Orders[0].transform.localScale = new Vector3(0, 0, 0);
				GameObject.Find("OrderSupplyMenu").gameObject.transform.localScale = new Vector3(1, 1, 1);
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				index++;
				break;
	        case 8:
		        //GameObject.Find("BuyHighlight").gameObject.transform.localScale = new Vector3(0, 0, 0);
		        //GameObject.Find("ComputerHighlight").gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
		        GameObject.Find("Laptop").gameObject.transform.localScale = new Vector3(1, 1, 1);
                gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
		        index++;
		        break;
			case 9:
				GameObject.Find("Book").gameObject.transform.localScale = new Vector3(2 ,2, 1);
				gameObject.GetComponentInChildren<Text>().text = tutorialText[index];
				index++;
				break;
			case 10:
				//GameObject.Find("ComputerHighlight").gameObject.transform.localScale = new Vector3(0, 0, 0);
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
		gameObject.GetComponentInChildren<Text>().text = "I own a vegetable warehouse, and you will be managing it for me. Let me give you a quick rundown of how things work around here. You better pay attention!";
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
		GameObject.Find("Book").gameObject.transform.localScale = new Vector3(2, 2, 1);
        foreach (Transform child in GameObject.Find("Inventory").gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        foreach (Transform child in GameObject.Find("ProjectedInventory").gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        GameObject.FindObjectOfType<Panels>().UpdateProjected();
	}
}
