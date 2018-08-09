using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateNotes : MonoBehaviour {
    public GameObject activator, count1, count2, count3, countDance;
    public GameObject note1, note2, note3, note4, note5, note6, note7, note8;
    public GameObject[] notesList;
    private GameObject noteA, noteB, noteC, noteD, noteE, noteF, noteG, noteH, noteI, noteJ, pattern1, pattern2, pattern3;
    private bool danceMode, noteMade, startDelay, missedNote, counted;
    private string move, noteType;
    private int setRound = 1;
    public float timeLeft = 3.0f;

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
        missedNote = false;
        counted = false;
    }

    /// <summary>
    /// When an action button is pressed, the dance rhythm game is activated and the proper notes appear
    /// A button must be equipped with the method pickMove to update this!
    /// </summary>
    void Update() {
        Debug.Log(counted);
        if (danceMode && !counted)
        {
            timeLeft -= Time.deltaTime;
            string count = (timeLeft).ToString("0");
            if (count.Equals("3"))
            {
                count3.SetActive(true);
            }
            else if (count.Equals("2"))
            {
                count3.SetActive(false);
                count2.SetActive(true);
            }
            else if (count.Equals("1"))
            {
                count2.SetActive(false);
                count1.SetActive(true);
            }
            else if (count.Equals("0"))
            {
                count1.SetActive(false);
                countDance.SetActive(true);
            }
            else if (timeLeft < 0)
            {
                countDance.SetActive(false);
                counted = true;

            }
        }
        else if (danceMode && counted)
        {

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
                                                        noteMade = true;
                        }
                        //when last note is hit or goes offscreen, destroy the notes and end turn
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
                            Destroy(noteA);
                            Destroy(noteB);
                            Destroy(noteC);
                            Destroy(noteD);

                            EnemyStats.stats.TakeDamage(damage);


                            activator.SetActive(false);
                            pickMove("");//dance mode off, turn off activator 
                            noteMade = false;
                            ResetCount();

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
                            ResetCount();

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
                            ResetCount();

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
                            noteA = SetNote(pattern1, 20f);
                            noteB = SetNote(pattern1, 20.5f);
                            noteC = SetNote(pattern1, 21f);
                            noteD = SetNote(pattern2, 16f);
                            noteE = SetNote(pattern2, 16.5f);
                            noteF = SetNote(pattern2, 17f);


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

                            if (BattleMenu.instance.currentState == BattleMenu.BattleTurns.P1)
                            {
                            PlayerStats.stats.useGP(2);
                            }
                            else
                            {
                                PlayerStats.stats.useGP2(2);
                            }
                            EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies



                            activator.SetActive(false);
                            noteMade = false;
                            pickMove("");
                            ResetCount();

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
                            if (BattleMenu.instance.currentState == BattleMenu.BattleTurns.P1)
                            {
                                PlayerStats.stats.useGP(3);
                            }
                            else
                            {
                                PlayerStats.stats.useGP2(3);
                            }

                            EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies

                            activator.SetActive(false);
                            noteMade = false;
                            pickMove("");
                            ResetCount();

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

                                SetNote(pattern1, 15f + setRound * 5);
                                SetNote(pattern1, 15.5f + setRound * 5);
                                noteA = SetNote(pattern2, 16f + setRound * 5);
                                //TO FIX: damage enemy based on noteA pressed; enemy should have note mapped to them at start?
                                EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies
                                setRound++;
                            }
                            if (setRound == 6)
                            {
                                if ((noteA == null || noteA.transform.position.x < -10))
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
                            ResetCount();
                            if (BattleMenu.instance.currentState == BattleMenu.BattleTurns.P1)
                            {
                                PlayerStats.stats.useGP(4);
                            }
                            else
                            {
                                PlayerStats.stats.useGP2(4);
                            }

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
                            Rand2Pattern();
                            noteA = SetNote(pattern1, 15f);
                            noteB = SetNote(pattern2, 15.55f);

                            Rand2Pattern();
                            noteC = SetNote(pattern1, 18f);
                            noteD = SetNote(pattern2, 18.55f);

                            Rand2Pattern();
                            noteE = SetNote(pattern1, 21f);
                            noteF = SetNote(pattern2, 21.55f);

                            Rand2Pattern();
                            noteG = SetNote(pattern1, 24f);
                            noteH = SetNote(pattern2, 24.55f);

                            Rand2Pattern();
                            noteI = SetNote(pattern1, 27f);
                            noteJ = SetNote(pattern2, 27.55f);

                            //IF two notes are pressed, continue loop, otherwise stop
                            //note exists and offscreen means miss

                            noteMade = true;
                            if (BattleMenu.instance.currentState == BattleMenu.BattleTurns.P1)
                            {
                                PlayerStats.stats.useGP(8);
                            }
                            else
                            {
                                PlayerStats.stats.useGP2(8);
                            }

                        }
                        if (noteMade)
                        {

                            if(noteA==null && noteB == null)
                            {
                                damage++;
                            }
                            if (noteC == null && noteD == null)
                            {
                                damage++;
                            }
                            if (noteE == null && noteF == null)
                            {
                                damage++;
                            }
                            if (noteG == null && noteH == null)
                            {
                                damage++;
                            }
                            if (noteI == null && noteJ == null)
                            {
                                damage++;
                            }
                            if (missedNote)
                            {
                                Debug.Log("combo failed");

                                Destroy(noteA);
                                Destroy(noteB);
                                Destroy(noteC);
                                Destroy(noteD);
                                Destroy(noteE);
                                Destroy(noteF);
                                Destroy(noteG);
                                Destroy(noteH);
                                Destroy(noteI);
                                Destroy(noteJ);
                                activator.SetActive(false);
                                noteMade = false;
                                EnemyStats.stats.TakeDamage(damage);

                                pickMove("");
                                ResetCount();

                                BattleMenu.instance.switchDancePhase();
                                missedNote = false;
                            }
                            else if ((noteJ == null || noteJ.transform.position.x < -10))//all notes hit
                            {
                                Debug.Log("combo end!");
                                activator.SetActive(false);
                                noteMade = false;
                                EnemyStats.stats.TakeDamage(damage+1);

                                pickMove("");
                                ResetCount();

                                BattleMenu.instance.switchDancePhase();
                            }

                            //check notes hit and do damage


                        }
                        break;
                    }
                case "Foe Attack":
                    {
                        int randMoveType=Random.Range(1,2);
                        int severity = Random.Range(1, 3);
                        switch (randMoveType)
                        {
                            case 1:
                                {
                                 //attacks player in front
                                 if(severity==1)
                                  PlayerStats.stats.TakeDamage(3);
                                    if (severity == 2)
                                        PlayerStats.stats.TakeDamage(5);
                                    if (severity == 3)
                                        PlayerStats.stats.TakeDamage(7);

                                    break; }
                            case 2:
                                {                                  
                                    //attacks both players

                                    if (severity == 1)
                                        PlayerStats.stats.TakeDamageBoth(2);
                                    if (severity == 2)
                                        PlayerStats.stats.TakeDamageBoth(4);
                                    if (severity == 3)
                                        PlayerStats.stats.TakeDamageBoth(5);

                                    break;
                                }

                        }
                    



                        int rand = Random.Range(1, 10);
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
            counted = false;
        }        
            //move determined by pickMove()
            
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
    public void MissedNote()
    {
        missedNote = true;
    }
    public void MissedNote(string tag)
    {
        missedNote = true;
    }
    private void ResetCount()
    {
        timeLeft = 3.0f;
    }
}
