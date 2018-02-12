using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shippingDock : MonoBehaviour
{

    public storage _storage;
	
	// Use this for initialization
	void Start () {
		
        _storage = GameObject.FindObjectOfType<storage>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void initialize(List<string> SupportedProducts, int quantity)
    {
        _storage.StoredItems = new Dictionary<string, int>();
        foreach (string s in SupportedProducts)
        {
            _storage.AddStorage(s, quantity);
        }
    }
	
	void OnMouseDown()
	{
		//_storage.GetComponent<storage>().RemoveStorage("corn", 5);
	}
}
