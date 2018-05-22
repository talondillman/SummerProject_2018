using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader_other : MonoBehaviour {

    //There can only be one
    public static LevelLoader_other ThisIsTheOnlyOne;

    private void Start()
    {
        if(ThisIsTheOnlyOne != null)
        {
            Destroy(this.gameObject);
            return;
        }

        ThisIsTheOnlyOne = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
