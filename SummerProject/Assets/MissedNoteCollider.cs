using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
       
        Destroy(col);
        CreateNotes.instance.MissedNote(col.tag);
    }
}
