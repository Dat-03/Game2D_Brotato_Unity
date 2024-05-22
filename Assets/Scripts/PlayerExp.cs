using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public HealthBar ExpBar;
    int currentExp = 0;
    int currentLevel = 1;
    int requireExp = 30;

    public GameObject levelUpPanel;
    public GameObject UpdateUi;
    public bool isupdate = false;
    public string name;
    public GameObject infomationQuest;

    // Level + exp
    public void UpdateExperience(int addExp, string name)
    {
        isupdate = true;
        this.name = name;
        currentExp += addExp;
        if (currentExp >= requireExp)
        {
            currentLevel++;
            currentExp = currentExp - requireExp;
            requireExp = (int)(requireExp * 2);
            OpenLevelUpPanel();
            // Level up panel
        }
        // Update Exp bar
        infomationQuest.GetComponent<infomationQuest>().isupdate = true;
        infomationQuest.GetComponent<infomationQuest>().name = name;
        ExpBar.UpdateBar(currentExp, requireExp, "Level " + currentLevel.ToString());
    }

    public void CloseLevelUpPanel()
    {
        CanvasGroup group = levelUpPanel.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
        Time.timeScale = 1;
    }

    public void OpenLevelUpPanel( )
    {
        CanvasGroup group = levelUpPanel.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
        Time.timeScale = 0;
    }
}
