using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSequence : MonoBehaviour {

    public int SequenceNumber = 0;
    private int CurrentNumber = 0;
    
    // Use this for initialization
	void Start () {
        if (SequenceNumber != 0)
            gameObject.SetActive(false);

        Invoke("ProgressSequence", 0.3871f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ProgressSequence()
    {
        CurrentNumber++;
        if (CurrentNumber == 4)
            CurrentNumber = 0;

        if (SequenceNumber != CurrentNumber)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

        Invoke("ProgressSequence", 0.3871f);
    }
}
