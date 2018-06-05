using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager current;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(this.gameObject);
            return;
        }

        current = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    
}
