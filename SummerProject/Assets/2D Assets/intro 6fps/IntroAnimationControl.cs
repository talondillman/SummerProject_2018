using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroAnimationControl : MonoBehaviour {

    bool introDone = false;
    public GameObject credits;
    bool creditsOn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (introDone && !creditsOn)
        {
            if (Input.GetKeyDown("q"))
            {
                credits.SetActive(true);
                creditsOn = true;
            }
            else if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene("Whitebox1_Jarom");
            }
            else if (Input.GetKeyDown("escape"))
            {
                Application.Quit();
                Debug.Log("Application Should Have Quit");
            }
        }
        else if (introDone && creditsOn)
        {
            if (Input.anyKeyDown)
            {
                credits.SetActive(false);
                creditsOn = false;
            }
        }
	}

    void turnOff()
    {
        Debug.Log("turnOff");
        introDone = true;
    }

    
}
