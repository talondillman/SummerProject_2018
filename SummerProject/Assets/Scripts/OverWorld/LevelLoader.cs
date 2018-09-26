﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelLoader : MonoBehaviour {
        //There can only be one
    public static LevelLoader ThisIsTheOnlyOne;
    private int DebugID;
    
    private Transform SpawnPoint;// Spawn Point to move palyer to on load scene
    private GameObject panel;   // The black loading screen panel
    private GameObject Player;  // The player. to Keep eyes on him. AT ALL TIMES
    private GameObject MainCamera; // The Main Camera for the OverWorld
    private Transform EndBattleSpawnPoint;   // Where the player should spawn after a battle

    public string LastScene;    // Keep the Last scene name
    [SerializeField] float timeToWhite = 0.75f;

    /// <summary>
    /// All the stuff for the UI
    /// </summary>
    public int Player1Health { get; set; }
    public int Player2Health { get; set; }
    public int Player1HealthMax { get; set; }
    public int Player2HealthMax { get; set; }
    public int TotalExperience { get; set; }
    public int TotalCredits { get; set; }
    public int MaxGP { get; set; }
    public int CurrentGP { get; set; }
    [SerializeField] TMP_Text plyr1health;
    [SerializeField] TMP_Text plyr2health;
    [SerializeField] TMP_Text plyr1healthmax;
    [SerializeField] TMP_Text plyr2healthmax;
    [SerializeField] TMP_Text totalxp;
    //[SerializeField] TMP_Text totalcredits;
    [SerializeField] TMP_Text currentGP;


    private void Start()
    {
        if (ThisIsTheOnlyOne != null) {
            Destroy(this.gameObject);
            return;
        }

        ThisIsTheOnlyOne = this;
        DebugID = Random.Range(0, 10000);
        Debug.Log("this is the only one = " + ThisIsTheOnlyOne.ToString());
        GameObject.DontDestroyOnLoad(this.gameObject);

        //Find the player && camera
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        setEverything();
        // Find the panel to make the screen go black for a second.
        panel = GameObject.Find("Canvas/BlackScreen");
        
        //Make it invisible
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    public void setEverything()
    {
        Debug.Log("Init values for UI");
        Player1HealthMax = 22;
        Player1Health = Player1HealthMax;
        Player2HealthMax = 27;
        Player2Health = Player2HealthMax;
        TotalExperience = 356;
        TotalCredits = 0;
        MaxGP = 19;
        CurrentGP = MaxGP;
        UpdateUI();

    }
    /// <summary>
    /// Makes it so you can leave the game
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
            Debug.Log("Application should have Quit");
        }
    }

    public void UpdateUI()
    {
        plyr1health.text = Player1Health.ToString();
        plyr2health.text = Player2Health.ToString();
        totalxp.text = TotalExperience.ToString();
        currentGP.text = CurrentGP.ToString();
    }
    /// <summary>
    /// Loads the scene and places player at a default position
    /// </summary>
    /// <param name="scene"> The scene to load </param>
    public void LoadScene(string scene)
    {
        LastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        Invoke("GoBlack", 1.5f);
        Invoke("GoBack", 1.5f);

        Vector3 spawnhere;
        SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.GetChild(0);
        spawnhere = SpawnPoint.position;
        Debug.Log("Finding player and moving");
        Player.transform.position = spawnhere;
    }

    /// <summary>
    /// Loads the scene and puts the player at a particular place
    /// </summary>
    /// <param name="scene"> The scene to load </param>
    /// <param name="spawnPoint"> The spawn point to place the player </param>
    public void LoadScene(string scene, int spawnPoint)
    {
        LastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        Invoke("GoBlack", 0f);
        Invoke("GoBack", timeToWhite);

        Vector3 spawnhere;
        SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.GetChild(spawnPoint);
        spawnhere = SpawnPoint.position;
        Debug.Log("Finding player and moving");
        Player.transform.position = spawnhere;
    }
    /// <summary>
    /// Loads the Dance Battle Scene
    /// </summary>
    /// <param name="scene"> name of the scene to load </param>
    /// <param name="DanceBattle"> Whether or not it's a dance battle</param>
    public void LoadScene(string scene, bool DanceBattle)
    {
       
        if (!DanceBattle) {
            //EndBattleSpawnPoint.position = new Vector3(Player.transform.position.x -1, Player.transform.position.y, Player.transform.position.z) ;
            //Debug.Log("SpawnPoint = " + EndBattleSpawnPoint.ToString());
        }
              

        if (DanceBattle) {
            LastScene = SceneManager.GetActiveScene().name;
            Debug.Log("Last Scene " + LastScene);
            MainCamera.SetActive(false);
            Player.SetActive(false);
            gameObject.transform.Find("Canvas").Find("Lower_Right_UI").gameObject.SetActive(false);
        } else {
            Debug.Log("Making player active");
            MainCamera.SetActive(true);
            Player.SetActive(true);
            gameObject.transform.Find("Canvas").Find("Lower_Right_UI").gameObject.SetActive(true);
            // Player.transform.position = EndBattleSpawnPoint.position;
        }
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
       
    }
    

    void GoBlack()
    {
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 255);
    }
    void GoBack()
    {
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }
}
