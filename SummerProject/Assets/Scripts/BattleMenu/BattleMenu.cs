using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class BattleMenu : MonoBehaviour
{

    public GameObject p1Menu;
    public GameObject p2Menu;

    public GameObject p1TestAction;
    public GameObject p2TestAction;
    public GameObject foeTestAction;
    public GameObject enemyButtonTest;
    public GameObject activator;
    public static BattleMenu instance;

    private bool p1Moved;
    private bool p2Moved;
    private bool foeMoved;
    private bool canSwap;
    private bool dancePhase;

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
    }

    void Update()
    {
        //check if swap tactic is possible
        if (!p1Moved && !p2Moved)
            canSwap = true;
        else
            canSwap = false;

        Debug.Log(currentState);

        //switch states depending on person's turn
        switch (currentState)
        {
            case (BattleTurns.P1):
                p2Menu.SetActive(false);
                p2TestAction.SetActive(false);

                p1TestAction.SetActive(true);
                p1Menu.SetActive(true);

                //If p1 has done an action, switch
                if (p1Moved && dancePhase)
                {
                    dancePhase = false; 
                    if (!p2Moved)
                        currentState = BattleTurns.P2;
                    else
                        currentState = BattleTurns.ENEMY;
                }

                break;

            case (BattleTurns.P2):
                p1Menu.SetActive(false);
                p1TestAction.SetActive(false);

                p2TestAction.SetActive(true);
                p2Menu.SetActive(true);

                if (p2Moved && dancePhase)
                {
                    dancePhase = false;
                    if (!p1Moved)
                        currentState = BattleTurns.P1;
                    else
                          currentState = BattleTurns.ENEMY;
                }

                break;

            case (BattleTurns.ENEMY):
                p1Menu.SetActive(false);
                p2Menu.SetActive(false);
                p1TestAction.SetActive(false);
                p2TestAction.SetActive(false);

                //enemy action
                foeTestAction.SetActive(true);
                enemyButtonTest.SetActive(true);

                if (foeMoved && dancePhase)
                {
                    foeTestAction.SetActive(false);
                    enemyButtonTest.SetActive(false);

                    //restart turn order after action
                    p1Moved = false;
                    p2Moved = false;
                    dancePhase = false;
                    currentState = BattleTurns.P1;
                }

                break;

            case (BattleTurns.LOSE):
                p1Menu.SetActive(false);
                p2Menu.SetActive(false);

                //SETUP BATTLE
                break;

            case (BattleTurns.WIN):
                p1Menu.SetActive(false);
                p2Menu.SetActive(false);

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
}
