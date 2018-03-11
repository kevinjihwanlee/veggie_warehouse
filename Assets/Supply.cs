using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Supply : MonoBehaviour
{
	// initializing public variables
	public Dictionary<string, int> StoredItems; //Items we have in inventory
    public Dictionary<string, int> StagedItems; //Inventory items to be taken out at the end of the day
    public Dictionary<string, int> OrderedItems; //Products we've ordered to be put in inventory at the end of the day

	public int MaxStorage;
	public double spoilRate;
    // dictionary of all items stored
	
	
	// Use this for initialization
	void Start ()
	{
		MaxStorage = 30;
		spoilRate = 0.2;
        StoredItems = new Dictionary<string, int>();
        StagedItems = new Dictionary<string, int>();
        OrderedItems = new Dictionary<string, int>();
	}

    public void initialize(List<string> SupportedProducts, int quantity)
    {
        StoredItems = new Dictionary<string, int>();
        StagedItems = new Dictionary<string, int>();
        OrderedItems = new Dictionary<string, int>();
        foreach (string s in SupportedProducts)
        {
            AddStorage(s, quantity);
            AddStaged(s, 0);
            AddOrdered(s, 0);
        }
    }

	public void UpgradeStorage(int increment)
	{
		MaxStorage += increment;
		FindObjectOfType<Panels>().UpdateSupply();
		FindObjectOfType<Panels>().UpdateProjected();
	}

	public void ReduceSpoilRate(double reducer)
	{
		spoilRate = spoilRate/reducer;
		FindObjectOfType<Panels>().UpdateSpoilRate();
	}

	public void AddStorage(string productName, int value)
	{
		if (!StoredItems.ContainsKey(productName))
		{
			if (value > MaxStorage)
			{
				StoredItems[productName] = MaxStorage;
			}
			
			StoredItems[productName] = value;
		}
		else
		{
			// check if you have more than max storage
			StoredItems[productName] += value;
			if (StoredItems[productName] > MaxStorage)
			{
				StoredItems[productName] = MaxStorage;
			}
		}
	}

    public void AddStaged(string productName, int value)
    {
        if (!StagedItems.ContainsKey(productName))
        {
            StagedItems[productName] = value;
        }
        else
        {
            StagedItems[productName] += value;
        }
    }

    public void UndoStaged(string productName, int value)
    {
        StagedItems[productName] -= value;
    }

    public void AddOrdered(string productName, int value)
    {
        if (!OrderedItems.ContainsKey(productName))
        {
            OrderedItems[productName] = value;
        }
        else
        {
            OrderedItems[productName] += value;
        }
    }

	public void RemoveStorage(string productName, int value)
	{
		if (StoredItems.ContainsKey(productName))
		{
			if (StoredItems[productName] < value)
			{
				// how should we handle this case?
				Debug.Log("this should break because there's not enough of the item in storage");
			}
			else
			{
				StoredItems[productName] -= value;
			}
        }
	}

    public void RemoveStaged(string productName)
    {
        if (StagedItems.ContainsKey(productName))
        {
            StoredItems[productName] -= StagedItems[productName];
            StagedItems[productName] = 0;
        }
    }

    public void RemoveOrdered(string productName)
    {
        if (OrderedItems.ContainsKey(productName))
        {
            StoredItems[productName] += OrderedItems[productName];
            OrderedItems[productName] = 0;
        }
    }

	public int Spoil(string productName, int day)
	{	
		int amountSpoiled = Convert.ToInt32(spoilRate * StoredItems[productName]);

/*		if (StoredItems.ContainsKey(productName))
		{
			RemoveStorage(productName, amountSpoiled);
		}*/

		if (day == 0)
		{
			amountSpoiled = 0;
		}

		return amountSpoiled;

	}

	public bool AvailableItem(string productName, int value)
	{
        if (StoredItems.ContainsKey(productName) && StoredItems[productName] >= value + StagedItems[productName])
		{
			return true;
		}
		return false;
	}
	
}

