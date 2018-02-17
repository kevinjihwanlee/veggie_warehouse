using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseManager : MonoBehaviour
{
    // I'm attaching this to the camera cuz idk where to rlly put this

    // basically just holds shit like actions, pay, satisfaction, etc all in a 
    // consolidated public place so we can edit it however we want @dt is this bad practice?

    public List<string> Actions;
    public int Day;
    public List<string> SupportedProducts;
    public float Satisfaction; // tbd if we want scale or less analog measure (rn out of 100)
    public Supply _supply;
    public List<string> OrderCompanies;
    public List<Order> Orders; 
    public Panels _panels;
    public int OrderCount;
	// possibly make orders an object? 
	// we should prob make it an object tbh if we're going to keep track of shit like price 
	// but orders should eventually be a dictionary so you can choose which order you want to ignore and which to fulfill
	// i'm just doing it like a stack rn cuz it's easy and makes a bit of sense
	public int Money;

	private Dictionary<string, int> _supplyTotalOrder;
	// Use this for initialization
	void Start()
	{
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
        Money = 50;

        //to be indexed randomly as orders are created
        OrderCompanies = new List<string> { "Whole Jewels", "Trader Bills", "Food Osco", "Bullseye", "Floormart", "DangerWay" };

        //the UI game object that has all the static UI functions
        _panels = GameObject.FindObjectOfType<Panels>();
        _panels.UpdateDay();
        _panels.UpdateMoney();

        //getting the gameobject for the storage and staged orders
        _supply = GameObject.FindObjectOfType<Supply>();
        _supply.initialize(SupportedProducts, 20);
        _panels.UpdateSupply();
        GameObject.Find("SupplyDock").GetComponent<MeshRenderer>().material.color = new Color32(47,50,159,255);
        OrderCount = 1;

        Orders = new List<Order>();
        for (int i = 1; i < 6; i ++)
            Orders.Add(GameObject.Find("Order" + i.ToString()).GetComponent<Order>()); 
	}


	public void NextDay()
	{
        int orderRev = 0;
        int veggieCost = GameObject.FindObjectOfType<BuyMenu2>().TotalOrderCost;
        var orderSupplyMenu = GameObject.Find("OrderSupplyMenu").gameObject.GetComponent<BuyMenu2>();
        _supplyTotalOrder = orderSupplyMenu._totalOrder;
        BuyMoreSupply(_supplyTotalOrder);
        orderSupplyMenu.NextDayReset();

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
                    Money -= 100;
                    orderRev -= 100;
                }
            }
        }
        foreach (string s in SupportedProducts)
        {
            _supply.RemoveStaged(s);
            _supply.RemoveOrdered(s);
        }
		
		Day += 1;
        if (OrderCount < 5)
        {
            float calculatedOrderCount = (Day * 60 + Money) / 500;
            if (calculatedOrderCount > OrderCount)
                OrderCount = (int)calculatedOrderCount;
        }

        int i = 1;
        while (OrderCount > active + i)
        {
            GenerateNewOrder(Orders[active + i]);
            i++;
        }

        _panels.UpdateDay();
        _panels.UpdateReceipt(orderRev, veggieCost);
        _panels.UpdateMoney();
        _panels.UpdateSupply();
	}

	// very basic new order generator
    private void GenerateNewOrder(Order o)
    {
        int magnitude = 0;
		var ord = new Dictionary<string, int>();
        foreach (string product in SupportedProducts)
        {
            ord[product] = Random.Range(0, 15);
            magnitude += ord[product];
        }
        int multiplier = Random.Range(11, 16);
        int value = multiplier * magnitude;
        string client = OrderCompanies[Random.Range(0, OrderCompanies.Count)];
        o.initialize(ord,client,value,magnitude);
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
    }

	void BuyMoreSupply(Dictionary<string, int> supplyOrder)
	{
		var currentOrder = GameObject.Find("OrderSupplyMenu").gameObject.GetComponent<BuyMenu2>()._totalOrder;
		foreach (KeyValuePair<string, int> product in currentOrder)
		{
			_supply.AddStorage(product.Key, product.Value);
		}
	}

}
