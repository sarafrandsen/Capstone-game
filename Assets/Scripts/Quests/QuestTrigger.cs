using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {
    public int questNumber;
    public bool startQuest;
    public bool endQuest;

    private QuestManager questManager;

	// Use this for initialization
	void Start () {
        questManager = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Player" && !questManager.questCompleted[questNumber])
        {
            if (startQuest && !questManager.quests[questNumber].gameObject.activeSelf)
            {
                //questManager.quests[questNumber].gameObject.SetActive(true);
                questManager.quests[questNumber].StartQuest();
            }
            if (endQuest && questManager.quests[questNumber].gameObject.activeSelf)
            {
                questManager.quests[questNumber].EndQuest();
            }
        }
    }
}
