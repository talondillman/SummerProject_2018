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

    public static CreateNotes instance;
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// When an action button is pressed, the dance rhythm game is activated and the proper notes appear
    /// A button must be equipped with the method pickMove to update this!
    /// </summary>
    void Update () {
        if (danceMode)
        {
            //move determined by pickMove()
            switch (move)
            {
                case "2-4 Step":
                    {
                        //hitbox on
                        activator.SetActive(true);

                        //make notes only once
                        if (!noteMade)
                        {
                             noteA = Instantiate(note2, new Vector3(17,0,0), Quaternion.identity);
                             noteB = Instantiate(note4, new Vector3(18, 0, 0), Quaternion.identity);
                             noteC = Instantiate(note2, new Vector3(20, 0, 0), Quaternion.identity);
                             noteD = Instantiate(note4, new Vector3(21, 0, 0), Quaternion.identity);

                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);
                            noteD.SetActive(true);                      
                         
                            noteMade = true;
                            
                        }
                        //when last note is hit or goes offscreen, destroy the notes and end turn
                        if ((noteD==null || noteD.transform.position.x < -10) && noteMade)
                        {
                            Destroy(noteA);
                            Destroy(noteB);
                            Destroy(noteC);
                            Destroy(noteD);

                            activator.SetActive(false);
                            pickMove("");//dance mode off, turn off activator 
                            noteMade = false;

                            //dance phase over, BattleMenu switches to next state
                            BattleMenu.instance.switchDancePhase();
                                                   }
                        break;
                    }

                case "Pop Lock":
                    {
                        activator.SetActive(true);
                        if (!noteMade)
                        {
                            noteA = Instantiate(note1, new Vector3(17, 0, 0), Quaternion.identity);
                            noteB = Instantiate(note3, new Vector3(19, 0, 0), Quaternion.identity);
                            noteC = Instantiate(note3, new Vector3(20, 0, 0), Quaternion.identity);
                            noteD = Instantiate(note1, new Vector3(24, 0, 0), Quaternion.identity);

                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);
                            noteD.SetActive(true);

                            noteMade = true;
                        }
                        if ((noteD == null || noteD.transform.position.x < -10) && noteMade)
                        {
                            Destroy(noteA);
                            Destroy(noteB);
                            Destroy(noteC);
                            Destroy(noteD);

                            activator.SetActive(false);
                            noteMade = false;
                            pickMove("");
                            BattleMenu.instance.switchDancePhase();
                        }
                        break;
                    }

                case "Foe Attack":
                    {
                        int rand = Random.Range(1,10);
                        PlayerStats.stats.TakeDamage(rand);
                        //deals rand damage to player
                        pickMove("");
                        BattleMenu.instance.switchDancePhase();

                        break;
                    }
            }
        }
        else
        {
            activator.SetActive(false);
        }
	}
    /// <summary>
    /// Used to change the switch statement in Update
    /// </summary>
    /// <param name="moveName"></param>
    public void pickMove(string moveName)
    {
        danceMode = !danceMode;
        move = moveName;
    }

}
