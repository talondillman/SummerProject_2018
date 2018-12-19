using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Keeps track of the players stats between Scenes. 
/// </summary>
public class PlayerStats : MonoBehaviour
{
	public int maxHealth = 50;                            // The amount of health the player starts the game with.
	public int currHealth, currHealth2;                                   // The current health the player has.
	public int groove;
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	public static PlayerStats stats;
	private LevelLoader lvlLoader = LevelLoader.ThisIsTheOnlyOne;


	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


	void Awake()
	{
		stats = this;
		currHealth = lvlLoader.Player1HealthMax;
		currHealth2 = lvlLoader.Player2HealthMax;
		groove = lvlLoader.CurrentGP;
	}

	private void Start()
	{
        // Set the initial health of the player.

        currHealth = lvlLoader.Player1HealthMax;
        currHealth2 = lvlLoader.Player2HealthMax;
        groove = lvlLoader.CurrentGP;

    }

	void Update()
	{

	}


	public void TakeDamageFrontPlayer(int amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;
		if (BattleMenu.instance.frontPlayer.Equals("P1"))
		{
			// Reduce the current health by the damage amount.

			currHealth -= amount;
			LevelLoader.ThisIsTheOnlyOne.Player1Health = currHealth;
			LevelLoader.ThisIsTheOnlyOne.UpdateUI();

			// Set the health bar's value to the current health.
			//healthSlider.value = currHealth;

		}

		if (BattleMenu.instance.frontPlayer.Equals("P2"))
		{
			// Reduce the current health by the damage amount.
			currHealth2 -= amount;
			LevelLoader.ThisIsTheOnlyOne.Player2Health =currHealth2;
			LevelLoader.ThisIsTheOnlyOne.UpdateUI();

			// Set the health bar's value to the current health.
			//healthSlider2.value = currHealth2;

		}


		// If the player has lost all it's health and the death flag hasn't been set yet...
		if ((currHealth2 <= 0 || currHealth <= 0) && !isDead)
		{
			// ... it should die.
			Death();
		}
	}

	public void TakeDamageBoth(int amount)
	{

		currHealth -= amount;
		currHealth2 -= amount;
		LevelLoader.ThisIsTheOnlyOne.Player1Health =currHealth;
		LevelLoader.ThisIsTheOnlyOne.Player2Health =currHealth2;
		//healthSlider.value = currHealth;


		// Set the health bar's value to the current health.
		//healthSlider2.value = currHealth2;
	}
	public void useGP(int amount)
	{
        groove -=amount;
        //gpSlider.value = groove;

		LevelLoader.ThisIsTheOnlyOne.CurrentGP = groove;
		LevelLoader.ThisIsTheOnlyOne.UpdateUI();
	}
	
	public void Reset()
	{
		PlayerPrefs.DeleteKey("GP");
		PlayerPrefs.DeleteKey("MaxHealth");
		currHealth = maxHealth;
		groove = 5;
	}
	void Death()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;
	}
}