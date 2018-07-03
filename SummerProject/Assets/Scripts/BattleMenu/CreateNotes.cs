using UnityEngine;

public class CreateNotes : MonoBehaviour {
    public GameObject activator;
    public GameObject note1, note2, note3, note4, note5,note6, note7,note8;
    public GameObject[] notesList;
    private GameObject noteA, noteB, noteC,noteD, pattern1, pattern2;
    private bool danceMode, noteMade;
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
                        Rand2Pattern();

                        break;
                    }
                case "Kick":
                    {
                        Rand2Pattern();

                        break;
                    }
                case "Wild Jive":
                    {
                        Rand2Pattern();

                        break;
                    }
                case "B-Boy":
                    {
                        Rand2Pattern();
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
    private GameObject SetNote(GameObject type, float x)
    {
        return Instantiate(type, new Vector3(x, 0, 0), Quaternion.identity);
    }
}
