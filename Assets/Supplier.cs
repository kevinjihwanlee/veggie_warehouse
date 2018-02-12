using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplier : MonoBehaviour
{

	public string vendorName;
	public string vegetable;
	public int stock;
	public int price;
	
	// Use this for initialization
	void Start ()
	{
		vendorName = "Wholesale Supplier Foo";
		vegetable = "Beets";
		stock = 5;
		price = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
