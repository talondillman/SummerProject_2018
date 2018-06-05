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

    public GameObject activator;
    public GameObject note1;
    public static BattleMenu instance;

    private string p1ActionName="";

    private bool p1Moved;
    private bool p2Moved;
    private bool foeMoved;
    private bool canSwap;
    private bool dancePhase;



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
        ACTION
    }

    public BattleTurns currentState = BattleTurns.P1;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (!p1Moved && !p2Moved)
        {
            canSwap = true;
        }
        else
        {
            canSwap = false;
        }

        switch (currentState)
        {
            case (BattleTurns.P1):
                p2Menu.SetActive(false);
                p2TestAction.SetActive(false);

                p1TestAction.SetActive(true);
                p1Menu.SetActive(true);

                //SETUP BATTLE

                if (p1Moved && dancePhase)
                {
                    switchDancePhase();
                    if (!p2Moved)
                    {
                        currentState = BattleTurns.P2;
                    }
                    else
                    {
                        currentState = BattleTurns.ENEMY;
                    }
                }

                break;

            case (BattleTurns.P2):
                p1Menu.SetActive(false);
                p1TestAction.SetActive(false);

                p2TestAction.SetActive(true);
                p2Menu.SetActive(true);

                //SETUP BATTLE
                if (p2Moved)
                {
                    switchDancePhase();
                    if (!p1Moved)
                    {
                        currentState = BattleTurns.P1;
                    }
                    else
                    {
                          currentState = BattleTurns.ENEMY;
                    }
                }

                break;

            case (BattleTurns.ENEMY):
                p1Menu.SetActive(false);
                p2Menu.SetActive(false);
                p1TestAction.SetActive(false);
                p2TestAction.SetActive(false);

                //enemy action
                foeTestAction.SetActive(true);

                p1Moved = false;
                p2Moved = false;

                if (foeMoved)
                {
                    currentState = BattleTurns.P1;
                }

                break;

            case (BattleTurns.ACTION):
                p1Menu.SetActive(false);
                p2Menu.SetActive(false);

                //SETUP BATTLE
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

    public void setActionName(string name)
    {
        this.p1ActionName = name;
    }

    public void switchDancePhase()
    {
        this.dancePhase = !dancePhase;
    }
}
