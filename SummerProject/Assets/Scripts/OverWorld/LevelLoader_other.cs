using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader_other : MonoBehaviour {

    //There can only be one
    public static LevelLoader_other ThisIsTheOnlyOne;
    private Transform SpawnPoint;

    [SerializeField] float timeToWhite = 0.75f;
    private void Start()
    {
        if (ThisIsTheOnlyOne != null) {
            Destroy(this.gameObject);
            return;
        }

        ThisIsTheOnlyOne = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }
    /// <summary>
    /// Loads the scene and places player at a regular position
    /// </summary>
    /// <param name="scene"> The scene to load </param>
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        Invoke("GoBlack", 1.5f);
        Invoke("GoBack", 1.5f);

        Vector3 spawnhere;
        SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.GetChild(0);
        spawnhere = SpawnPoint.position;
        Debug.Log("Finding player and moving");
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnhere;
    }

    /// <summary>
    /// Loads the scene and puts the player at a particular place
    /// </summary>
    /// <param name="scene"> The scene to load </param>
    /// <param name="spawnPoint"> The spawn point to place the player </param>
    public void LoadScene(string scene, int spawnPoint)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        Invoke("GoBlack", 0f);
        Invoke("GoBack", timeToWhite);

        Vector3 spawnhere;
        SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.GetChild(spawnPoint - 1);
        spawnhere = SpawnPoint.position;
        Debug.Log("Finding player and moving");
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnhere;
    }
    /// <summary>
    /// Loads the Dance Battle Scene
    /// </summary>
    /// <param name="scene"> name of the scene to load </param>
    /// <param name="DanceBattle"> Whether or not it's a dance battle</param>
    public void LoadScene(string scene, bool DanceBattle)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        GoBlack();
        Invoke("GoBack", timeToWhite);

        if (DanceBattle) {
            Debug.Log("Making player inactive");
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        } else {
            Debug.Log("Making player active");
            GameObject.FindGameObjectWithTag("Player").SetActive(true);
        }
    }

    void GoBlack()
    {
        GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 255);
    }
    void GoBack()
    {
        GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }
}
