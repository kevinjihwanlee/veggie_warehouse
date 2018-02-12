using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyMenu : MonoBehaviour
{

    private Button _buyButton;
    private Button _buyCorn;
    private Button _buySquash;
    private Button _buyBeets;
	private Supplier _supplier;
	private Text _displayText;
	private Supply _supply;
	private WarehouseManager _warehouseManager;

	private Text _beetGuiStock; 

	// Use this for initialization
	void Start ()
	{
        _buyCorn = GameObject.Find("BuyCorn").GetComponent<Button>();
        _buySquash = GameObject.Find("BuySquash").GetComponent<Button>();
        _buyBeets = GameObject.Find("BuyBeets").GetComponent<Button>();
		//_supplier = gameObject.GetComponent<Supplier>();
		_displayText = gameObject.GetComponent<Text>();
        _supply = GameObject.FindObjectOfType<Supply>();
		_warehouseManager = GameObject.Find("Main Camera").GetComponent<WarehouseManager>();

		//_beetGuiStock = GameObject.Find("Beets Inventory").GetComponent<Text>();

		_displayText.text = "Buy Corn, Squash, or Beets to increase your stock\nin one day!\n$50 for a bundle of 5.";
		
        _buyCorn.onClick.AddListener(buyCorn);
        _buySquash.onClick.AddListener(buySquash);
        _buyBeets.onClick.AddListener(buyBeets);
	}
	
	// Update is called once per frame
	void Update () {
		
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

    void buyCorn()
    {
        buyVeggie("Corn");
    }
    void buySquash()
    {
        buyVeggie("Squash");
    }
    void buyBeets()
    {
        buyVeggie("Beets");
    }

    public void buyVeggie(string veg)
    {
        int total = 0;
        foreach (string s in _warehouseManager.SupportedProducts)
        {
            total += _supply.OrderedItems[s];
        }
        if(_warehouseManager.Money >= 50)
        {
            _supply.AddOrdered(veg, 5);
            _warehouseManager.Money -= 50;
            GameObject.FindObjectOfType<Panels>().UpdateMoney();
            GameObject.FindObjectOfType<Panels>().UpdateOrdered();
        }
    }
}
