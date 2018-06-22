using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour {
	
	public Transform player;
	public float distance = 2.0f;
    public string travelToSceneNamed;
    public int spawnPoint;

	// Use this for initialization
	void Awake () {
        player =  GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.position, transform.position) < distance)
			gameObject.GetComponent<MeshRenderer>().enabled = true;
		else
			gameObject.GetComponent<MeshRenderer>().enabled = false;
	}

    void OnTriggerStay(Collider col)
    {
        //print("Collision with Door Trigger");
        if (col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            if (travelToSceneNamed == "" || travelToSceneNamed == null)
                print("No Scene Specified");
            else
                LevelLoader.ThisIsTheOnlyOne.LoadScene(travelToSceneNamed, spawnPoint);
        }
    }
}
