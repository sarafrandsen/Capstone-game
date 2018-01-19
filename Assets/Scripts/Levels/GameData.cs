using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    [HideInInspector]
    public Dictionary<string, bool> fireDoors;
    [SerializeField]
    public List<string> storiesCollected;

    private static bool gameDataExists; // game data state saved between scenes

    // Use this for initialization
    void Start()
    {
        if (!gameDataExists)
        {
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
            { "West", false },
        };

        storiesCollected = new List<string>();
	}
}
