using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenu2 : MonoBehaviour {

	private WarehouseManager _warehouseManager;
	public int AvailableFunds;
	public int TotalOrderCost;
	public Dictionary<string, int> _totalOrder;

	// Use this for initialization
	void Start ()
	{
		_warehouseManager = GameObject.Find("Main Camera").GetComponent<WarehouseManager>();
		
		_totalOrder = new Dictionary<string, int>();

        foreach(string product in GameObject.FindObjectOfType<WarehouseManager>().SupportedProducts)
		{
			_totalOrder[product] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		AvailableFunds = _warehouseManager.Money;

		//TotalOrderCost = 0;
		
		//foreach(Transform child in transform)
		//{
		//	var product = child.gameObject.GetComponent<ModifyOrder>();
		//	if (product != null)
		//	{
  //              TotalOrderCost += product.SupplyOrderQuantity * 10;
  //              _totalOrder[product.ProductName] = product.SupplyOrderQuantity;
		//	}
		//}

		transform.Find("Cost").gameObject.GetComponent<Text>().text = "Total Cost: " + TotalOrderCost;
	}

	public void NextDayReset()
	{
        GameObject.FindObjectOfType<WarehouseManager>().Money -= TotalOrderCost;
        TotalOrderCost = 0;
		foreach (Transform child in transform)
		{
			var product = child.gameObject.GetComponent<ModifyOrder>();
			if (product != null)
			{
				product.Reset();
			}
		}
	}
}
