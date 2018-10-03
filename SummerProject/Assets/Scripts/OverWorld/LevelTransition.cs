﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] string loadScene;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player") {
            EnemyHandler.thisMovingEnemy.SetActive(false);
            LevelLoader.ThisIsTheOnlyOne.LoadScene(loadScene, true);
        }
        //SceneManager.LoadScene(loadScene, LoadSceneMode.Additive);
    }
}
