using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
	public int maxHealth = 50;                            // The amount of health the player starts the game with.
	public int currHealth, currHealth2;                                   // The current health the player has.
	public int groove, groove2;
	public Slider healthSlider, gpSlider, healthSlider2, gpSlider2;                                 // Reference to the UI's health bar.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	public static PlayerStats stats;
	public LevelLoader lvlLoader = LevelLoader.ThisIsTheOnlyOne;


	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


	void Awake()
	{
		stats = this;
		currHealth = maxHealth;
		currHealth2 = maxHealth;
		groove = 15;
		groove2 = 15;
	}

	private void Start()
	{
		// Set the initial health of the player.

		currHealth = maxHealth;
		currHealth2 = maxHealth;
		groove = 15;
		groove2 = 15;

	}

	void Update()
	{

		////if health <=0 show Game Over
		//if(PlayerPrefs.GetInt("MaxHealth") != currHealth)
		//{
		//    PlayerPrefs.SetInt("MaxHealth", currHealth);
		//    PlayerPrefs.Save();
		//}
		//if (PlayerPrefs.GetInt("GP") != groove)
		//{
		//    PlayerPrefs.SetInt("GP", groove);
		//    PlayerPrefs.Save();
		//}

	}


	public void TakeDamage(int amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;
		if (BattleMenu.instance.frontPlayer.Equals("P1"))
		{
			// Reduce the current health by the damage amount.

			currHealth -= amount;
			lvlLoader.Player1Health -= amount;
			lvlLoader.UpdateUI();

			// Set the health bar's value to the current health.
			healthSlider.value = currHealth;

		}

		if (BattleMenu.instance.frontPlayer.Equals("P2"))
		{
			// Reduce the current health by the damage amount.
			currHealth2 -= amount;
			lvlLoader.Player2Health -= amount;
			lvlLoader.UpdateUI();

			// Set the health bar's value to the current health.
			healthSlider2.value = currHealth2;

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
		lvlLoader.Player1Health -= amount;
		lvlLoader.Player2Health -= amount;
		healthSlider.value = currHealth;


		// Set the health bar's value to the current health.
		healthSlider2.value = currHealth2;
	}
	public void useGP(int amount)
	{
		groove -= amount;
		gpSlider.value = groove;
		lvlLoader.CurrentGP = 17;
		lvlLoader.CurrentGP -= amount;

		lvlLoader.UpdateUI();
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