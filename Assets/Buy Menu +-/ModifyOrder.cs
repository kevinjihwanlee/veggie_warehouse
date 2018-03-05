using System;
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
	private Button _addOne;
	private Button _removeOne;
	private string _originalText;
	public int _availableFunds;
	public int _totalOrderCost;
	public int _price;
	public string ProductName;
	private Dictionary<string, int> _buyPrices;
	private bool _firstTime;
	
	// Use this for initialization
	void Start ()
	{
		_firstTime = false;
        SupplyOrderQuantity = 0;
		_add = transform.Find("Add").gameObject.GetComponent<Button>();
		_remove = transform.Find("Remove").gameObject.GetComponent<Button>();
		_addOne = transform.Find("Add 1").gameObject.GetComponent<Button>();
		_removeOne = transform.Find("Remove 1").gameObject.GetComponent<Button>();
		DeltaQuantity = 5;

		
		_add.onClick.AddListener(AddOrder);
        _remove.onClick.AddListener(RemoveOrder);
		_addOne.onClick.AddListener(AddOrderOne);
        _removeOne.onClick.AddListener(RemoveOrderOne);

        ProductName = name;
		
        _originalText = GetComponent<Text>().text;
		Debug.Log(ProductName);
        GetComponent<Text>().text = _originalText + SupplyOrderQuantity.ToString();

        _price = 10;
	}

	void Update()
	{
		if (!_firstTime)
		{
			_firstTime = true;
			UpdatePrices();
		}
		
        _price = GameObject.FindObjectOfType<WarehouseManager>().buyPrices[ProductName];
		_availableFunds = GetComponentInParent<BuyMenu2>().AvailableFunds;
		_totalOrderCost = GetComponentInParent<BuyMenu2>().TotalOrderCost;
		
        if (_totalOrderCost + DeltaQuantity*_price > _availableFunds)
			DisableAddButton();

		if (_totalOrderCost + DeltaQuantity*_price <= _availableFunds)
			EnableButton();
		
        if (_totalOrderCost + _price > _availableFunds)
			DisableAddOneButton();

		if (_totalOrderCost + _price <= _availableFunds)
			EnableOneButton();

		if (SupplyOrderQuantity > 0)
		{
			_removeOne.interactable = true;
		}
		else
		{
			_removeOne.interactable = false;
		} 
		
		if (SupplyOrderQuantity >= 5)
		{
			_remove.interactable = true;
		}
		else
		{
            _remove.interactable = false;
		}
        
	}

	public void Reset()
	{
        SupplyOrderQuantity = 0;
        GetComponent<Text>().text = _originalText + "($" + _buyPrices[ProductName] + "): "+ SupplyOrderQuantity.ToString();
	}

	void AddOrder()
	{
		_buyPrices = GameObject.Find("Main Camera").GetComponent<WarehouseManager>().buyPrices;
		SupplyOrderQuantity += DeltaQuantity;
        BuyMenu2 bm = GameObject.Find("OrderSupplyMenu").GetComponent<BuyMenu2>();
        bm.TotalOrderCost += DeltaQuantity * _price;
        bm._totalOrder[ProductName] = SupplyOrderQuantity;
        GetComponent<Text>().text = _originalText + "($" + _buyPrices[ProductName] + "): "+ SupplyOrderQuantity.ToString();
        GameObject.FindObjectOfType<Panels>().UpdateProjected();
	}
	
	void RemoveOrder()
	{
		_buyPrices = GameObject.Find("Main Camera").GetComponent<WarehouseManager>().buyPrices;
		SupplyOrderQuantity -= DeltaQuantity;

        if (SupplyOrderQuantity < 0)
        {
            SupplyOrderQuantity = 0;
        }
        
        else
        {
            BuyMenu2 bm = GameObject.Find("OrderSupplyMenu").GetComponent<BuyMenu2>();
            bm.TotalOrderCost -= DeltaQuantity * _price;
            bm._totalOrder[ProductName] = SupplyOrderQuantity;
        	GetComponent<Text>().text = _originalText + "($" + _buyPrices[ProductName] + "): "+ SupplyOrderQuantity.ToString();
        }
        GameObject.FindObjectOfType<Panels>().UpdateProjected();
	}
	
	void AddOrderOne()
	{
		_buyPrices = GameObject.Find("Main Camera").GetComponent<WarehouseManager>().buyPrices;
		SupplyOrderQuantity += 1;
        BuyMenu2 bm = GameObject.Find("OrderSupplyMenu").GetComponent<BuyMenu2>();
        bm.TotalOrderCost += _price;
        bm._totalOrder[ProductName] = SupplyOrderQuantity;
        GetComponent<Text>().text = _originalText + "($" + _buyPrices[ProductName] + "): "+ SupplyOrderQuantity.ToString();
        GameObject.FindObjectOfType<Panels>().UpdateProjected();
	}
	
	void RemoveOrderOne()
	{
		_buyPrices = GameObject.Find("Main Camera").GetComponent<WarehouseManager>().buyPrices;
		SupplyOrderQuantity -= 1;

        if (SupplyOrderQuantity < 0)
        {
            SupplyOrderQuantity = 0;
        }
        else
        {
            BuyMenu2 bm = GameObject.Find("OrderSupplyMenu").GetComponent<BuyMenu2>();
            bm.TotalOrderCost -= _price;
            bm._totalOrder[ProductName] = SupplyOrderQuantity;
        	GetComponent<Text>().text = _originalText + "($" + _buyPrices[ProductName] + "): "+ SupplyOrderQuantity.ToString();
        }
        GameObject.FindObjectOfType<Panels>().UpdateProjected();
	}

	public void UpdatePrices()
	{
		_buyPrices = GameObject.Find("Main Camera").GetComponent<WarehouseManager>().buyPrices;
		GetComponent<Text>().text = _originalText + "($" + _buyPrices[ProductName] + "): ";
	}

	public void DisableAddButton()
	{
		_add.interactable = false;
	}
	
	private void EnableButton()
	{
		_add.interactable = true;
	}

	public void DisableAddOneButton()
	{
		_addOne.interactable = false;
	}

	public void EnableOneButton()
	{
		_addOne.interactable = true;
	}
	
	
}
