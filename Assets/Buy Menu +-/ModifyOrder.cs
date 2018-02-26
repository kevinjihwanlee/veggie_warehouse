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
        GetComponent<Text>().text = _originalText + SupplyOrderQuantity.ToString();
		
		_add.onClick.AddListener(AddOrder);
        _remove.onClick.AddListener(RemoveOrder);

        ProductName = name;

        _price = 10;
	}

	void Update()
	{
		_availableFunds = GetComponentInParent<BuyMenu2>().AvailableFunds;
		_totalOrderCost = GetComponentInParent<BuyMenu2>().TotalOrderCost;
		
        if (_totalOrderCost + DeltaQuantity*_price > _availableFunds)
			DisableAddButton();

		if (_totalOrderCost + DeltaQuantity*_price <= _availableFunds)
			EnableButton();

        if (SupplyOrderQuantity > 0)
            _remove.interactable = true;
        else
            _remove.interactable = false;
        
        _price = GameObject.FindObjectOfType<WarehouseManager>().buyPrices[ProductName];
	}

	public void Reset()
	{
        SupplyOrderQuantity = 0;
        GetComponent<Text>().text = _originalText + SupplyOrderQuantity.ToString();
	}

	void AddOrder()
	{
		SupplyOrderQuantity += DeltaQuantity;
        BuyMenu2 bm = GameObject.Find("OrderSupplyMenu").GetComponent<BuyMenu2>();
        bm.TotalOrderCost += DeltaQuantity * _price;
        bm._totalOrder[ProductName] = SupplyOrderQuantity;
        GetComponent<Text>().text = _originalText + SupplyOrderQuantity.ToString();
	}
	
	void RemoveOrder()
	{
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
            GetComponent<Text>().text = _originalText + SupplyOrderQuantity.ToString();
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
