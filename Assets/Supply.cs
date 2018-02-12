using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Supply : MonoBehaviour
{
	// initializing public variables
	public Dictionary<string, int> StoredItems;
    public Dictionary<string, int> StagedItems;// dictionary of all items stored
	
	
	// Use this for initialization
	void Start ()
    {
        StoredItems = new Dictionary<string, int>();
        StagedItems = new Dictionary<string, int>();
	}

    public void initialize(List<string> SupportedProducts, int quantity)
    {
        StoredItems = new Dictionary<string, int>();
        StagedItems = new Dictionary<string, int>();
        foreach (string s in SupportedProducts)
        {
            AddStorage(s, quantity);
            AddStaged(s, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddStorage(string productName, int value)
	{
		if (!StoredItems.ContainsKey(productName))
		{
			StoredItems[productName] = value;
//			Debug.Log("just added new item: " + productName + " to storage");
            //Debug.Log(StoredItems[productName]);
		}
		else
		{
			StoredItems[productName] += value;
/*			Debug.Log("updated existing item: " + productName);*/
		}
	}

    public void AddStaged(string productName, int value)
    {
        if (!StagedItems.ContainsKey(productName))
        {
            StagedItems[productName] = value;
            //          Debug.Log("just added new item: " + productName + " to storage");
            //Debug.Log(StoredItems[productName]);
        }
        else
        {
            StagedItems[productName] += value;
            /*          Debug.Log("updated existing item: " + productName);*/
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

	public bool AvailableItem(string productName, int value)
	{
        if (StoredItems.ContainsKey(productName) && StoredItems[productName] >= value + StagedItems[productName])
		{
			return true;
		}
		return false;
	}
	
}

