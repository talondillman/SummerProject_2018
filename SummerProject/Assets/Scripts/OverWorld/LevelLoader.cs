using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
        //There can only be one
    public static LevelLoader ThisIsTheOnlyOne;
    
    private Transform SpawnPoint;// Spawn Point to move palyer to on load scene
    private GameObject panel;   // The black loading screen panel
    private GameObject Player;  // The player. to Keep eyes on him. AT ALL TIMES
    private GameObject MainCamera; // The Main Camera for the OverWorld
    private Transform EndBattleSpawnPoint;   // Where the player should spawn after a battle

    public Scene LastScene { get; set; }    // Keep the Last scene name
    [SerializeField] float timeToWhite = 0.75f;

    private void Start()
    {
        if (ThisIsTheOnlyOne != null) {
            Destroy(this.gameObject);
            return;
        }

        ThisIsTheOnlyOne = this;
        Debug.Log("this is the only one = " + ThisIsTheOnlyOne.ToString());
        GameObject.DontDestroyOnLoad(this.gameObject);

        //Find the player && camera
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // Find the panel to make the screen go black for a second.
        panel = GameObject.Find("Canvas/BlackScreen");
        
        //Make it invisible
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }
    /// <summary>
    /// Loads the scene and places player at a default position
    /// </summary>
    /// <param name="scene"> The scene to load </param>
    public void LoadScene(string scene)
    {
        LastScene = SceneManager.GetActiveScene();
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
        LastScene = SceneManager.GetActiveScene();
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
        LastScene = SceneManager.GetActiveScene();
        EndBattleSpawnPoint = Player.transform;
        Debug.Log("SpawnPoint = " + EndBattleSpawnPoint.ToString());

        SceneManager.LoadScene(scene, LoadSceneMode.Single);        

        if (DanceBattle) {
            Debug.Log("Making player inactive");
            MainCamera.SetActive(false);
            Player.SetActive(false);
        } else {
            Debug.Log("Making player active");
            MainCamera.SetActive(true);
            Player.SetActive(true);
            Player.transform.position = EndBattleSpawnPoint.position;
        }
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
