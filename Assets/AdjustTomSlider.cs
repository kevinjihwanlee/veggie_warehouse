using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustTomSlider : MonoBehaviour
{

	public int ProjectedInventory;
	public int _maxStorage;
	private Slider _slider;

	public string Vegetable;
	
	// Use this for initialization
	void Start ()
	{
		_slider = GetComponent<Slider>();
	}

	public void UpdateSlider()
	{
		ProjectedInventory = FindObjectOfType<Panels>().ProjectedInventory[Vegetable];
		_maxStorage = FindObjectOfType<Supply>().MaxStorage;
		_slider.value = ProjectedInventory*100/ _maxStorage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

