using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBook : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		if (GameObject.Find("InventoryReceipt").gameObject.transform.localScale == new Vector3(0,0,0))
		{
			GameObject.Find("InventoryReceipt").gameObject.transform.localScale = new Vector3(1,1,1);
		}
	}
}
