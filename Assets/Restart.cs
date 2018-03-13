using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{

    private Button _startGame;
	
    // Use this for initialization
    void Start ()
    {
        _startGame = GameObject.Find("Restart").GetComponent<Button>();
        _startGame.onClick.AddListener(RestartGame);
        GameObject.Find("FailedOrder").GetComponent<AudioSource>().Play();
    }
	
    // Update is called once per frame
    void Update () {
		
	
    }

    void RestartGame()
    {
        SceneManager.LoadScene("warehouse_screen");
    }
}
