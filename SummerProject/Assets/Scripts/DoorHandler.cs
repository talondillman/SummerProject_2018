using UnityEngine;

public class DoorHandler : MonoBehaviour {
	
	private Transform player;
	[SerializeField] float distance = 2.0f;
    [SerializeField] string travelToSceneNamed;
    [SerializeField] int spawnID;

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
                LevelLoader.ThisIsTheOnlyOne.LoadScene(travelToSceneNamed, spawnID);
        }
    }
}
