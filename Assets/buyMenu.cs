using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyMenu : MonoBehaviour
{

	private Button _buyButton;
	private Supplier _supplier;
	private Text _displayText;
	private Supply _supply;
	private WarehouseManager _warehouseManager;

	// Use this for initialization
	void Start ()
	{
		_buyButton = GameObject.Find("BuyButton").GetComponent<Button>();
		_supplier = gameObject.GetComponent<Supplier>();
		_displayText = gameObject.GetComponent<Text>();
		_supply = GameObject.Find("Storage").GetComponent<Supply>();
		_warehouseManager = GameObject.Find("Main Camera").GetComponent<WarehouseManager>();

		_displayText.text = _supplier.vendorName + "\r\n" + _supplier.vegetable + ": " + _supplier.stock + "\r\n" +
		                    "Price: $" + _supplier.price;
		_buyButton.GetComponentInChildren<Text>().text = "Buy";
		
		_buyButton.onClick.AddListener(getSupply);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void getSupply()
	{
		// for now just buys the supplier out, can later add an option to the gui for how much you want to buy 
		_supply.AddStorage("Broccoli", _supplier.stock);
		_supplier.stock = 0;
		_buyButton.interactable = false;
		_displayText.text = _supplier.vendorName + "\r\n" + _supplier.vegetable + ": " + _supplier.stock + "\r\n" +
		                    "Price: " + _supplier.price;
		// have to subtract money but there is currently no revenue
		_warehouseManager.Money -= _supplier.price * _supplier.stock;
	}
}
