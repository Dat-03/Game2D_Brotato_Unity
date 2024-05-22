using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject QuestUi;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateQuest(bool isupdate, string name)
    {
        QuestUi.GetComponent<QuestUIManager>().isupdate = isupdate;
        QuestUi.GetComponent<QuestUIManager>().name = name;
    }
}
