using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour {
    public GameObject activator;
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;

    private bool doAction;
    private bool noteMade;

    // Use this for initialization
    void Start () {
        activator.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (doAction)
        {
            Debug.Log("hello");
            activator.SetActive(true);
            if (!noteMade)
            {
                GameObject note = Instantiate(note1);
                note.SetActive(true);
                noteMade = true;
            }
        }
        else
        {
            activator.SetActive(false);

        }
    }
    public void ChooseAction(string name)
    {
        switch (name)
        {
            case "2-4 Step":
                {
                    doAction = true;
                    Debug.Log(doAction);
                    Debug.Log("Move used.");
                    break;
                }
            case "Pop n Lock":
                {
                    doAction = true;
                    break;
                }
        }    }

}
