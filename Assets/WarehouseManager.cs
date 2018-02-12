using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using NUnit.Framework;
using UnityEngine;

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
    public List<string> companies;
	// possibly make orders an object? 
	// we should prob make it an object tbh if we're going to keep track of shit like price 
	// but orders should eventually be a dictionary so you can choose which order you want to ignore and which to fulfill
	// i'm just doing it like a stack rn cuz it's easy and makes a bit of sense
	public int Money;

	// Use this for initialization
	void Start()
	{
		Day = 0;

		// initializing actions
		// tbh not sure this is necessary
		Actions = new List<string> {"addItem"};

		SupportedProducts = new List<string>
		{
			"Corn",
            "Squash",
			"Beets",
        };
        GameObject.FindObjectOfType<Supply>().StoredItems = new Dictionary<string, int>();
        foreach (string s in SupportedProducts)
        {
            GameObject.FindObjectOfType<Supply>().StoredItems[s] = 0;
        }
        foreach (string s in SupportedProducts)
        {
            GameObject.FindObjectOfType<Supply>().AddStorage(s, 50);
        }

		Satisfaction = 50f;

        Money = 500;
        companies = new List<string> { "Whole Jewels", "Trader Bills", "Food Osco", "Bullseye", "Floormart", "DangerWay" };
	}


	public void NextDay()
	{
		
		// first check to see if longest order can be fulfilled
		if (Orders.Count > 0)
		{
			var oldestOrder = Orders[0];
            if (oldestOrder.Fulfilled)
			{
				// maybe add ui to this?
				Debug.Log("just fulfilled this order");
                Object.Destroy(oldestOrder);
				// rn just money increases by a set amount
				Money += 1000;
			}
			else
			{
				Debug.Log("didn't fulfill this order");
			}
		}	
		
		Day += 1;
		
		Debug.Log("It's a new day");
		
		Orders.Add(GenerateNewOrder());

        GameObject.FindObjectOfType<Panels>().UpdateMoney();
        GameObject.FindObjectOfType<Panels>().UpdateDay();
	}

	// very basic new order generator
    private Order GenerateNewOrder()
    {
        var o = (GameObject)Object.Instantiate((GameObject)Resources.Load("Order"));
        o.transform.SetParent(GameObject.Find("Canvas").transform);
        o.transform.localScale = new Vector3(2, .6f, 1);
        Vector3 oldpos = o.transform.position;
        oldpos = oldpos + new Vector3(100,250,0);
        o.transform.position = oldpos;
		var ord = new Dictionary<string, int>();
		foreach (string product in SupportedProducts)
		{
			int quantity = Random.Range(0, 10);
			ord[product] = quantity;
        }
        int index = (int)(Random.value * companies.Count);
        o.GetComponent<Order>().ClientName = companies[index];
        o.GetComponent<Order>().order = ord;
        o.GetComponent<Order>().set();
        return o.GetComponent<Order>();
	}

	// this function checks the shipping dock to see if orders can be fulfilled
	// lets play you have to have the order items in the shipping dock to fulfill it 
	// returns true if order is fulfilled, false if order isn't
    public bool FulfillOrder(Order o)
	{
        Dictionary<string,int> order = o.order;
		// sorry this is gross, prob a better way to do this
		foreach (KeyValuePair<string, int> item in order)
		{
            if (!GameObject.FindObjectOfType<Supply>().AvailableItem(item.Key, item.Value))
			{
				return false;
			}
		}
		
		foreach (KeyValuePair<string, int> item in order)
		{
            GameObject.FindObjectOfType<Supply>().RemoveStorage(item.Key, item.Value);
		}

		return true;

	}

}
