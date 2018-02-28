using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterObject : MonoBehaviour
{

	private GameObject _buyMenu;
	private bool _present;
	
	// Use this for initialization
	void Start () {
		_buyMenu = GameObject.Find("OrderSupplyMenu");
		//_buyMenu.gameObject.transform.localScale = new Vector3(0, 0, 0);
		_present = true;
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown()
	{
		if (!_present)
		{
			_buyMenu.gameObject.transform.localScale = new Vector3(1, 1, 1);
			_present = true;
		}
		else
		{
			_buyMenu.gameObject.transform.localScale = new Vector3(0, 0, 0);
			_present = false;
		}

	}
	
}
