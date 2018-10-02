using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A beat that is pressed in DanceBattle to deal damage.
/// </summary>
public class Note : MonoBehaviour {
    Rigidbody2D rb;
    public float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
        rb.velocity = new Vector2(-speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setSpeed(int rate)
    {
        speed = rate;
    }
}
