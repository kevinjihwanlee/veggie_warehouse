using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supplyDock : MonoBehaviour
{

	private GameObject _storage;
	
	// is this the best way to manage menus?
	private GameObject _buymenu;
	
	// Use this for initialization
	void Start () {
		_storage = GameObject.Find("Storage");
		_buymenu = GameObject.Find("BuyMenu");
		_buymenu.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown()
	{
		// _storage.GetComponent<storage>().RemoveStorage("corn", 5);
		// on click should open a menu UI for buying
		if (_buymenu.gameObject.activeSelf)
		{
			_buymenu.gameObject.SetActive(false);
		}
		else
		{
			_buymenu.gameObject.SetActive(true);
		}
	}
}
