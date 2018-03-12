using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class StorageObject : MonoBehaviour {
	
	// super unsure where we should handle the inventory recap menu logic. sticking it in here for now

	private GameObject _inventoryReceiptObject;
	private Button _exitButton;
	
	// trying to get the children of each title
	//private GameObject _inventoryTitles;
	//private Transform _inventoryTitleTransforms;

	// used in UpdateInventoryReceipt
	private string _veggieTypeComp;

	
	

	// Use this for initialization
	void Start ()
	{
		gameObject.GetComponent<MeshRenderer>().material.color = Color.grey;
		foreach (Transform child in gameObject.transform)
		{
			child.gameObject.GetComponent<MeshRenderer>().material.color = Color.grey;
		}
		_inventoryReceiptObject = GameObject.Find("InventoryReceipt");
		//_inventoryTitles = GameObject.Find("Labels");
		//_inventoryTitleTransforms= _inventoryTitles.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		Debug.Log("we good");
        _inventoryReceiptObject.gameObject.transform.localScale = new Vector3(1, 1, 1);
	}

	void ExitMenu()
    {
        _inventoryReceiptObject.gameObject.transform.localScale = new Vector3(0, 0, 0);
	}
	
	
	// handles the net totals of the inventory receipt
	public void UpdateInventoryReceiptNet(string veg, string val)
	{
		
		// can be made more readable at a later date

		var cornTotal = GameObject.Find("CornTotal");
		var squashTotal = GameObject.Find("SquashTotal");
		var beetsTotal = GameObject.Find("BeetsTotal");
		
		if (veg == "Corn")
		{
			cornTotal.GetComponent<Text>().text = val;
		}
		
		else if (veg == "Squash")
		{
			squashTotal.GetComponent<Text>().text = val;
		}
		
		else if (veg == "Beets")
		{
			beetsTotal.GetComponent<Text>().text = val;

		}
		else
		{
			Debug.Log("You are trying to update an unsupported veggie.");
		}
		
/*		int counter;
		int totalVeg;

		int totalShipped = 0;
		int totalBought = 0;
		int totalSpoiled = 0;
		int totalGained = 0;
		
		foreach (Transform child in _inventoryTitleTransforms)
		{
			counter = 0;
			totalVeg = 0;
			
			
			if (child.name.Equals("Titles"))
			{
				continue;
			}
			foreach (Transform entry in child)
			{
				_veggieTypeComp = entry.name.Substring(0, 4);
				
				if (_veggieTypeComp.Equals("Corn"))
				{
					if (counter == 0)
					{
						entry.GetComponent<Text>().text = oSup["Corn"].ToString();
						totalVeg -= oSup["Corn"];
						totalShipped += oSup["Corn"];
					}
					if (counter == 1)
					{
						entry.GetComponent<Text>().text = oBuy["Corn"].ToString();
						totalVeg += oBuy["Corn"];
						totalBought += oBuy["Corn"];
					}
					if (counter == 3)
					{
						entry.GetComponent<Text>().text = totalVeg.ToString();
						totalGained += totalVeg;
					}
					counter++;
				}
				else if (_veggieTypeComp.Equals("Squa"))
				{
					if (counter == 0)
					{
						entry.GetComponent<Text>().text = oSup["Squash"].ToString();
						totalVeg -= oSup["Squash"];
						totalShipped += oSup["Squash"];
					}
					if (counter == 1)
					{
						entry.GetComponent<Text>().text = oBuy["Squash"].ToString();
						totalVeg += oBuy["Squash"];
						totalBought += oBuy["Squash"];
					}
					if (counter == 3)
					{
						entry.GetComponent<Text>().text = totalVeg.ToString();
						totalGained += totalVeg;
					}
					counter++;
				}
				else if (_veggieTypeComp.Equals("Beet"))
				{
					if (counter == 0)
					{
						entry.GetComponent<Text>().text = oSup["Beets"].ToString();
						totalVeg -= oSup["Beets"];
						totalShipped += oSup["Beets"];
					}
					if (counter == 1)
					{
						entry.GetComponent<Text>().text = oBuy["Beets"].ToString();
						totalVeg += oBuy["Beets"];
						totalBought += oBuy["Beets"];
					}
					if (counter == 3)
					{
						entry.GetComponent<Text>().text = totalVeg.ToString();
						totalGained += totalVeg;
					}
					counter++;
				}
				else
				{
					if (counter == 0)
					{
						entry.GetComponent<Text>().text = totalShipped.ToString();
					}
					if (counter == 1)
					{
						entry.GetComponent<Text>().text = totalBought.ToString();
					}
					if (counter == 3)
					{
						entry.GetComponent<Text>().text = totalGained.ToString();
					}
					counter++;
				}
			}
		}*/
	}
}
