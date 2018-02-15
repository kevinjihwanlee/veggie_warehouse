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
    public List<Order> Orders; // please tell me there's a better way to do this
    public Supply _supply;
    public List<string> OrderCompanies;
    public Panels _panels;
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
	}


	public void NextDay()
	{

        List<Order> remove = new List<Order>();

		var orderSupplyMenu = GameObject.Find("OrderSupplyMenu").gameObject.GetComponent<BuyMenu2>(); 
		_supplyTotalOrder = orderSupplyMenu._totalOrder;
		BuyMoreSupply(_supplyTotalOrder);
		orderSupplyMenu.NextDayReset();

        foreach (Order o in Orders)
        {
            //if the player has hit fulfill and has enough inventory
            if (o.Fulfilled)
            {
                Object.Destroy(o.gameObject);
                remove.Add(o);
                Money += o.magnitude * 15;
            }
            else
            {
                o.FulfillFail = false;
                if (Money > 100)
                    Money -= 100;
                else
                    Money = 0;
            }
        }
        foreach (Order o in remove)
        {
            Orders.Remove(o);
        }
        foreach (string s in SupportedProducts)
        {
            _supply.RemoveStaged(s);
            _supply.RemoveOrdered(s);
        }
		
		Day += 1;

        if(Orders.Count == 0)
            Orders.Add(GenerateNewOrder());

        _panels.UpdateMoney();
        _panels.UpdateDay();
        _panels.UpdateSupply();
        //_panels.UpdateOrdered();
	}

	// very basic new order generator
    private Order GenerateNewOrder()
    {
        var o = (GameObject)Object.Instantiate((GameObject)Resources.Load("Order"));

        o.transform.SetParent(GameObject.Find("Canvas").transform);
        o.transform.localScale = new Vector3(2, .5f, 1);

        Vector3 oldpos = o.transform.position;
        oldpos = oldpos + new Vector3(567,600,0);
        o.transform.position = oldpos;

		var ord = new Dictionary<string, int>();
        foreach (string product in SupportedProducts)
            ord[product] = Random.Range(0, 15);
        
        int index = (int)(Random.value * OrderCompanies.Count);

        o.GetComponent<Order>().initialize(ord,OrderCompanies[index]);

        return o.GetComponent<Order>();
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

	void BuyMoreSupply(Dictionary<string, int> supplyOrder)
	{
		var currentOrder = GameObject.Find("OrderSupplyMenu").gameObject.GetComponent<BuyMenu2>()._totalOrder;
		foreach (KeyValuePair<string, int> product in currentOrder)
		{
			_supply.AddStorage(product.Key, product.Value);
		}
	}

}
