using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panels : MonoBehaviour {

    public Button _nextDayButton;
    private int Day;
    public Order[] Orders;
    public int lastDay;
    private WarehouseManager _manager;

    public Dictionary<string, int> ProjectedInventory;

	
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
        UpdateSpoilRate();
        _manager = FindObjectOfType<WarehouseManager>();

        ProjectedInventory = new Dictionary<string, int>();
        
        var maxStorage = GameObject.FindObjectOfType<Supply>().MaxStorage;
        Dictionary<string, int> StoredItems = GameObject.FindObjectOfType<Supply>().StoredItems;
        Dictionary<string, int> StagedItems = GameObject.FindObjectOfType<Supply>().StagedItems;
        Dictionary<string, int> BoughtItems = GameObject.FindObjectOfType<BuyMenu2>()._totalOrder;
        double spoilRate = GameObject.FindObjectOfType<Supply>().spoilRate;
        
        GameObject g = GameObject.Find("Projected Corn");
        // first val is hardcoded
        ProjectedInventory["Corn"] = 16;
        g.GetComponent<Text>().text = ProjectedInventory["Corn"].ToString() + '/' + maxStorage.ToString();

        var squash = GameObject.Find("Squash Inventory");
        ProjectedInventory["Squash"] = 16;
        squash.GetComponent<Text>().text = ProjectedInventory["Squash"].ToString() + '/' + maxStorage.ToString();

        var beets = GameObject.Find("Beets Inventory");
        ProjectedInventory["Beets"] = 16;
        beets.GetComponent<Text>().text = ProjectedInventory["Beets"].ToString() + '/' + maxStorage.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (Day > 0)
            _nextDayButton.GetComponentInChildren<Text>().text = "Go To Next Day";		
	}

    public void UpdateSupply()
    {
        //This function updates the counts that are shown for the users current supply
        var maxStorage = GameObject.FindObjectOfType<Supply>().MaxStorage;
        Dictionary<string, int> StoredItems = GameObject.FindObjectOfType<Supply>().StoredItems;
        GameObject g = GameObject.Find("Corn Inventory");
        g.GetComponent<Text>().text = StoredItems["Corn"].ToString() + '/' + maxStorage;

        g = GameObject.Find("Squash Inventory");
        g.GetComponent<Text>().text = StoredItems["Squash"].ToString() + '/' + maxStorage;

        g = GameObject.Find("Beets Inventory");
        g.GetComponent<Text>().text = StoredItems["Beets"].ToString() + '/' + maxStorage;
    }



    public void UpdateProjected()
    {
        var maxStorage = GameObject.FindObjectOfType<Supply>().MaxStorage;
        //This function updates the counts that are shown for the users projected supply
        Dictionary<string, int> StoredItems = GameObject.FindObjectOfType<Supply>().StoredItems;
        Dictionary<string, int> StagedItems = GameObject.FindObjectOfType<Supply>().StagedItems;
        Dictionary<string, int> BoughtItems = GameObject.FindObjectOfType<BuyMenu2>()._totalOrder;
        double spoilRate = GameObject.FindObjectOfType<Supply>().spoilRate;
        GameObject g = GameObject.Find("Projected Corn");
        ProjectedInventory["Corn"] = (StoredItems["Corn"] - StagedItems["Corn"]) -
                                     Convert.ToInt32((StoredItems["Corn"] - StagedItems["Corn"]) * spoilRate) +
                                     BoughtItems["Corn"];
        g.GetComponent<Text>().text = ProjectedInventory["Corn"].ToString() + '/' + maxStorage.ToString();

        ProjectedInventory["Squash"] = (StoredItems["Squash"] - StagedItems["Squash"]) - Convert.ToInt32((StoredItems["Squash"] - StagedItems["Squash"]) * spoilRate) + BoughtItems["Squash"];
        g = GameObject.Find("Projected Squash");
        g.GetComponent<Text>().text = ProjectedInventory["Squash"].ToString() + '/' + maxStorage.ToString();

        ProjectedInventory["Beets"] = (StoredItems["Beets"] - StagedItems["Beets"]) - Convert.ToInt32((StoredItems["Beets"] - StagedItems["Beets"]) * spoilRate) + BoughtItems["Beets"];
        g = GameObject.Find("Projected Beets");
        g.GetComponent<Text>().text = ProjectedInventory["Beets"].ToString() + '/' + maxStorage.ToString();
	    
        _manager.UpdateAllSliders();
    }

    public void UpdateSpoilRate()
    {
        var spoilPercent = Math.Round(FindObjectOfType<Supply>().spoilRate * 100.0, 2);
        GameObject g = GameObject.Find("SpoilRate");
        g.GetComponent<Text>().text = spoilPercent.ToString() + '%';
    }

    public void UpdateBuySellPrices()
    {
        //This function updates the prices of the veggies in the panel
        Dictionary<string, int> SellPrices = GameObject.FindObjectOfType<WarehouseManager>().sellPrices;
        Dictionary<string, int> BuyPrices = GameObject.FindObjectOfType<WarehouseManager>().buyPrices;
        GameObject g = GameObject.Find("CornBPrice");
        g.GetComponent<Text>().text = "$" + BuyPrices["Corn"].ToString();
        g = GameObject.Find("SquashBPrice");
        g.GetComponent<Text>().text = "$" + BuyPrices["Squash"].ToString();
        g = GameObject.Find("BeetsBPrice");
        g.GetComponent<Text>().text = "$" + BuyPrices["Beets"].ToString();
        g = GameObject.Find("CornSPrice");
        g.GetComponent<Text>().text = "$" + SellPrices["Corn"].ToString();
        g = GameObject.Find("SquashSPrice");
        g.GetComponent<Text>().text = "$" + SellPrices["Squash"].ToString();
        g = GameObject.Find("BeetsSPrice");
        g.GetComponent<Text>().text = "$" + SellPrices["Beets"].ToString();
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

    public void UpdateLives()
    {
        //This function updates the current Day that the player is on
        GameObject g = GameObject.Find("Lives");
        int lives = 3 - FindObjectOfType<WarehouseManager>().failed;
        string extra = "XXX";
        if (lives == 2)
            extra = "XX";
        else if (lives == 1)
            extra = "X";
        else if (lives == 0)
            extra = "";
        else if (lives == 4)
            extra = "XXXX";
        g.GetComponent<Text>().text = "Lives: " + extra;
    }
}
