using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Audio;

public class WarehouseManager : MonoBehaviour
{
    // I'm attaching this to the camera cuz idk where to rlly put this

    // basically just holds shit like actions, pay, satisfaction, etc all in a 
    // consolidated public place so we can edit it however we want @dt is this bad practice?

    public List<string> Actions;
    public int Day;
    public List<string> SupportedProducts;
    public Dictionary<string, int> buyPrices;
    public Dictionary<string, int> sellPrices;
    public Dictionary<string, int> capPrices;
    public float Satisfaction; // tbd if we want scale or less analog measure (rn out of 100)
    public Supply _supply;
    public List<string> OrderCompanies;
    public List<Order> Orders; 
    public Panels _panels;
    public int OrderCount;
	public int NewOrderDurationOffset;
	
    public AudioClip chaching;

	public StorageObject _storage;

	private GameObject _buyMenu;
	public GameObject _inventoryRecap;

    public int failed;
	// possibly make orders an object? 
	// we should prob make it an object tbh if we're going to keep track of shit like price 
	// but orders should eventually be a dictionary so you can choose which order you want to ignore and which to fulfill
	// i'm just doing it like a stack rn cuz it's easy and makes a bit of sense
	public int Money;

	private GameObject _endScreen;

	private Dictionary<string, int> _supplyTotalOrder;
	// Use this for initialization
	void Start()
	{
		NewOrderDurationOffset = 0;
		_buyMenu = GameObject.Find("OrderSupplyMenu");
		_endScreen = GameObject.Find("EndGame");
		_endScreen.SetActive(false);
		// initializing actions
		// tbh not sure this is necessary
		Actions = new List<string> {"addItem"};

		SupportedProducts = new List<string>
		{
			"Corn",
            "Squash",
			"Beets",
        };

		Satisfaction = 50f;

        Day = 0;
        Money = 150;
        failed = 0;

        //to be indexed randomly as orders are created
        OrderCompanies = new List<string> { "Whole Jewels", "Trader Bills", "Food Osco", "Bullseye", "Floormart", "DangerWay" };

        buyPrices = new Dictionary<string, int>();
        sellPrices = new Dictionary<string, int>();
		capPrices = new Dictionary<string, int>();
		
        buyPrices["Corn"] = 12;
        buyPrices["Squash"] = 10;
        buyPrices["Beets"] = 8;
		
        sellPrices["Corn"] = 20;
        sellPrices["Squash"] = 16;
        sellPrices["Beets"] = 14;
		
        capPrices["Corn"] = 20;
        capPrices["Squash"] = 16;
        capPrices["Beets"] = 14;

        //the UI game object that has all the static UI functions
        _panels = GameObject.FindObjectOfType<Panels>();
        _panels.UpdateDay();
        _panels.UpdateMoney();

        //getting the gameobject for the storage and staged orders
        _supply = GameObject.FindObjectOfType<Supply>();
        _supply.initialize(SupportedProducts, 20);
        _panels.UpdateSupply();

        // setting colors and active state of 3D models and UI
        //GameObject.Find("SupplyDock").GetComponent<MeshRenderer>().material.color = new Color32(47,50,159,255);

        GameObject.Find("InventoryReceipt").gameObject.transform.localScale = new Vector3(0, 0, 0);
		
        OrderCount = 1;

        Orders = new List<Order>();
        for (int i = 1; i < 6; i ++)
            Orders.Add(GameObject.Find("Order" + i.ToString()).GetComponent<Order>()); 
		
		//the StorageObject used for the inventory receipt
		_storage = GameObject.Find("Storage").GetComponent<StorageObject>();
		
		// for the tutorial
		GameObject.Find("Money").gameObject.transform.localScale = new Vector3(0, 0, 0);
		GameObject.Find("Day").gameObject.transform.localScale = new Vector3(0, 0, 0);
		GameObject.Find("Lives").gameObject.transform.localScale = new Vector3(0, 0, 0);
		GameObject.Find("Progress Button").gameObject.transform.localScale = new Vector3(0, 0, 0);
		GameObject.Find("Receipt").gameObject.transform.localScale = new Vector3(0, 0, 0);
		GameObject.Find("Upgrade Storage").gameObject.transform.localScale = new Vector3(0, 0, 0);

		//GameObject.Find("Inventory Title").gameObject.transform.localScale = new Vector3(0, 0, 0);
		//GameObject.Find("Inventory").gameObject.transform.localScale = new Vector3(0, 0, 0);

		foreach (Transform child in GameObject.Find("Inventory").gameObject.transform)
		{
			//child.localScale = new Vector3(0, 0, 0);
			child.gameObject.SetActive(false);
        }
        foreach (Transform child in GameObject.Find("ProjectedInventory").gameObject.transform)
        {
            //child.localScale = new Vector3(0, 0, 0);
            child.gameObject.SetActive(false);
        }
		
		GameObject.Find("IncomingOrdersTitle").gameObject.transform.localScale = new Vector3(0, 0, 0);

		_buyMenu.gameObject.transform.localScale = new Vector3(0, 0, 0);
		
		GameObject.Find("Laptop").gameObject.transform.localScale = new Vector3(0,0,0);
		FindObjectOfType<UpgradeMenu>().HideUpgradeMenu();
		
	}

	public void NextDay()
	{
        int orderRev = 0;
        int veggieCost = GameObject.FindObjectOfType<BuyMenu2>().TotalOrderCost;
		
        var orderSupplyMenu = GameObject.Find("OrderSupplyMenu").gameObject.GetComponent<BuyMenu2>();
		
        _supplyTotalOrder = orderSupplyMenu._totalOrder;
		
		
		
        // BuyMoreSupply(_supplyTotalOrder);
		
        int active = -1;
        for (int j = 0; j < Orders.Count; j++)
        {
            Order o = Orders[j];
	        if (o.active)
	        {

		        active = j;
		        //if the player has hit fulfill and has enough inventory
		        if (o.Fulfilled)
		        {
			        Money += o.value;
			        orderRev += o.value;
			        GenerateNewOrder(o);
		        }
		        else
		        {
			        o.FulfillFail = false;
                    if (o.daysRemaining == 0)
                    {
                        failed += 1;
                        GenerateNewOrder(o);
                    }
                    else
                    {
                        o.decrementDays();
                    }
		        }
	        }
        }
		
		orderSupplyMenu.NextDayReset();
		
		// I stuck UpdateInventoryReceipt logic in here for now
        foreach (string s in SupportedProducts)
        {
	        // sets some variables
	        var stageVal = _supply.StagedItems[s];
	        var boughtVal = _supplyTotalOrder[s];
	        
	        // removes the staged order(s) from supply
	        _inventoryRecap = GameObject.Find(s + "Shipped");
	        _inventoryRecap.GetComponent<Text>().text = stageVal.ToString();
            _supply.RemoveStaged(s);
	        
	        // removes the spoiled items from remaining supply
		    var spoiledVal = _supply.Spoil(s, Day);
		    _inventoryRecap = GameObject.Find(s + "Spoiled");
		    _inventoryRecap.GetComponent<Text>().text = spoiledVal.ToString();
		    _supply.RemoveStorage(s, spoiledVal);
	        
	        // adds bought items to supply
	        _inventoryRecap = GameObject.Find(s + "Bought");
	        _inventoryRecap.GetComponent<Text>().text = boughtVal.ToString();
            //_supply.RemoveOrdered(s);
	        BuyMoreSupply(_supplyTotalOrder, s);
	        
            _supplyTotalOrder[s] = 0;
	        
	        // updates net totals
	        var totalVeg = boughtVal - stageVal - spoiledVal;
	        _storage.UpdateInventoryReceiptNet(s, totalVeg.ToString());

        }
		
		Day += 1;
        if (OrderCount < 5)
        {
            int v = Money;
            foreach(string s in SupportedProducts){
                v += _supply.StoredItems[s] * buyPrices[s];
            }
            float calculatedOrderCount = (Day * 60 + v) / 730;
            if (calculatedOrderCount > OrderCount)
                OrderCount = (int)calculatedOrderCount;
        }

        int i = 1;
        while (OrderCount > active + i)
        {
            GenerateNewOrder(Orders[active + i]);
            i++;
        }

		
		RandomEventGenerator();
        _panels.UpdateDay();
        _panels.UpdateReceipt(orderRev, veggieCost);
        _panels.UpdateMoney();
        _panels.UpdateSupply();
        _panels.UpdateLives();
        _panels.UpdateProjected();

        GameObject.Find("ChachingSound").GetComponent<AudioSource>().Play();

		if (Day > 15)
		{
			WinGame();	
		}
		
        if (failed > 2)
            LoseGame();
		
	}

	void RandomEventGenerator()
	{
		// we'll probably wanna put this in a recap change
		var prob = Random.Range(0, 100);
		
		// percentage of the time we want random events to occur
		var eventProbability = 10;
		if (prob > eventProbability) return;
		
        var selector = prob % 3;
        var veggie = SupportedProducts[selector];
		var priceChange = Random.Range(-3, 3);
		
		ChangeVeggiePrice(veggie, priceChange);
		
		_buyMenu.GetComponent<BuyMenu2>().UpdateAllPrices();
	}

	// very basic new order generator
    public void GenerateNewOrder(Order o)
    {
        int magnitude = 0;
		var ord = new Dictionary<string, int>();
        int value = 0;
        int duration = 0;
        if (Random.Range(1, 3) == 1)
            duration = Random.Range(1, 4);
        foreach (string product in SupportedProducts)
        {
            ord[product] = Random.Range(0, 15 + 4 * duration);
            value += ord[product] * sellPrices[product];
            magnitude += ord[product];
        }
        value += 50 * duration;
        string client = OrderCompanies[Random.Range(0, OrderCompanies.Count)];
	    duration += NewOrderDurationOffset;
        o.initialize(ord, client, value, duration, sellPrices);
        o.gameObject.GetComponent<Transform>().localScale = new Vector3(2, .6f, 1);
	}

	// this function checks the shipping dock to see if orders can be fulfilled
	// lets play you have to have the order items in the shipping dock to fulfill it 
	// returns true if order is fulfilled, false if order isn't
    public bool StageOrder(Order o)
    {
        Dictionary<string,int> order = o.order;
		// sorry this is gross, prob a better way to do this
		foreach (KeyValuePair<string, int> item in order)
		{
            if (!_supply.AvailableItem(item.Key, item.Value))
			{
				return false;
			}
		}
		foreach (KeyValuePair<string, int> item in order)
		{
            _supply.AddStaged(item.Key, item.Value);
        }
        _panels.UpdateProjected();
        return true;
    }

    public void UnstageOrder(Order o)
    {
        Dictionary<string, int> order = o.order;
        // sorry this is gross, prob a better way to do this
        foreach (KeyValuePair<string, int> item in order)
        {
            _supply.UndoStaged(item.Key, item.Value);
        }
        _panels.UpdateProjected();
    }

	void BuyMoreSupply(Dictionary<string, int> supplyOrder, string veg)
	{
		_supply.AddStorage(veg, supplyOrder[veg]);
		/*var currentOrder = GameObject.Find("OrderSupplyMenu").gameObject.GetComponent<BuyMenu2>()._totalOrder;
		foreach (KeyValuePair<string, int> product in currentOrder)
		{
			_supply.AddStorage(product.Key, product.Value);

		}*/
	
	}

	void WinGame()
	{
		EndGame(true);
	}

	void LoseGame()
	{
		EndGame(false);
	}

	void EndGame(bool win)
	{
		_endScreen.SetActive(true);
		if (!win)
		{
//			_endScreen.GetComponent<Text>().text = "You Lose";
			_endScreen.GetComponentInChildren<Text>().text = "You Lose";
		}
	}

	bool CanFulfillOrders()
	{
		var fulfillableOrder = false;
		
		foreach (Order o in Orders)
		{
			var lackSupply = false;

			if (!o.active)
			{
				continue;
			}
			
			foreach (KeyValuePair<string, int> item in o.order)
			{
				if (!_supply.AvailableItem(item.Key, item.Value))
				{
					lackSupply = true;
					break;
				}					
			}

			if (lackSupply)
			{
				continue;
			}

			fulfillableOrder = true;

		}

		return fulfillableOrder;
	}

	public void Buy(int amount)
	{
		Money -= amount;
		FindObjectOfType<Panels>().UpdateMoney();
	}


	public void ChangeVeggiePrice(string veggie, int amount)
	{
		var minPrice = 5;

		if (buyPrices[veggie] + amount > capPrices[veggie])
		{
			buyPrices[veggie] = capPrices[veggie];
		}
		else if (buyPrices[veggie] + amount < minPrice)
		{
			buyPrices[veggie] = minPrice;
		}
		else
		{
			buyPrices[veggie] += amount;
		}
	}
}
