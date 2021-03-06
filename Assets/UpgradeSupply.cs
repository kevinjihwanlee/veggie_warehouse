﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSupply : MonoBehaviour
{

	private int _maxStorage;
	private double _spoilRate;
	private int _count;
	private GameObject _manager;
	public int UpgradePrice;

	// Use this for initialization
	void Start()
	{
		_manager = GameObject.Find("Main Camera");
		GetComponent<Button>().onClick.AddListener(UpgradeStorage);
		_count = 1;
		UpgradePrice = _count * 250;
		GetComponentInChildren<Text>().text = "Upgrade Storage: ($" + UpgradePrice + ")";
	}

	void Update()
	{
		if (_manager.GetComponent<WarehouseManager>().Money < UpgradePrice)
		{
			GetComponent<Button>().interactable = false;
		}
		else
		{
			GetComponent<Button>().interactable = true;
		}
	}

	void UpgradeStorage()
	{
        FindObjectOfType<WarehouseManager>().Buy(UpgradePrice);
		_count++;
		UpgradePrice = _count * 250;
		GetComponentInChildren<Text>().text = "Upgrade Storage: ($" + UpgradePrice + ")";
		FindObjectOfType<Supply>().UpgradeStorage(_count * 10);
        FindObjectOfType<Supply>().ReduceSpoilRate(1.25);
        FindObjectOfType<Panels>().UpdateProjected();
	}
}
