using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGui : MonoBehaviour
{
    public GameObject p1Menu;
    private bool isShowing;
    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            p1Menu.SetActive(isShowing);
        }
    }
}
