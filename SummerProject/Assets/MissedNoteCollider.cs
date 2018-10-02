using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The cleanup tool for the dancebattle - deletes the note and reports that it has been missed
/// </summary>
public class MissedNoteCollider : MonoBehaviour {
    GameObject note;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
		//Activator.instance.MissedNote();
        Destroy(col.gameObject);//destroys the missed note
        CreateNotes.instance.MissedNote();//notifies the CreateNote class so the damage is adjusted
    }
}
