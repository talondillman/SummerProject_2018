using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CreateNotes : MonoBehaviour {
    public GameObject activator;
    public GameObject note1, note2, note3, note4, note5,note6, note7,note8;
    public GameObject[] notesList;
    private GameObject noteA, noteB, noteC,noteD, noteE,noteF, pattern1, pattern2, pattern3;
    private bool danceMode, noteMade, startDelay;
    private string move;
    private int setRound = 1;

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
        notesList[4] = note5;
        notesList[5] = note6;
        notesList[6] = note7;
        notesList[7] = note8;
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
                            Rand2Pattern();
                            noteA = SetNote(pattern1, 17);
                            noteB = SetNote(pattern2, 17.5f);
                            noteC = SetNote(pattern1, 19);
                            noteD = SetNote(pattern2, 19.5f);

                           // noteA.SetActive(true);
                           // noteB.SetActive(true);
                          //  noteC.SetActive(true);
                          //  noteD.SetActive(true);                      
                         
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
                            Rand2Pattern();
                            noteA = SetNote(pattern1, 17);
                            noteB = SetNote(pattern2, 18f);
                            noteC = SetNote(pattern2, 18.75f);                      

                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);

                            noteMade = true;
                        }
                        if ((noteC == null || noteC.transform.position.x < -10) && noteMade)
                        {
                            int damage = 4;
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

                            EnemyStats.stats.TakeDamage(damage);
                            Destroy(noteA);
                            Destroy(noteB);
                            Destroy(noteC);

                            activator.SetActive(false);
                            noteMade = false;
                            pickMove("");
                            BattleMenu.instance.switchDancePhase();
                        }
                        break;
                    }
                    //currently heals player, fix to raise defense later
                case "Flow":
                    {
                        activator.SetActive(true);
                        if (!noteMade)
                        {
                            Rand2Pattern();
                            noteA = SetNote(pattern1, 17);
                            noteB = SetNote(pattern2, 17.5f);
                            noteC = SetNote(pattern2, 18.25f);


                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);

                            noteMade = true;
                        }
                        if ((noteC == null || noteC.transform.position.x < -10) && noteMade)
                        {
                            int heal = 0;
                            if (noteA == null)
                            {
                                heal--;
                            }
                            if (noteB == null)
                            {
                                heal--;
                            }
                            if (noteC == null)
                            {
                                heal--;
                            }

                            PlayerStats.stats.TakeDamage(heal);
                            Destroy(noteA);
                            Destroy(noteB);
                            Destroy(noteC);

                            activator.SetActive(false);
                            noteMade = false;
                            pickMove("");
                            BattleMenu.instance.switchDancePhase();
                        }
                        break;

                    }
                case "Charleston":
                    {
                        //two sets of triplet notes
                        activator.SetActive(true);
                        int damage = 0;
                        if (!noteMade)
                        {
                            Rand2Pattern();
                            noteA = SetNote(pattern1, 19f);
                            noteB = SetNote(pattern1, 19.33f);
                            noteC = SetNote(pattern1, 19.66f);
                            noteD = SetNote(pattern2, 16f);
                            noteE = SetNote(pattern2, 16.33f);
                            noteF = SetNote(pattern2, 16.66f);

                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);
                            noteD.SetActive(true);
                            noteE.SetActive(true);
                            noteF.SetActive(true);

                            noteMade = true;
                        }
                        if ((noteC == null || noteC.transform.position.x < -10) && noteMade)
                        {
                            if (noteA == null && noteB == null && noteC == null)
                            {
                                damage++;
                            }
                            if (noteD == null && noteE == null && noteF == null)
                            {
                                damage++;
                            }
                            Destroy(noteA);
                            Destroy(noteB);
                            Destroy(noteC);
                            Destroy(noteD);
                            Destroy(noteE);
                            Destroy(noteF);



                            EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies

                           

                                activator.SetActive(false);
                                noteMade = false;
                                pickMove("");
                                BattleMenu.instance.switchDancePhase();
                            
                        }
                        
                        break;
                    }
                case "Kick":
                    {
                        activator.SetActive(true);
                        int damage = 1;
                        if (!noteMade)
                        {
                            Rand3Pattern();
                            noteA = SetNote(pattern1, 15f);
                            noteB = SetNote(pattern2, 20f);
                            noteC = SetNote(pattern1, 20.5f);
                            noteD = SetNote(pattern3, 24.5f);
                            noteE = SetNote(pattern1, 25.75f);
                            noteF = SetNote(pattern2, 26.25f);

                            noteA.SetActive(true);
                            noteB.SetActive(true);
                            noteC.SetActive(true);
                            noteD.SetActive(true);
                            noteE.SetActive(true);
                            noteF.SetActive(true);

                            noteMade = true;
                        }
                        if ((noteF == null || noteF.transform.position.x < -10) && noteMade)
                        {
                            if (noteA == null)
                            {
                                damage++;
                            }
                            if (noteB == null && noteC == null)
                            {
                                damage++;
                            }
                            if (noteD == null && noteE == null && noteF == null)
                            {
                                damage++;
                            }
                            Destroy(noteA);
                            Destroy(noteB);
                            Destroy(noteC);
                            Destroy(noteD);
                            Destroy(noteE);
                            Destroy(noteF);

                            EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies

                            activator.SetActive(false);
                            noteMade = false;
                            pickMove("");
                            BattleMenu.instance.switchDancePhase();

                        }
                        break;
                    }
                case "Wild Jive":
                    {
                        
                         activator.SetActive(true);
                        int damage = 1;
                        bool startDelay = false;
                        Debug.Log("Note made is " + noteMade + "  / round " + setRound);
                        if (!noteMade)
                        {

                            while (setRound <= 5)
                            {
                                Rand2Pattern();

                                SetNote(pattern1, 15f + setRound * 10);
                                SetNote(pattern1, 15.5f + setRound * 10);
                                noteA = SetNote(pattern2, 16f + setRound * 10);
                                //TO FIX: damage enemy based on noteA pressed; enemy should have note mapped to them at start?
                                EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies
                                setRound++;
                            }
                            if (setRound == 6)
                            {
                                if((noteA == null || noteA.transform.position.x < -10))
                                {
                                    noteMade = true;
                                }
                            
                            }
                        }
                        if (noteMade)
                        {
                            GameObject[] notes1 = GameObject.FindGameObjectsWithTag("Note 1");
                            GameObject[] notes2 = GameObject.FindGameObjectsWithTag("Note 2");
                            GameObject[] notes3 = GameObject.FindGameObjectsWithTag("Note 3");
                            GameObject[] notes4 = GameObject.FindGameObjectsWithTag("Note 4");
                            GameObject[] notes5 = GameObject.FindGameObjectsWithTag("Note 5");
                            GameObject[] notes6 = GameObject.FindGameObjectsWithTag("Note 6");
                            GameObject[] notes7 = GameObject.FindGameObjectsWithTag("Note 7");
                            GameObject[] notes8 = GameObject.FindGameObjectsWithTag("Note 8");

                            for (var i = 0; i < notes1.Length; i++)
                            {
                                Destroy(notes1[i]);
                            }
                            for (var i = 0; i < notes2.Length; i++)
                            {
                                Destroy(notes2[i]);
                            }
                            for (var i = 0; i < notes3.Length; i++)
                            {
                                Destroy(notes3[i]);
                            }
                            for (var i = 0; i < notes4.Length; i++)
                            {
                                Destroy(notes4[i]);
                            }
                            for (var i = 0; i < notes5.Length; i++)
                            {
                                Destroy(notes5[i]);
                            }
                            for (var i = 0; i < notes6.Length; i++)
                            {
                                Destroy(notes6[i]);
                            }
                            for (var i = 0; i < notes7.Length; i++)
                            {
                                Destroy(notes7[i]);
                            }
                            for (var i = 0; i < notes8.Length; i++)
                            {
                                Destroy(notes8[i]);
                            }


                            activator.SetActive(false);
                            noteMade = false;
                            pickMove("");
                            BattleMenu.instance.switchDancePhase();

                        }
                        break;
                    }
                case "B-Boy"://TO FIX: KEEPS ON CRASHING
                    {
                        activator.SetActive(true);
                        int damage = 1;
                        Debug.Log("Note made is " + noteMade + "  / round " + setRound);
                        if (!noteMade)
                        {

                            while (setRound <= 10)
                            {
                                Rand2Pattern();
                             
                                if (noteA == null && noteB == null)
                                {
                                    noteA = SetNote(pattern1, 15f);
                                    noteB = SetNote(pattern2, 15.25f);
                                }
                                //IF two notes are pressed, continue loop, otherwise stop
                                //note exists and offscreen means miss
                                if((noteA !=null && (noteA.transform.position.x < -10)) || (noteB!=null && (noteB.transform.position.x < -10)))
                                {
                                    Debug.Log("combo failed");
                                    activator.SetActive(false);
                                    Destroy(noteA);
                                    Destroy(noteB);
                                    pickMove("");
                                    BattleMenu.instance.switchDancePhase();
                                }
                                else
                                {
                                    Debug.Log("combo hit!");
                                    EnemyStats.stats.TakeDamage(damage);//TO FIX: damage random enemies
                                    ++setRound;
                                }
                            }
                            if (setRound == 10)
                            {
                                if ((noteB == null || noteB.transform.position.x < -10))
                                {
                                    noteMade = true;
                                }

                            }
                        }
                        if (noteMade)
                        {
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
    private void Rand2Pattern()
    {
        pattern1 = notesList[Random.Range(0, notesList.Length - 1)];
        pattern2 = notesList[Random.Range(0, notesList.Length - 1)];
        while (pattern1.Equals(pattern2))
        {
            pattern2 = notesList[Random.Range(0, notesList.Length - 1)];
        }
    }
    private void Rand3Pattern()
    {
        pattern1 = notesList[Random.Range(0, notesList.Length - 1)];
        pattern2 = notesList[Random.Range(0, notesList.Length - 1)];
        pattern3 = notesList[Random.Range(0, notesList.Length - 1)];

        while (pattern1.Equals(pattern2))
        {
            pattern2 = notesList[Random.Range(0, notesList.Length - 1)];
        }
        while (pattern3.Equals(pattern2)|| pattern3.Equals(pattern1))
        {
            pattern3 = notesList[Random.Range(0, notesList.Length - 1)];
        }
    }

    private GameObject SetNote(GameObject type, float x)
    {
        return Instantiate(type, new Vector3(x, 0, 0), Quaternion.identity);
    }
    IEnumerator DelayNoteMade()
    {
        yield return new WaitForSeconds(0.01f);
        noteMade = true;

    }
}
