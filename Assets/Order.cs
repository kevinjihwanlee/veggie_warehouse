using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Order : MonoBehaviour {

    public Dictionary<string, int> order;
    public string ClientName;
    public bool Fulfilled;
    public int value;
    public bool FulfillFail;
    public bool active;
    public Button stage;
    private float time;
    public int daysRemaining;

    // Use this for initialization
    void Start()
    {
        GetComponent<Transform>().localScale = new Vector3(0, 0, 0);

        stage = GetComponentInChildren<Button>();
        stage.onClick.AddListener(f);

        Fulfilled = false;
        FulfillFail = false;
        active = false;
        time = Time.time;
        
    }

    public void initialize(Dictionary<string, int> o, string client, int val, int days, Dictionary<string,int> prices)
    {
        Start();
        daysRemaining = days;
        active = true;
        ClientName = client;
        order = o;
        value = val;
        Component[] comps = gameObject.GetComponentsInChildren<Text>();
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
            else if (c.name == "CornP")
                ((Text)c).text = "$" + prices["Corn"].ToString() + " ea";
            else if (c.name == "SquashP")
                ((Text)c).text = "$" + prices["Squash"].ToString() + " ea";
            else if (c.name == "BeetsP")
                ((Text)c).text = "$" + prices["Beets"].ToString() + " ea";
        }
        setDay();
    }

    void f()
    {
        bool t = Time.time - time > .1f;
        if (!Fulfilled && t)
        {
            if (GameObject.FindObjectOfType<WarehouseManager>().StageOrder(this))
            {
                GameObject.Find("GoodSound").GetComponent<AudioSource>().Play();
                Fulfilled = true;
                FulfillFail = false;
            }
            else if (FulfillFail)
                FulfillFail = false;
            else
            {
                GameObject.Find("BadSound").GetComponent<AudioSource>().Play();
                FulfillFail = true;
            }
            time = UnityEngine.Time.time;
        }
        else if (t)
        {
            GameObject.FindObjectOfType<WarehouseManager>().UnstageOrder(this);
            Fulfilled = false;
            time = Time.time;
        }
    }

    public void decrementDays()
    {
        daysRemaining -= 1;
        setDay();
    }

    public void setDay()
    {
        string prestring = "Stage Today!";
        if (daysRemaining > 0)
            prestring = "Days Left: " + daysRemaining.ToString();
        foreach (Text t in GetComponentsInChildren<Text>())
        {
            if (t.name == "DaysRemaining")
                t.text = prestring;
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
        {
            GetComponent<Image>().color = new Color32(83, 65, 36, 255);
        }
    }

}
