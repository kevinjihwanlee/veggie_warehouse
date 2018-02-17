using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panels : MonoBehaviour {

    public Button _nextDayButton;
    private int Day;
    public Order[] Orders;
    public int lastDay;

    // Use this for initialization
    void Start()
    {
        //adding button listener to Next Day Button
        _nextDayButton = GameObject.Find("Top").GetComponentInChildren<Button>();
        _nextDayButton.onClick.AddListener(GameObject.FindObjectOfType<WarehouseManager>().NextDay);

        Day = 0;

        Orders = GameObject.FindObjectsOfType<Order>();

        //Camera cam = GameObject.FindObjectOfType<Camera>();
        //Vector3 left = new Vector3(cam.pixelWidth * .1f, cam.pixelHeight * .5f, 0);
        //Vector3 top = new Vector3(cam.pixelWidth * .6f, cam.pixelHeight * .9f, 0);

        //GameObject.Find("Left").GetComponent<Transform>().position = cam.ScreenToWorldPoint(left);
        //GameObject.Find("Top").GetComponent<Transform>().position = cam.ScreenToWorldPoint(top);
    }
	
	// Update is called once per frame
	void Update () {
        if (Day > 0)
            _nextDayButton.GetComponentInChildren<Text>().text = "Go To Next Day";		
	}

    public void UpdateSupply()
    {
        //This function updates the counts that are shown for the users current supply
        Dictionary<string, int> StoredItems = GameObject.FindObjectOfType<Supply>().StoredItems;
        GameObject g = GameObject.Find("Corn Inventory");
        g.GetComponent<Text>().text = StoredItems["Corn"].ToString();

        g = GameObject.Find("Squash Inventory");
        g.GetComponent<Text>().text = StoredItems["Squash"].ToString();

        g = GameObject.Find("Beets Inventory");
        g.GetComponent<Text>().text = StoredItems["Beets"].ToString();
    }

    public void UpdateReceipt(int orderRev, int veggieCost)
    {
        GameObject.Find("Receipt").GetComponent<Text>().text = "Recap Day " + lastDay.ToString() + ":";
        GameObject.Find("OrderRevenueValue").GetComponentInChildren<Text>().text = "$" + orderRev.ToString();
        GameObject.Find("VeggieCostValue").GetComponentInChildren<Text>().text = "$" + (-veggieCost).ToString();
        GameObject.Find("ProfitValue").GetComponentInChildren<Text>().text = "$" + (orderRev - veggieCost).ToString();
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
        lastDay = Day;
        Day = GameObject.FindObjectOfType<WarehouseManager>().Day;
        g.GetComponent<Text>().text = "Day: " + Day.ToString();
    }
}
