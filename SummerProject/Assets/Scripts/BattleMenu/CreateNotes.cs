using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNotes : MonoBehaviour {
    public GameObject activator;
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;
    public GameObject[] notesList;
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
    public void Start()
    {
        notesList = new GameObject[8];
        notesList[0] = note1;
        notesList[1] = note2;
        notesList[2] = note3;
        notesList[3] = note4;
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
                            GameObject pattern1 = notesList[Random.Range(0, notesList.Length-1)];
                            GameObject pattern2 = notesList[Random.Range(0, notesList.Length-1)];
                            if (pattern1.Equals(pattern2))
                            {
                                Destroy(pattern2);
                                pattern2 = notesList[Random.Range(0, notesList.Length - 1)];
                            }
                             noteA = Instantiate(pattern1, new Vector3(17,0,0), Quaternion.identity);
                             noteB = Instantiate(pattern2, new Vector3(18, 0, 0), Quaternion.identity);
                             noteC = Instantiate(pattern1, new Vector3(20, 0, 0), Quaternion.identity);
                             noteD = Instantiate(pattern2, new Vector3(21, 0, 0), Quaternion.identity);

                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);
                            noteD.SetActive(true);                      
                         
                            noteMade = true;
                            
                        }
                        //when last note is hit or goes offscreen, destroy the notes and end turn
                        if ((noteD==null || noteD.transform.position.x < -10) && noteMade)
                        {
                            int damage = 5;
                            if (noteA != null)
                            {
                                damage--;
                            }
                            if (noteB != null)
                            {
                                damage--;
                            }
                            if (noteC != null)
                            {
                                damage--;
                            }
                            if (noteD != null)
                            {
                                damage--;
                            }
                            EnemyStats.stats.TakeDamage(damage);

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
                            GameObject pattern1 = notesList[Random.Range(0, notesList.Length - 1)];
                            GameObject pattern2 = notesList[Random.Range(0, notesList.Length - 1)];
                            while (pattern1.Equals(pattern2))
                            {
                                Destroy(pattern2);
                                pattern2 = notesList[Random.Range(0, notesList.Length - 1)];
                            }
                            noteA = Instantiate(pattern1, new Vector3(17, 0, 0), Quaternion.identity);
                            noteB = Instantiate(pattern2, new Vector3(19, 0, 0), Quaternion.identity);
                            noteC = Instantiate(pattern2, new Vector3(20, 0, 0), Quaternion.identity);
                            noteD = Instantiate(pattern1, new Vector3(24, 0, 0), Quaternion.identity);

                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);
                            noteD.SetActive(true);

                            noteMade = true;
                        }
                        if ((noteD == null || noteD.transform.position.x < -10) && noteMade)
                        {
                            int damage = 5;
                            if (noteA != null)
                            {
                                damage--;
                            }
                            if (noteB != null)
                            {
                                damage--;
                            }
                            if (noteC != null)
                            {
                                damage--;
                            }
                            if (noteD != null)
                            {
                                damage--;
                            }
                            EnemyStats.stats.TakeDamage(damage);
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
