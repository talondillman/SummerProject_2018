using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour {
    public bool basicAtk;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseUpAsButton()
    {
        if (basicAtk)
        {
            GUI.Button(new Rect(Screen.width / 2, 150, 100, 30), "Attack 1");
        }
    }
}
