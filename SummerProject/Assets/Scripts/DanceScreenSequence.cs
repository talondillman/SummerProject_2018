using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceScreenSequence : MonoBehaviour {

    public Texture[] screens;
    public Texture[] floors;
    private int floorSequence = 0;
    MeshRenderer mRender;

	// Use this for initialization
	void Start () {
        mRender = GetComponent<MeshRenderer>();

        Invoke("ChangeScreen", 0.3871f);
        Invoke("ChangeFloor", 0.3871f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeScreen()
    {
        Material[] mats;
        mats = mRender.materials;
        mats[3].SetTexture("_MainTex", screens[Random.Range(0, screens.Length)]);
        mRender.materials = mats;

        Invoke("ChangeScreen", 0.3871f);
    }

    void ChangeFloor()
    {
        Material[] mats;
        mats = mRender.materials;
        floorSequence++;
        if (floorSequence == floors.Length)
            floorSequence = 0;
        mats[1].SetTexture("_MainTex", floors[floorSequence]);
        mRender.materials = mats;

        Invoke("ChangeFloor", 0.3871f);
    }
}
