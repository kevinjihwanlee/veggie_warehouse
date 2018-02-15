using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Order : MonoBehaviour {

    public Dictionary<string, int> order;
    public string ClientName;
    public bool Fulfilled;
    public int magnitude;
    public int value;
    public bool FulfillFail;
    public bool active;
    public Button stage;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);

        stage = gameObject.GetComponentInChildren<Button>();
        stage.onClick.AddListener(f);

        Fulfilled = false;
        FulfillFail = false;
        active = false;
    }

    public void initialize(Dictionary<string, int> o, string client, int val, int mag)
    {
        Start();
        active = true;
        ClientName = client;
        order = o;
        value = val;
        magnitude = mag;
        Component[] comps = this.gameObject.GetComponentsInChildren<Text>();
        foreach (Component c in comps)
        {
            if (c.name == "Client")
                ((Text)c).text = ClientName;
            else if (c.name == "OrderValue")
                ((Text)c).text = "$" + value.ToString();
            else if (c.name == "Corn")
                ((Text)c).text = "Corn: " + order["Corn"].ToString();
            else if (c.name == "Squash")
                ((Text)c).text = "Squash: " + order["Squash"].ToString();
            else if (c.name == "Beets")
                ((Text)c).text = "Beets: " + order["Beets"].ToString();
        }
    }

    void f()
    {
        if (!Fulfilled)
        {
            if (GameObject.FindObjectOfType<WarehouseManager>().StageOrder(this))
                Fulfilled = true;
            else
                FulfillFail = true;
        }
    }

    void Update()
    {
        //change the colors based on what happened when the user hit fulfill
        if (Fulfilled)
        {
            GetComponent<Image>().color = new Color32(21, 189, 12, 255);
        }
        else if (FulfillFail)
        {
            GetComponent<Image>().color = new Color32(231, 65, 85, 255);
        }
        else
            GetComponent<Image>().color = new Color32(83, 65, 36, 255);
    }
}
