using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenu2 : MonoBehaviour {

    private Button _buyButton;
	private Supplier _supplier;
	private Text _displayText;
	private Supply _supply;
	
	private WarehouseManager _warehouseManager;
	public int AvailableFunds;
	public int TotalOrderCost;

	private Text _beetGuiStock;
	public Dictionary<string, int> _totalOrder;

	// Use this for initialization
	void Start ()
	{
        _supply = GameObject.FindObjectOfType<Supply>();
		_warehouseManager = GameObject.Find("Main Camera").GetComponent<WarehouseManager>();
		
		_totalOrder = new Dictionary<string, int>();

		foreach(Transform child in transform)
		{
			var product = child.gameObject.GetComponent<ModifyOrder>();
			if (product != null)
			{
				_totalOrder[product.ProductName] = 0;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		AvailableFunds = _warehouseManager.Money;

		TotalOrderCost = 0;
		
		foreach(Transform child in transform)
		{
			var product = child.gameObject.GetComponent<ModifyOrder>();
			if (product != null)
			{
                TotalOrderCost += product.SupplyOrderQuantity * 10;
                _totalOrder[product.ProductName] = product.SupplyOrderQuantity;
			}
		}

		transform.Find("Cost").gameObject.GetComponent<Text>().text = "Total Cost: " + TotalOrderCost;
	}
	
	void getSupply()
	{
		// for now just buys the supplier out, can later add an option to the gui for how much you want to buy 
		//_supply.AddStorage(_supplier.vegetable, _supplier.stock);

		_buyButton.interactable = false;
		_displayText.text = _supplier.vendorName + "\r\n" + _supplier.vegetable + ": " + _supplier.stock + "\r\n" +
		                    "Price: " + _supplier.price;
		// have to subtract money but there is currently no revenue

		_warehouseManager.Money -= _supplier.price * _supplier.stock;

		GameObject.FindObjectOfType<Panels>().UpdateMoney();
		
		// need to update the stock as well
		
		_supplier.stock = 0;
	}

	public void NextDayReset()
	{
        GameObject.FindObjectOfType<WarehouseManager>().Money -= TotalOrderCost;
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
