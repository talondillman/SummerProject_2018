using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveList :MonoBehaviour
{
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;

    

    public Move[] Action;

    private void Start()
    {
        note1.SetActive(false);
        note2.SetActive(false);
        note3.SetActive(false);
        note4.SetActive(false);

        string movesAsJson = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json"));
       // Move[] moveList = JsonHelper.FromJson<Move>(movesAsJson);
     // Action = JsonHelper.FromJson<Move>(movesAsJson);
      //  Debug.Log(Action[0].name);


      //  Debug.Log(moveList[0].name);
    }

public void ChooseAction(int index)
        {

        }
    [SerializeField]
    public class Move
    {
        public int id;
        public string name;
        public int damage;
        public string[] notes;
        public bool multitarget;

        public Move(int id, string name, int damage, string[] notes, bool multitarget)
        {
            this.id = id;
            this.name = name;
            this.damage = damage;
            this.notes = notes;
            this.multitarget = multitarget;
        }
    }


}