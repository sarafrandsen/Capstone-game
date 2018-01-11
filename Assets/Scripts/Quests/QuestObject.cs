using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {
    //public Quest quest;
    public string startText;
    public string endText;


    public int questNumber;
    public QuestManager questManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartQuest()
    {
        Debug.Log("Quest Started");

        questManager.ShowQuestText(startText);
        questManager.questCompleted[questNumber] = false;
        gameObject.SetActive(true);
    }

    public void EndQuest()
    {
        Debug.Log("Quest Completed");
        questManager.ShowQuestText(endText);
        questManager.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}

//public enum Quest
//{
//    Intro,
//    First
//}
