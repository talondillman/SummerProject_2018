using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// This class creates the moves and calculates damage based on the move picked. 
/// </summary>
public class CreateNotes : MonoBehaviour
{
	/// <summary>
	/// The hitbox and the countdown images
	/// </summary>
	public GameObject activator, count1, count2, count3, countDance;
	/// <summary>
	/// A list of the note prefabs
	/// </summary>
	public GameObject note1, note2, note3, note4, note5, note6, note7, note8;
	/// <summary>
	/// An array of all the notes so they may be randomly chosen.
	/// </summary>
	public GameObject[] notesList;
	private List<string> allMissedNotes;
	public List<string> hitNotes;


	private GameObject noteA, noteB, noteC, noteD, noteE, noteF, noteG, noteH, noteI, noteJ, lastNote, pattern1, pattern2, pattern3;
	private bool danceMode, noteMade, startDelay, missedNote, CountdownOver;
	private string move, noteType;
	public float timeLeft = 3.0f;
	public float noteStartPos = 3f;
	private float noteSpeed = 2f;
	/// <summary>
	/// The time for a quarter beat
	/// </summary>
	private float quarterBeat;
	private int setRound = 0;

	private int missedNotes = 0;

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
		CountdownOver = false;

		quarterBeat = (noteSpeed / 31) * 4;
		allMissedNotes = new List<string>();
		hitNotes = new List<string>();

	}

	/// <summary>
	/// When an action button is pressed, the dance rhythm game is activated and the proper notes appear
	/// A button must be equipped with the method pickMove to update this!
	/// </summary>
	void FixedUpdate()
	{
		if (danceMode && !CountdownOver)
		{
			Countdown();

		}
		///countdown over, start move
		else if (danceMode && CountdownOver)
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
							SetNote(pattern1, 0);
							SetNote(pattern2, quarterBeat * 2);
							SetNote(pattern1, quarterBeat * 8);
							lastNote = SetNote(pattern2, quarterBeat * 10);
							noteMade = true;
						}
						//when last note is hit or goes offscreen, destroy the notes and end turn
						if ((lastNote == null) && noteMade)
						{
							HitEnemy(5);
							MoveDone();

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
							SetNote(pattern1, 0);
							SetNote(pattern2, (quarterBeat * 2));
							lastNote = SetNote(pattern2, (quarterBeat * 4));

							noteMade = true;
						}
						if ((lastNote == null) && noteMade)
						{
							int damage = 4;
							damage = damage - missedNotes;
							EnemyStats.stats.TakeDamage(damage);

							MoveDone();
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
							SetNote(pattern1, 0);
							SetNote(pattern2, quarterBeat);
							lastNote = SetNote(pattern2, (quarterBeat * 5));

							noteMade = true;
						}
						if ((lastNote == null) && noteMade)
						{
							int heal = -3;
							heal = +missedNotes;
							PlayerStats.stats.TakeDamageFrontPlayer(heal);

							MoveDone();

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
							PlayerStats.stats.useGP(2);

							Rand2Pattern();
							SetNote(pattern1, 0);
							SetNote(pattern1, (quarterBeat * 0.666f));
							SetNote(pattern1, (quarterBeat * 1.333f));
							SetNote(pattern2, (quarterBeat * 3));
							SetNote(pattern2, (quarterBeat * 3.666f));
							lastNote = SetNote(pattern2, (quarterBeat * 4.333f));


							noteMade = true;
						}
						if ((lastNote == null) && noteMade)
						{
							damage = 3;

							if (allMissedNotes.Contains(pattern1.tag))//we should not miss any pattern1 notes if we want to do damage
							{
								damage--;
							}
							if (allMissedNotes.Contains(pattern2.tag))
							{
								damage--;
							}

							EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies

							MoveDone();

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
							PlayerStats.stats.useGP(3);


							Rand3Pattern();
							noteA = SetNote(pattern1, 0);
							noteB = SetNote(pattern2, quarterBeat * 4);
							noteC = SetNote(pattern1, quarterBeat * 6);
							noteD = SetNote(pattern3, quarterBeat * 10);
							noteE = SetNote(pattern1, quarterBeat * 12);
							noteF = SetNote(pattern2, quarterBeat * 14);


							noteMade = true;
						}
						if ((noteF == null) && noteMade)
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




							EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies

							MoveDone();

							BattleMenu.instance.switchDancePhase();

						}
						break;
					}
				case "Wild Jive":
					{
						///TOFIX: if press p1 then p2 it breaks
						activator.SetActive(true);
						int damage = 1;
						//bool startDelay = false;
						if (!noteMade)
						{

							while (setRound <= 5)
							{
								Rand2Pattern();

								SetNote(pattern1, quarterBeat * (setRound * 8));
								SetNote(pattern1, quarterBeat * (setRound * 8 + 2));
								noteA = SetNote(pattern2, quarterBeat * (setRound * 8 + 4));
								//TO FIX: damage enemy based on noteA pressed; enemy should have note mapped to them at start?
								EnemyStats.stats.TakeDamage(damage);//TO FIX: damage all enemies
								setRound++;
							}
							if (setRound == 6)
							{
								if ((noteA == null))
								{
									noteMade = true;
								}

							}
						}
						if (noteMade)
						{
							PlayerStats.stats.useGP(4);
							MoveDone();
							ResetCount();
							setRound = 0;


							BattleMenu.instance.switchDancePhase();

						}
						break;
					}
				case "B-Boy"://TO FIX: KEEPS ON CRASHING
					{
						activator.SetActive(true);
						int damage = 1;
						if (!noteMade)
						{
							PlayerStats.stats.useGP(8);

							Rand2Pattern();
							noteA = SetNote(pattern1, 0);
							noteB = SetNote(pattern2, quarterBeat);

							Rand2Pattern();
							noteC = SetNote(pattern1, quarterBeat * 4);
							noteD = SetNote(pattern2, quarterBeat * 5);

							Rand2Pattern();
							noteE = SetNote(pattern1, quarterBeat * 8);
							noteF = SetNote(pattern2, quarterBeat * 9);

							Rand2Pattern();
							noteG = SetNote(pattern1, quarterBeat * 12);
							noteH = SetNote(pattern2, quarterBeat * 13);

							Rand2Pattern();
							noteI = SetNote(pattern1, quarterBeat * 16);
							noteJ = SetNote(pattern2, quarterBeat * 17);

							//IF two notes are pressed, continue loop, otherwise stop
							//note exists and offscreen means miss

							noteMade = true;



						}
						if (noteMade)
						{
							damage = 3;
							if(hitNotes.Count == 2)
							{
								damage++;
							}
							if (hitNotes.Count == 4)
							{
								damage++;
							}
							if (hitNotes.Count == 6)
							{
								damage++;
							}
							if (hitNotes.Count == 8)
							{
								damage++;
							}
							if (hitNotes.Count == 10)
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

								HitEnemy(damage);
								MoveDone();
								ResetCount();

								BattleMenu.instance.switchDancePhase();
								missedNote = false;
							}
							else if ((noteJ == null))//all notes hit
							{
								Debug.Log("combo end!");
								activator.SetActive(false);
								noteMade = false;
								EnemyStats.stats.TakeDamage(damage + 1);

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
			CountdownOver = false;
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
		while (pattern3.Equals(pattern2) || pattern3.Equals(pattern1))
		{
			pattern3 = notesList[Random.Range(0, notesList.Length - 1)];
		}
	}

	private GameObject SetNote(GameObject type, float xOffset)
	{
		return Instantiate(type, new Vector3(noteStartPos + xOffset, activator.transform.position.y, activator.transform.position.z), Quaternion.identity);
	}
	IEnumerator DelayNoteMade()
	{
		yield return new WaitForSeconds(0.01f);
		noteMade = true;

	}
	/// <summary>
	/// If a note has been missed, keep track and reduce damage done. 
	/// </summary>
	public void MissedNote(string tag)
	{
		missedNote = true;
		allMissedNotes.Add(tag);
		missedNotes += 1;

	}
	private void ResetCount()
	{
		timeLeft = 3.0f;
	}
	public void EnemyAttack()
	{
		int randMoveType = Random.Range(1, 2);
		int severity = Random.Range(1, 3);
		switch (randMoveType)
		{
			case 1:
				{
					//attacks player in front
					if (severity == 1)
						PlayerStats.stats.TakeDamageFrontPlayer(3);
					if (severity == 2)
						PlayerStats.stats.TakeDamageFrontPlayer(5);
					if (severity == 3)
						PlayerStats.stats.TakeDamageFrontPlayer(7);

					break;
				}
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
	}
	private void Countdown()
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
			CountdownOver = true;

		}
	}
	private void HitEnemy(int damage)
	{
		int dmg = damage;
		dmg = dmg - missedNotes;
		EnemyStats.stats.TakeDamage(dmg);

		Debug.Log("damage done is " + dmg);

	}
	private void MoveDone()
	{
		activator.SetActive(false);
		pickMove("");//dance mode off, turn off activator 
		noteMade = false;
		ResetCount();
		missedNotes = 0;
		allMissedNotes.Clear();
		hitNotes.Clear();
	}
}
