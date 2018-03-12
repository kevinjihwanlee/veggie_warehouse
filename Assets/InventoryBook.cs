using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBook : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		var inventoryReceipt = GameObject.Find("InventoryReceipt");
		if (inventoryReceipt.transform.localScale.magnitude == 0)
		{
			inventoryReceipt.gameObject.transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			inventoryReceipt.gameObject.transform.localScale = new Vector3(0, 0, 0);
		}
	}
}
