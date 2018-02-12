using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panels : MonoBehaviour {

	// Use this for initialization
    void Start () {
        Dictionary<string,int> storage = GameObject.Find("Storage").GetComponent<Supply>().StoredItems;

        //RectTransform o = GameObject.Find("Left").GetComponent<RectTransform>();
        //Vector3 pos = o.localPosition;
        //pos.x = -55;
        //o.localPosition = pos;

        RectTransform o = GameObject.Find("Top").GetComponent<RectTransform>();
        //pos = o.localPosition;
        //pos.y = 35;
        //o.localPosition = pos;
        Button b = o.gameObject.GetComponentInChildren<Button>();
        b.onClick.AddListener(GameObject.FindObjectOfType<WarehouseManager>().NextDay);

        UpdateDay();
        UpdateMoney();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateSupply()
    {
        Dictionary<string, int> StoredItems = GameObject.FindObjectOfType<Supply>().StoredItems;
        GameObject g = GameObject.Find("Corn Inventory");
        g.GetComponent<Text>().text = "Corn: " + StoredItems["Corn"].ToString();

        g = GameObject.Find("Squash Inventory");
        g.GetComponent<Text>().text = "Squash: " + StoredItems["Squash"].ToString();

        g = GameObject.Find("Beets Inventory");
        g.GetComponent<Text>().text = "Beets: " + StoredItems["Beets"].ToString();
    }

    public void UpdateMoney()
    {
        GameObject g = GameObject.Find("Money");
        g.GetComponent<Text>().text = "Money: $" + GameObject.FindObjectOfType<WarehouseManager>().Money.ToString();
    }

    public void UpdateDay()
    {
        GameObject g = GameObject.Find("Day");
        g.GetComponent<Text>().text = "Day: " + GameObject.FindObjectOfType<WarehouseManager>().Day.ToString();
    }
}
