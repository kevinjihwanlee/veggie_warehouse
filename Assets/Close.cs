using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Close : MonoBehaviour
{

	private Button _exitButton;
	// Use this for initialization
	void Start ()
	{
		_exitButton = GetComponent<Button>();
		_exitButton.onClick.AddListener(CloseMenu);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CloseMenu()
	{
		var parent = _exitButton.transform.parent;
		parent.localScale = new Vector3(0, 0, 0);
	}
	
}
