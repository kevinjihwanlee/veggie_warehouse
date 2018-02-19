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
		int counter;
		int totalVeg;
		
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
				Debug.Log(entry.name);
				_veggieTypeComp = entry.name.Substring(0, 4);
				
				if (_veggieTypeComp.Equals("Corn"))
				{
					if (counter == 0 && oSup != null)
					{
						entry.GetComponent<Text>().text = oSup.order[entry.name.Substring(0, 4)].ToString();
						totalVeg += oSup.order[entry.name.Substring(0, 4)];
					}
					if (counter == 1)
					{
						entry.GetComponent<Text>().text = oBuy["Corn"].ToString();
					}
					if (counter == 3)
					{
						entry.GetComponent<Text>().text = totalVeg.ToString();
					}
					counter++;
				}
				else if (_veggieTypeComp.Equals("Squa"))
				{
					if (counter == 0 && oSup != null)
					{
						entry.GetComponent<Text>().text = oSup.order[entry.name.Substring(0, 6)].ToString();
						/*totalVeggies += o.order[entry.name.Substring(0, 6)];
						Debug.Log(totalVeggies);*/
					}
					//Debug.Log(entry.GetComponent<Text>().text);
					counter++;
				}
				else if (_veggieTypeComp.Equals("Beet"))
				{
					if (counter == 0 && oSup != null)
					{
						entry.GetComponent<Text>().text = oSup.order[entry.name.Substring(0, 5)].ToString();
						/*totalVeggies += o.order[entry.name.Substring(0, 5)];
						Debug.Log(totalVeggies);*/
					}
					//Debug.Log(entry.GetComponent<Text>().text);
					counter++;
				}
				else
				{
					
				}
			}
		}
	}
}
