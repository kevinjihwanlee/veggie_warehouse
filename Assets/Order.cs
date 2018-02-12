using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour {

    public Dictionary<string, int> order;
    public string ClientName;
    public bool Fulfilled;
    public int magnitude;
    public bool FulfillFail;

    // Use this for initialization
    void Start()
    {

        Button B = this.GetComponentInChildren<Button>();
        B.onClick.AddListener(f);

        Fulfilled = false;
        FulfillFail = false;
    }

    void f()
    {
        if (!Fulfilled && GameObject.FindObjectOfType<WarehouseManager>().StageOrder(this))
        {
            Fulfilled = true;
        }
    }

    public void initialize(Dictionary<string,int> o, string client)
    {
        ClientName = client;
        order = o;
        magnitude = order["Corn"] + order["Squash"] + order["Beets"];
        Component[] comps = this.gameObject.GetComponentsInChildren<Text>();
        foreach (Component c in comps)
        {
            if (c.name == "Client")
                ((Text)c).text = ClientName;
            else if (c.name == "Corn")
                ((Text)c).text = "Corn: " + order["Corn"].ToString();
            else if (c.name == "Squash")
                ((Text)c).text = "Squash: " + order["Squash"].ToString();
            else if (c.name == "Beets")
                ((Text)c).text = "Beets: " + order["Beets"].ToString();
        }
    }
    void Update()
    {
        //change the colors based on what happened when the user hit fulfill
        if (Fulfilled)
        {
            GetComponent<Image>().color = new Color32(21, 189, 12, 255);
        }
        if (FulfillFail)
        {
            GetComponent<Image>().color = new Color32(231, 65, 85, 255);
        }
    }
}
