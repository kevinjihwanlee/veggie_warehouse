using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panels : MonoBehaviour {

    public Button _nextDayButton;

    // Use this for initialization
    void Start()
    {
        //adding button listener to Next Day Button
        _nextDayButton = GameObject.Find("Top").GetComponentInChildren<Button>();
        _nextDayButton.onClick.AddListener(GameObject.FindObjectOfType<WarehouseManager>().NextDay);

        Camera cam = GameObject.FindObjectOfType<Camera>();
        Vector3 left = new Vector3(225, cam.pixelHeight / 2, 0);
        Vector3 top = new Vector3(cam.pixelWidth / 2 + 150, cam.pixelHeight + 300, 0);

        GameObject.Find("Left").GetComponent<Transform>().position = cam.WorldToScreenPoint(left);
        GameObject.Find("Top").GetComponent<Transform>().position = cam.WorldToScreenPoint(top);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateSupply()
    {
        //This function updates the counts that are shown for the users current supply
        Dictionary<string, int> StoredItems = GameObject.FindObjectOfType<Supply>().StoredItems;
        GameObject g = GameObject.Find("Corn Inventory");
        g.GetComponent<Text>().text = "Corn: " + StoredItems["Corn"].ToString();

        g = GameObject.Find("Squash Inventory");
        g.GetComponent<Text>().text = "Squash: " + StoredItems["Squash"].ToString();

        g = GameObject.Find("Beets Inventory");
        g.GetComponent<Text>().text = "Beets: " + StoredItems["Beets"].ToString();
    }

    public void UpdateOrdered()
    {
        //This function updates the counts that are shown for the users current supply
        Dictionary<string, int> OrderedItems = GameObject.FindObjectOfType<Supply>().OrderedItems;
        GameObject g = GameObject.Find("Ordered Corn");
        g.GetComponent<Text>().text = "Corn: " + OrderedItems["Corn"].ToString();

        g = GameObject.Find("Ordered Squash");
        g.GetComponent<Text>().text = "Squash: " + OrderedItems["Squash"].ToString();

        g = GameObject.Find("Ordered Beets");
        g.GetComponent<Text>().text = "Beets: " + OrderedItems["Beets"].ToString();
    }

    public void UpdateMoney()
    {
        //This function updates the current money that the player has
        GameObject g = GameObject.Find("Money");
        g.GetComponent<Text>().text = "Money: $" + GameObject.FindObjectOfType<WarehouseManager>().Money.ToString();
    }

    public void UpdateDay()
    {
        //This function updates the current Day that the player is on
        GameObject g = GameObject.Find("Day");
        g.GetComponent<Text>().text = "Day: " + GameObject.FindObjectOfType<WarehouseManager>().Day.ToString();
    }
}
