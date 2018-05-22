using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public static LevelLoader current;
    Transform catalogue;
    Transform shared;
    GameObject player;
    //Stuff to ignore
    Dictionary<Transform, bool> ignore = new Dictionary<Transform, bool>();

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        player = GameObject.FindGameObjectWithTag("Player");

        //create a shared area i.e the player
        shared = new GameObject().transform;
        shared.name = "_Shared";
        player.transform.SetParent(shared);

        //ignore this level loader and shared level loader
        ignore[transform] = true;
        ignore[shared] = true;
        ignore[player.transform] = true;

        UpdateCatalogue(SceneManager.GetActiveScene().name);

    }

    void UpdateCatalogue(string sceneName)
    {
        //create the catalogue
        catalogue = new GameObject().transform;
        catalogue.name = "_" + sceneName;

        //Loop through all assets
        foreach (Transform t in Object.FindObjectsOfType<Transform>())
        {
            if (ignore.ContainsKey(t))
            {
                Debug.Log("Skipping " + t.ToString());
                continue;
            }
            if (t.parent == null)
            {
                Debug.Log("Adding " + t.ToString());
                t.SetParent(catalogue);
            }
        }
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneLoop(scene));
    }

    //destroys the stored catalogue
    //loads the new scene
    IEnumerator LoadSceneLoop(string scene)
    {

        Destroy(catalogue.gameObject);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);

        yield return null;

        UpdateCatalogue(SceneManager.GetActiveScene().name);

        //todo place player at propper position
        // todo cleanup other player in scene
    }
}
