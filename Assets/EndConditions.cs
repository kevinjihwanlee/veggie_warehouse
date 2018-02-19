using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndConditions : MonoBehaviour
{

	public int _endMoney;
	private Component _manager;
	
	// Use this for initialization
	void Start ()
	{
		_endMoney = GameObject.Find("Main Camera").GetComponent<WarehouseManager>().Money;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}