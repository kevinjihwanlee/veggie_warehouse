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
    private float time;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);

        stage = gameObject.GetComponentInChildren<Button>();
        stage.onClick.AddListener(f);

        Fulfilled = false;
        FulfillFail = false;
        active = false;
        time = UnityEngine.Time.time;
        
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
        bool t = UnityEngine.Time.time - time > .1f;
        if (!Fulfilled && t)
        {
            if (GameObject.FindObjectOfType<WarehouseManager>().StageOrder(this))
            {
                Fulfilled = true;
                FulfillFail = false;
            }
            else
                FulfillFail = true;
            time = UnityEngine.Time.time;
        }
        else if (t)
        {
            GameObject.FindObjectOfType<WarehouseManager>().UnstageOrder(this);
            Fulfilled = false;
            time = UnityEngine.Time.time;
        }
    }

    void Update()
    {
        //change the colors based on what happened when the user hit fulfill
        if (Fulfilled)
        {
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Unstage Order";
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().transform.localScale = new Vector3(.8f, .8f, .8f);
            GetComponent<Image>().color = new Color32(21, 189, 12, 255);
        }
        else if (FulfillFail)
        {
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Stage Order";
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Image>().color = new Color32(231, 65, 85, 255);
        }
        else
        {
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Stage Order";
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Image>().color = new Color32(83, 65, 36, 255);
        }
    }

}
