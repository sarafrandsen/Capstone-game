using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    [HideInInspector]
    public Dictionary<string, bool> fireDoors;
    [SerializeField]
    public List<string> storiesCollected;

    public bool cheatMode = false;

    private static bool showInstructions; // instructions scene
    private static bool gameDataExists; // game data state saved between scenes

    // Use this for initialization
    void Start()
    {
        
        if (!gameDataExists)
        {
            transform.gameObject.tag = "GameData";
            gameDataExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fireDoors = new Dictionary<string, bool>() {
            { "North", false },
            { "East", false },
            { "South", false },
            //{ "West", false },
            { "NorthWest", false },
        };

        storiesCollected = new List<string>();

        if (cheatMode)
        {
            fireDoors["North"] = fireDoors["East"] = fireDoors["South"] = fireDoors["NorthWest"] = true;
            storiesCollected.Add("A good story");
            storiesCollected.Add("A better story");
            storiesCollected.Add("A best story");
            storiesCollected.Add("A worst story");
            //storiesCollected.Add("A terrible story");
        }

	}
}
