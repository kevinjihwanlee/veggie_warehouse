using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{

	private bool _notVisible;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		var upgradeMenu = FindObjectOfType<UpgradeMenu>();
		if (upgradeMenu.transform.localScale.magnitude > 0)
		{
			FindObjectOfType<UpgradeMenu>().HideUpgradeMenu();
		}
		else
		{
			FindObjectOfType<UpgradeMenu>().ShowUpgradeMenu();
		}
	}
}
