using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The cleanup tool for the dancebattle - deletes the note and reports that it has been missed
/// </summary>
public class MissedNoteCollider : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D col)
	{
		//Activator.instance.MissedNote();
		CreateNotes.instance.MissedNote(col.gameObject.tag);//notifies the CreateNote class so the damage is adjusted
		Destroy(col.gameObject);//destroys the missed note
	}
}
