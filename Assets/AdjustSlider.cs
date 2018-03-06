using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSlider : MonoBehaviour
{

	private Dictionary<string, int> _totalInventory;
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
		_slider = GetComponent<Slider>();
		_maxStorage = FindObjectOfType<Supply>().MaxStorage;
		_totalInventory = FindObjectOfType<Supply>().StoredItems;
		Debug.Log(_totalInventory[Vegetable]);
		
		_slider.value = _totalInventory[Vegetable]*100/ _maxStorage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
