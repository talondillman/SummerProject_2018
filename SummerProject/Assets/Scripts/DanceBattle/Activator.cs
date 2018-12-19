using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Activator is the hitbox for the dance battle. When a key is pressed, it will check if the key corresponds to a key mapped to the note, and if the note is currently in the activator.
/// </summary>
public class Activator : MonoBehaviour
{
	SpriteRenderer sr;
	/// <summary>
	/// Checks that a note is in the activator
	/// </summary>
	bool pressableNote = false;
	/// <summary>
	/// A queue is made to keep track of what notes are entering and leaving
	/// </summary>
	Queue<GameObject> notes = new Queue<GameObject>();
	/// <summary>
	/// The original color of the activator
	/// </summary>
	Color old;
	public static Activator instance;
	/// <summary>
	/// Checks if the note has been destroyed inside the activator
	/// </summary>
	bool noteHit = false;
	


	// Use this for initialization
	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		old = sr.color;
		instance = this;
	}
    private bool checkNoteInput(string note)
    {
        if (notes.Count == 0)//don't check note tag if there hasn't been a note added to a queue
        {
            return Input.GetButtonDown(note);
        }
        return Input.GetButtonDown(note) && notes.Peek().tag == note;
    }
	// Update is called once per frame
	void Update()
	{
		if (!this.isActiveAndEnabled)
		{
			sr.color = new Color(255, 255, 255);
		}

        bool input1 = checkNoteInput("Note 1");
		bool input2 = checkNoteInput("Note 2");
		bool input3 = checkNoteInput("Note 3");
		bool input4 = checkNoteInput("Note 4");
		bool input5 = checkNoteInput("Note 5");
		bool input6 = checkNoteInput("Note 6");
		bool input7 = checkNoteInput("Note 7");
        bool input8 = checkNoteInput("Note 8");

		//if one of the input keys is pressed but there's no note in range, flash the activator but do nothing
		if (!pressableNote && (input1 || input2 || input3 || input4 || input5 || input6 || input7 || input8))
		{
			StartCoroutine(Pressed());

		}
		if (notes.Count != 0)//queue is not empty
		{
			//button pressed should correspond with type of note
			if (pressableNote && (input1 || input2 || input3 || input4 || input5 || input6 || input7 || input8))
            {
				CreateNotes.instance.hitNotes.Add(notes.Peek().tag);
				Destroy(notes.Dequeue());
				StartCoroutine(Pressed());
				pressableNote = false;
				noteHit = true;
			}
		}
	}

	/// <summary>
	/// When a note enters the hitbox it should be added to the queue. It has not been hit yet and is still able to be pressed.
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerEnter2D(Collider2D col)
	{
		noteHit = false;
		pressableNote = true;
		string tag = col.gameObject.tag;
		//Debug.Log("Note Collision: " + tag);

		notes.Enqueue(col.gameObject);
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		pressableNote = false;//note has been missed
		if (!noteHit && notes.Count != 0)
			notes.Dequeue();//if the note hasn't been hit then dequeue it. It 
	}

	IEnumerator Pressed()
	{
		//change color of flash depending on button pressed?
		//Need to fix

		sr.color = new Color(0, 0, 0);
		yield return new WaitForSeconds(0.001f);
		sr.color = old;
	}
}

