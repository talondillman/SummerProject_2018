using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour {
    public bool basicAtk;
    private BattleTurnStart battleStart = new BattleTurnStart();
    // Use this for initialization
    public enum BattleTurns
    {
        START,
        P1,
        P2,
        ENEMY,
        LOSE,
        WIN,
        CALCDAMAGE
    }

    private BattleTurns currentState;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentState);
        switch (currentState)
        {
            case (BattleTurns.START):
                //SETUP BATTLE
                battleStart.prepBattle();
                break;
            case (BattleTurns.P1):
                //SETUP BATTLE
                break;
            case (BattleTurns.P2):
                //SETUP BATTLE
                break;
            case (BattleTurns.ENEMY):
                //SETUP BATTLE
                break;
            case (BattleTurns.CALCDAMAGE):
                //SETUP BATTLE
                break;
            case (BattleTurns.LOSE):
                //SETUP BATTLE
                break;
            case (BattleTurns.WIN):
                //SETUP BATTLE
                break;


        }
	}
 
    private void OnGUI()
    {
        if(GUILayout.Button("Next State"))
        {
            if (currentState == BattleTurns.START)
            {
                currentState = BattleTurns.P1;
            }
        }
    }
}
