using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour {

    public Dictionary<string, int> order;
    public List<string> companies;
    public string ClientName;
    public bool changeflag;

    // Use this for initialization
    void Start()
    {
        order = new Dictionary<string, int>();
        order["corn"] = (int)(UnityEngine.Random.value * 101) + 0;
        order["squash"] = (int)(UnityEngine.Random.value * 101) + 0;
        order["beets"] = (int)(UnityEngine.Random.value * 101) + 0;
        companies = new List<string> {"Whole Jewels", "Trader Bills", "Food Osco", "Bullseye", "Floormart", "DangerWay"};
        int index = (int) (Random.value * companies.Count);
        ClientName = companies[index];
        changeflag = true;
    }

    public void set(int corn, int squash, int beets)
    {
        order["corn"] = corn;
        order["squash"] = squash;
        order["beets"] = beets;
        changeflag = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (changeflag){
            Component[] comps = this.gameObject.GetComponentsInChildren<Text>();
            foreach(Component c in comps){
                if (c.name == "Client")
                    ((Text)c).text = ClientName;
                else if (c.name == "Corn")
                    ((Text)c).text = "Corn: " + order["corn"].ToString();
                else if (c.name == "Squash")
                    ((Text)c).text = "Squash: " + order["squash"].ToString();
                else if (c.name == "Beets")
                    ((Text)c).text = "Beets: " + order["beets"].ToString();
            }
            changeflag = false;
        }
		
	}
}
