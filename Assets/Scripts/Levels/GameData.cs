using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    [HideInInspector]
    public Dictionary<string, bool> fireDoors = new Dictionary<string, bool>();
    [SerializeField]
    public List<string> storiesCollected = new List<string>();

    public bool cheatMode = false;

    private static bool showInstructions; // instructions scene
    private static bool gameDataExists; // game data state saved between scenes

    public static GameData Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            transform.gameObject.tag = "GameData";
            DontDestroyOnLoad(transform.gameObject);
        }

        fireDoors = new Dictionary<string, bool>() {
            { "North", false },
            { "East", false },
            { "South", false },
            //{ "West", false },
            { "NorthWest", false },
        };
    }

    // Use this for initialization
    void Start()
    {
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
