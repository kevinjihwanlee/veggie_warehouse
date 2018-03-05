using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{

	private bool _notVisible;

	// Use this for initialization
	void Start ()
	{
		_notVisible = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		if (_notVisible)
		{
			FindObjectOfType<UpgradeMenu>().ShowUpgradeMenu();
			_notVisible = false;
		}
		else
		{
			FindObjectOfType<UpgradeMenu>().HideUpgradeMenu();
			_notVisible = true;
		}
	}
}
