using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Supply : MonoBehaviour
{
	// initializing public variables
	public Dictionary<string, int> 	StoredItems;		// dictionary of all items stored
	
	
	// Use this for initialization
	void Start ()
	{
        //StoredItems = new Dictionary<string, int>();
        //List<string> SupportedProducts = GameObject.FindObjectOfType<WarehouseManager>().SupportedProducts;
        //foreach (string s in SupportedProducts)
        //{
        //    AddStorage(s, 200);
        //}
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
        GameObject.FindObjectOfType<Panels>().UpdateSupply();
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
        GameObject.FindObjectOfType<Panels>().UpdateSupply();
	}

	public bool AvailableItem(string productName, int value)
	{
		if (StoredItems.ContainsKey(productName) && StoredItems[productName] >= value )
		{
			return true;
		}
		return false;
	}
	
}

