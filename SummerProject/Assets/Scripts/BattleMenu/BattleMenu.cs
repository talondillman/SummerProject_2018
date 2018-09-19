using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class BattleMenu : MonoBehaviour
{

    public GameObject p1Menu, p2Menu, Bboy,Wild,Kick,Charleston;

    public GameObject p1TestAction;
    public GameObject p2TestAction;
    public GameObject foeTestAction;

    public GameObject winText;
    public GameObject loseText;


    public GameObject enemyButtonTest;
    public static BattleMenu instance;

    private bool p1Moved;
    private bool p2Moved;
    private bool foeMoved;
    private bool canSwap;
    private bool dancePhase;
    private bool endBattle;
    public string frontPlayer;

    public BattleTurns currentState = BattleTurns.P1;//all fights start with P1


    /// <summary>
    /// The different types of scenes in a battle 
    /// </summary>
    public enum BattleTurns
    {
        P1,
        P2,
        ENEMY,
        LOSE,
        WIN,
    }


    void Start()
    {
        enemyButtonTest.SetActive(false);
        instance = this;
        frontPlayer = "P1";
    }

    void Update()
    {
        //check if swap tactic is possible
        if (!p1Moved && !p2Moved)
            canSwap = true;
        else
            canSwap = false;

        //switch states depending on person's turn
        switch (currentState)
        {
            case (BattleTurns.P1):
                p2Menu.SetActive(false);
                p2TestAction.SetActive(false);

                if (!p1Moved)
                {
                    p1TestAction.SetActive(true);
                    p1Menu.SetActive(true);
                    if (PlayerStats.stats.groove <8)
                    {
                        Bboy.SetActive(false);
                        if (PlayerStats.stats.groove < 4)
                        {
                            Wild.SetActive(false);
                            if (PlayerStats.stats.groove < 3)
                            {
                                Kick.SetActive(false);
                                if (PlayerStats.stats.groove < 2)
                                    Charleston.SetActive(false);
                                else
                                    Charleston.SetActive(true);
                            }
                            else
                            {
                                Kick.SetActive(true);
                            }
                        }
                        else
                        {
                            Wild.SetActive(true);
                        }
                    }
                    else
                    {
                        Bboy.SetActive(true);
                    }


                }
                //If p1 has done an action, switch
                if (p1Moved && dancePhase)
                {
                    endBattleState();
                    if (!endBattle)
                    {
                        dancePhase = false;
                        if (!p2Moved)
                            currentState = BattleTurns.P2;
                        else
                        {
                            frontPlayer = "P2";//p2 first to move
                            currentState = BattleTurns.ENEMY;
                        }
                    }
                }

                break;

            case (BattleTurns.P2):
                p1Menu.SetActive(false);
                p1TestAction.SetActive(false);

                p2TestAction.SetActive(true);
                p2Menu.SetActive(true);

                if (p2Moved && dancePhase)
                {
                    endBattleState();
                    if (!endBattle)
                    {
                        dancePhase = false;
                        if (!p1Moved)
                            currentState = BattleTurns.P1;
                        else
                        {
                            frontPlayer = "P1";//p1 first to move
                            currentState = BattleTurns.ENEMY;
                        }
                    }
                }

                break;

            case (BattleTurns.ENEMY):
                p1Menu.SetActive(false);
                p2Menu.SetActive(false);
                p1TestAction.SetActive(false);
                p2TestAction.SetActive(false);

                //enemy action
                //foeTestAction.SetActive(true);
              //  enemyButtonTest.SetActive(true);

                int rand = Random.Range(1, 10);
                PlayerStats.stats.TakeDamage(rand);

              //  if (foeMoved && dancePhase)
               // {
                   // foeTestAction.SetActive(false);
                    //enemyButtonTest.SetActive(false);

                    //restart turn order after action
                    p1Moved = false;
                    p2Moved = false;
                    dancePhase = false;
                    endBattleState();
                    if(!endBattle)
                    currentState = BattleTurns.P1;
//                }

                break;

            case (BattleTurns.LOSE):
                p1Menu.SetActive(false);
                p2Menu.SetActive(false);
                loseText.SetActive(true);
                //SETUP BATTLE
                break;

            case (BattleTurns.WIN):
                p1Menu.SetActive(false);
                p2Menu.SetActive(false);
                winText.SetActive(true);

                //Scene lastScene = LevelLoader.ThisIsTheOnlyOne.LastScene;
                 LevelLoader.ThisIsTheOnlyOne.LoadScene("Whitebox1_Jarom", false);
               // Debug.Log(lastScene.name);
               // LevelLoader.ThisIsTheOnlyOne.LoadScene(lastScene.name, false);

                //SETUP BATTLE
                break;


        }
    }

/// <summary>
/// Used to swap which player is active
/// </summary>
    public void swapAction()
    {
        if (canSwap)
        {
            if (currentState.Equals(BattleTurns.P1))
                currentState = BattleTurns.P2;
            else if (currentState.Equals(BattleTurns.P2))
                currentState = BattleTurns.P1;
        }
    }
    public void p1Action()
    {
     p1Moved = true;
    }
    public void p2Action()
    {
        p2Moved = true;
    }
    public void foeAction()
    {
        foeMoved = true;
    }


    public void switchDancePhase()
    {
        this.dancePhase = !dancePhase;
    }
public void endBattleState()
    {
        if(PlayerStats.stats.currHealth <= 0 && PlayerStats.stats.currHealth2<=0)
        {
            currentState = BattleTurns.LOSE;//all fights start with P1
            endBattle = true;

}
        if (EnemyStats.stats.currentHealth <= 0)
        {
            currentState = BattleTurns.WIN;
            endBattle = true;
        }
    }
}
