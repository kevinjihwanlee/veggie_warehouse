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
	void Start () {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
		foreach (Transform child in gameObject.transform)
		{
			child.gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
		}
		_inventoryReceiptObject = GameObject.Find("InventoryReceipt");

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
		
	}
}
