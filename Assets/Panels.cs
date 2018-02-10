using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : MonoBehaviour {

	// Use this for initialization
    void Start () {
        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform o in children)
        {
            if (o.name == "Left")
            {
                Vector3 pos = o.localPosition;
                pos.x = -55;
                o.localPosition = pos;
            }
            if (o.name == "Top")
            {
                Vector3 pos = o.localPosition;
                pos.y = 35;
                o.localPosition = pos;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
