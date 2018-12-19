using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


// @TODO realistically all that should be in this script is the level loading stuff and the catalogues to keep track of 
// shared objects I would like to move all the UI stuff to a different script for easier management and adaptibility.

public class LevelLoader : MonoBehaviour {
        //There can only be one
    public static LevelLoader ThisIsTheOnlyOne; //The only instance of this GameObject that should ever exist.
    private Transform catalogue;//The catalogue of everthing in the scene you don't want to keep
    private Transform shared;//Everything that you want to keep track of from scene to scene
    Dictionary<Transform, bool> ignore = new Dictionary<Transform, bool>();//Everthing in the scene should be stored in here w/ a boolean declaring it worth keeping / not keeping
    private int DebugID;
    
    private Transform SpawnPoint;// Spawn Point to move palyer to on load scene
    private int spawnID; //Find correct SpawnPoint
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

    /// <summary>
    /// Initialize all object references
    /// adds everything to where it belongs into the ignore dictionary and everything else set parent to the catalogue transform object
    /// </summary>
    private void Awake()
    {
        if (ThisIsTheOnlyOne != null) {
            Destroy(this.gameObject);
            return;
        }

        ThisIsTheOnlyOne = this;
        
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        //Create the shared area
        shared = new GameObject().transform;
        shared.name = "_Shared";
        Player.transform.SetParent(shared);
        MainCamera.transform.SetParent(shared);

        //Setup ignored elements
        ignore[transform] = true;
        ignore[Player.transform] = true;
        ignore[MainCamera.transform] = true;
        ignore[shared] = true;

        updateCatalogue(SceneManager.GetActiveScene().name);
        
    }
    /// <summary>
    /// Call other funtions and scripts here
    /// Makes sure the Black load screen is invisble.
    /// </summary>
    public void Start()
    {
        setEverything();

        // Find the panel to make the screen go black for a second.
        panel = GameObject.Find("Canvas/BlackScreen");
        //Make sure it is invisible
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    
    /// <summary>
    /// Sets all the GUI variables
    /// </summary>
    public void setEverything()
    {
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
    /// <summary>
    /// Updates the GUI with appropriate data when called
    /// </summary>
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
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);

        Invoke("GoBlack", 1.5f);
        Invoke("GoBack", 1.5f);

    }

    /// <summary>
    /// Loads the scene and puts the player at a particular place
    /// </summary>
    /// <param name="scene"> The scene to load </param>
    /// <param name="spawnID"> The spawn point to place the player </param>
    public void LoadScene(string scene, int spawnID)
    {
        LastScene = SceneManager.GetActiveScene().name;
        this.spawnID = spawnID;

        StartCoroutine(LoadSceneLoop(scene));

        Invoke("GoBlack", 0f);
        Invoke("GoBack", timeToWhite);

    }
    /// <summary>
    /// Loads the Dance Battle Scene or transitions back from the Dance Battle.
    /// </summary>
    /// <param name="scene"> name of the scene to load </param>
    /// <param name="DanceBattle"> Whether or not it's a dance battle</param>
    public void LoadScene(string scene, bool DanceBattle)
    {

        Invoke("GoBlack", 0f);
        Invoke("GoBack", timeToWhite);

        spawnID = -1;
        
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
        }

        StartCoroutine(LoadSceneLoop(scene));
    }

    private void SpawnHere(int spawnID)
    {
        if(spawnID < 0) { spawnID = GameObject.FindGameObjectWithTag("SpawnPoint").transform.childCount; }
        Vector3 spawnhere;
        SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.GetChild(spawnID);
        spawnhere = SpawnPoint.position;
        Player.transform.position = spawnhere;
    }
    /// <summary>
    /// Runs through every object in the scene and puts it in the catalogue
    /// </summary>
    private void updateCatalogue(string scene)
    {
        //Create catalogue of gameObjects
        catalogue = new GameObject().transform;
        catalogue.name = "_" + scene;

        foreach (Transform t in Object.FindObjectsOfType<Transform>()) {

            if (ignore.ContainsKey(t)) {
                //Debug.Log("This object is ignored : " + t.ToString());
                continue;
            }

            if (t.parent == null) {
                //Debug.Log("This object parent set to catlogue : " + t.ToString());
                t.SetParent(catalogue);
            }
        }
    }

    /// <summary>
    /// Destroys the catalougue from the previous scene and creates a new one in the new scene.
    /// </summary>
    /// <param name="scene">The scene to move to</param>
    /// <returns></returns>
    IEnumerator LoadSceneLoop(string scene)
    {
        //Destroy everyting from the previous scene.
        Destroy(catalogue.gameObject);

        SceneManager.LoadScene(scene, LoadSceneMode.Additive);

        yield return null;

        //Create a new catalogue from the new scene. Spawn player in the right place.
        updateCatalogue(scene);
        SpawnHere(spawnID);

        //%TODO hadle the enemy respawn
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
