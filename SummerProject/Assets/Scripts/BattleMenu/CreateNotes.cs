using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNotes : MonoBehaviour {
    public GameObject activator;
    public GameObject note1;
    private bool danceMode;
    private bool noteMade;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (danceMode)
        {
            activator.SetActive(true);
            if (!noteMade)
            {
               GameObject note= Instantiate(note1);
                note.SetActive(true);
                noteMade = true;
            }
        }
        else
        {
            activator.SetActive(false);
            note1.SetActive(false);
        }
	}
    public void danceToggle()
    {
        danceMode = !danceMode;
    }

}
