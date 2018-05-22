using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] string loadScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelLoader_other.ThisIsTheOnlyOne.LoadScene(loadScene);
        //SceneManager.LoadScene(loadScene, LoadSceneMode.Additive);
    }
}
