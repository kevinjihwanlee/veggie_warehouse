using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour {

    public Dictionary<string, int> order;
    public string ClientName;
    public bool Fulfilled;

    // Use this for initialization
    void Start()
    {

        Button B = this.GetComponentInChildren<Button>();
        B.onClick.AddListener(f);

        Fulfilled = false;
    }

    void f(){
        if (!Fulfilled && GameObject.FindObjectOfType<WarehouseManager>().FulfillOrder(this))
        {
            Fulfilled = true;
        }
    }

    public void set()
    {
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

    //private void OnMouseDown()
    //{
    //    bool fulfill = true;
    //    Supply s = GameObject.Find("Storage").GetComponent<Supply>();
    //    foreach(string a in order.Keys){
    //        if (order[a] < s.StoredItems[a])
    //            fulfill = false;
    //        Debug.Log(a);
    //    }
    //    if (fulfill)
    //        GameObject.FindObjectOfType<WarehouseManager>().FulfillOrder(order);
    //}

    // Update is called once per frame
    void Update()
    {
        if (Fulfilled){
            GetComponent<Image>().color = new Color32(21,189,12,255);
        }
    }
}
