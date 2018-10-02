using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// This class keeps track of the turn state in the battle and if determines what to do if there's a win/loss.
/// </summary>
public class BattleMenu : MonoBehaviour
{
	/// <summary>
	/// Game objects from the danceBattle
	/// </summary>
	public GameObject p1Menu, p2Menu, Bboy, Wild, Kick, Charleston;

	/// <summary>
	/// For testing only: confirms which turn it is
	/// </summary>
	public GameObject p1TestAction;
	public GameObject p2TestAction;
	public GameObject foeTestAction;

	public GameObject winText;
	public GameObject loseText;

	/// <summary>
	/// An instance of BattleMenu that can be used in other classes. 
	/// </summary>
	public static BattleMenu instance;

	/// <summary>
	/// Checks which players have done actions yet
	/// </summary>
	private bool p1HasMoved;
	private bool p2HasMoved;
	private bool foeMoved;
	private bool canSwap;
	/// <summary>
	/// Checks that the dance battle is still ongoing. 
	/// </summary>
	private bool playerTurn;
	private bool endBattle;
	/// <summary>
	/// The player in the front during battle
	/// </summary>
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
		instance = this;
		endBattle = false;
		frontPlayer = "P1";
	}

	void Update()
	{
		//Players can only swap places if neither have moved yet
		if (!p1HasMoved && !p2HasMoved)
			canSwap = true;
		else
			canSwap = false;

		//switch states depending on person's turn
		switch (currentState)
		{
			case (BattleTurns.P1):
				p2Menu.SetActive(false);
				p2TestAction.SetActive(false);

				if (!p1HasMoved)
				{
					p1TestAction.SetActive(true);
					p1Menu.SetActive(true);
					GPMovesAvailable();

				}
				//If p1 has done an action, switch to the new turn
				if (p1HasMoved && playerTurn)
				{
					EndBattle();
					if (!endBattle)
					{
						playerTurn = false;
						if (!p2HasMoved)
							currentState = BattleTurns.P2;
						else
						{
							frontPlayer = "P2";//p2 moved already, so they went first
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

				if (p2HasMoved && playerTurn)
				{
					EndBattle();
					if (!endBattle)
					{
						playerTurn = false;
						if (!p1HasMoved)
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
				Debug.Log("Enemy turn now");
				p1Menu.SetActive(false);
				p2Menu.SetActive(false);
				p1TestAction.SetActive(false);
				p2TestAction.SetActive(false);

				CreateNotes.instance.EnemyAttack();
				
				//restart turn order after action
				p1HasMoved = false;
				p2HasMoved = false;
				playerTurn = false;
				EndBattle();
				if (!endBattle)//if not a win/loss, continue the battle
					currentState = BattleTurns.P1;

				break;

			case (BattleTurns.LOSE):
				p1Menu.SetActive(false);
				p2Menu.SetActive(false);
				loseText.SetActive(true);
				break;

			case (BattleTurns.WIN):
				p1Menu.SetActive(false);
				p2Menu.SetActive(false);
				winText.SetActive(true);

				Debug.Log("Last Scene " + LevelLoader.ThisIsTheOnlyOne.LastScene);
				LevelLoader.ThisIsTheOnlyOne.LoadScene(LevelLoader.ThisIsTheOnlyOne.LastScene, false);
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
		p1HasMoved = true;
	}
	public void p2Action()
	{
		p2HasMoved = true;
	}
	public void foeAction()
	{
		foeMoved = true;
	}


	public void switchDancePhase()
	{
		this.playerTurn = !playerTurn;
	}
	/// <summary>
	/// Checks if the battle has been won or lost. 
	/// </summary>
	public void EndBattle()
	{
		if (PlayerStats.stats.currHealth <= 0 && PlayerStats.stats.currHealth2 <= 0)
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

	
	/// <summary>
	/// Checks if there is enough GP available to use this move. If not, hide the item. TO FIX: Only applies to P1 Menu
	/// </summary>
	private void GPMovesAvailable()
	{
		Debug.Log("GP is " + PlayerStats.stats.groove);

		if (PlayerStats.stats.groove < 8)
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
}
