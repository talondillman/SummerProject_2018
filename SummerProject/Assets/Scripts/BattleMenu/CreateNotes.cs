using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNotes : MonoBehaviour {
    public GameObject activator;
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;

    private GameObject noteA;
    private GameObject noteB;
    private GameObject noteC;
    private GameObject noteD;


    private bool danceMode;
    private bool noteMade;
    private string move;
    // Use this for initialization
    void Start () {
        //activator.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (danceMode)
        {
            switch (move)
            {
                case "2-4 Step":
                    {
                        activator.SetActive(true);
                        if (!noteMade)
                        {
                             noteA = Instantiate(note2, new Vector3(17,0,0), Quaternion.identity);
                             noteB = Instantiate(note4, new Vector3(18, 0, 0), Quaternion.identity);
                             noteC = Instantiate(note2, new Vector3(20, 0, 0), Quaternion.identity);
                             noteD = Instantiate(note4, new Vector3(23, 0, 0), Quaternion.identity);

                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);
                            noteD.SetActive(true);

                         
                            noteMade = true;
                            
                        }
                        if (noteD==null && noteMade)
                        {
                            Debug.Log("Switch to P2");
                            BattleMenu.instance.switchDancePhase();
                                                   }
                        break;
                    }
            }
        }
        else
        {
            activator.SetActive(false);
           // note1.SetActive(false);
        }
	}
    public void pickMove(string moveName)
    {
        danceMode = !danceMode;
        move = moveName;
    }

}
