using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    // public KeyCode key;
    bool active = false;
    GameObject note;
    Color old;


    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        old = sr.color;

    }

    // Update is called once per frame
    void Update()
    {
        //button pressed, activator flashes
        if (Input.GetButtonDown("Note 1") || Input.GetButtonDown("Note 2") || Input.GetButtonDown("Note 3") || Input.GetButtonDown("Note 4"))
        {
            StartCoroutine(Pressed());
        }
        //button pressed should correspond with type of note
        if (Input.GetButtonDown("Note 1") && note.tag == "Note 1" && active)
        {
            //if button pressed at correct time, note is destroyed
            Destroy(note);
            StartCoroutine(Pressed());
        }

        if (Input.GetButtonDown("Note 2") && note.tag == "Note 2" && active)
        {
            //if button pressed at correct time, note is destroyed
            Destroy(note);
            StartCoroutine(Pressed());
        }
        if (Input.GetButtonDown("Note 3") && note.tag == "Note 3" && active)
        {
            //if button pressed at correct time, note is destroyed
            Destroy(note);
            StartCoroutine(Pressed());
        }
        if (Input.GetButtonDown("Note 4") && note.tag == "Note 4" && active)
        {
            //if button pressed at correct time, note is destroyed
            Destroy(note);
            StartCoroutine(Pressed());
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        active = true;
        string tag = col.gameObject.tag;
        if (tag == "Note 1" || tag == "Note 2" || tag == "Note 3" || tag == "Note 4")
        {
            note = col.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }
    IEnumerator Pressed()
    {
        //change color of flash depending on button pressed?
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.01f);
        sr.color = old;
    }

}

