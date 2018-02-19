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
	private GameObject _inventoryTitles;
	private Transform _inventoryTitleTransforms;

	// used in UpdateInventoryReceipt
	private string _veggieTypeComp;

	
	

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<MeshRenderer>().material.color = new Color32(47,50,159,255);
		_inventoryReceiptObject = GameObject.Find("InventoryReceipt");
		_exitButton = GameObject.Find("ExitInvRecButton").GetComponent<Button>();
		_exitButton.onClick.AddListener(ExitMenu);

		_inventoryTitles = GameObject.Find("Labels");
		_inventoryTitleTransforms= _inventoryTitles.gameObject.transform;
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		_inventoryReceiptObject.gameObject.SetActive(true);
	}

	void ExitMenu()
	{
		_inventoryReceiptObject.gameObject.SetActive(false);
	}

	public void UpdateInventoryReceipt(Order oSup, Dictionary<string, int> oBuy)
	{
		
		// can be made more readable at a later date
		
		int counter;
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
					if (counter == 0 && oSup != null)
					{
						entry.GetComponent<Text>().text = oSup.order["Corn"].ToString();
						totalVeg -= oSup.order["Corn"];
						totalShipped += oSup.order["Corn"];
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
					if (counter == 0 && oSup != null)
					{
						entry.GetComponent<Text>().text = oSup.order["Squash"].ToString();
						totalVeg -= oSup.order["Squash"];
						totalShipped += oSup.order["Squash"];
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
					if (counter == 0 && oSup != null)
					{
						entry.GetComponent<Text>().text = oSup.order["Beets"].ToString();
						totalVeg -= oSup.order["Beets"];
						totalShipped += oSup.order["Beets"];
					}
					if (counter == 1)
					{
						entry.GetComponent<Text>().text = oBuy["Beets"].ToString();
						totalVeg += oBuy["Beets"];
						totalBought += oBuy["Squash"];
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
					if (counter == 0 && oSup != null)
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
		}
	}
}
