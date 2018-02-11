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
	public List<Dictionary<string, int>> Orders; // please tell me there's a better way to do this
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
			"Broccoli",
			"Corn",
			"Eggplant",
		};

		Satisfaction = 50f;

		Money = 500;
	}


	void NextDay()
	{
		
		// first check to see if longest order can be fulfilled
		if (Orders.Count > 0)
		{
			var oldestOrder = Orders[0];
			if (FulfillOrder(oldestOrder))
			{
				// maybe add ui to this?
				Debug.Log("just fulfilled this order");
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
	}

	// very basic new order generator
	private Dictionary<string, int> GenerateNewOrder()
	{
		var order = new Dictionary<string, int>();
		foreach (string product in SupportedProducts)
		{
			int quantity = Random.Range(0, 7);
			if (quantity > 0)
			{
				order[product] = quantity;
			}
		}

		return order;
	}

	// this function checks the shipping dock to see if orders can be fulfilled
	// lets play you have to have the order items in the shipping dock to fulfill it 
	// returns true if order is fulfilled, false if order isn't
	public bool FulfillOrder(Dictionary<string, int> order)
	{
		// sorry this is gross, prob a better way to do this
		var shippingDock = GameObject.Find("ShippingDock");
		foreach (KeyValuePair<string, int> item in order)
		{
			if (!shippingDock.GetComponent<Supply>().AvailableItem(item.Key, item.Value))
			{
				return false;
			}
		}
		
		foreach (KeyValuePair<string, int> item in order)
		{
			shippingDock.GetComponent<Supply>().RemoveStorage(item.Key, item.Value);
		}

		return true;

	}

}
