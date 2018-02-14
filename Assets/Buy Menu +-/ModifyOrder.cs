using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ModifyOrder : MonoBehaviour
{

	public int SupplyOrderQuantity;
	public int DeltaQuantity;

	private Button _add;
	private Button _remove;
	private string _originalText;
	public int _availableFunds;
	public int _totalOrderCost;
	public int _price;
	public string ProductName;
	
	// Use this for initialization
	void Start ()
	{
		SupplyOrderQuantity = 0;
		_add = transform.Find("Add").gameObject.GetComponent<Button>();
		_remove = transform.Find("Remove").gameObject.GetComponent<Button>();
		DeltaQuantity = 5;

		_originalText = GetComponent<Text>().text;
		
		_add.onClick.AddListener(AddOrder);
		_remove.onClick.AddListener(RemoveOrder);

		// hardcoded for now
		_price = 50;

		ProductName = gameObject.name;
	}

	void Update()
	{
		GetComponent<Text>().text = _originalText + SupplyOrderQuantity;

		_availableFunds = GetComponentInParent<BuyMenu2>().AvailableFunds;
		_totalOrderCost = GetComponentInParent<BuyMenu2>().TotalOrderCost;
		
		if (_totalOrderCost + _price > _availableFunds)
		{
			DisableAddButton();
		}

		if (_totalOrderCost + _price <= _availableFunds)
		{
			EnableButton();
		}
	}

	public void Reset()
	{
		SupplyOrderQuantity = 0;
	}

	void AddOrder()
	{
		SupplyOrderQuantity += DeltaQuantity;
	}
	
	void RemoveOrder()
	{
		SupplyOrderQuantity -= DeltaQuantity;
		
		if (SupplyOrderQuantity < 0)
		{
			SupplyOrderQuantity = 0;
		}
	}

	public void DisableAddButton()
	{
		_add.interactable = false;
	}
	
	public void EnableButton()
	{
		_add.interactable = true;
	}
	
	
}
