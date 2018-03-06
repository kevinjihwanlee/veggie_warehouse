using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSlider : MonoBehaviour
{

	private int _inventory;
	private int _maxStorage;
	
	// Use this for initialization
	void Start ()
	{
		_maxStorage = FindObjectOfType<Supply>().MaxStorage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
