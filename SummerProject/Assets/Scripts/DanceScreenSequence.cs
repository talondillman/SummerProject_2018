using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceScreenSequence : MonoBehaviour {

    public Texture[] screens;
    MeshRenderer mRender;

	// Use this for initialization
	void Start () {
        mRender = GetComponent<MeshRenderer>();

        Invoke("ChangeScreen", 0.3871f);
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
}
