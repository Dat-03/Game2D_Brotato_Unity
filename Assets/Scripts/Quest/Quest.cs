using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
public class Quest
{
    public string description;
    public int monstersToKill;
    public int goldReward;
    public int expReward;
    public int completed;
    


    public void Complete()
    {
        completed = 1; // Sửa từ false thành true khi nhiệm vụ hoàn thành
        Debug.Log(completed + " was completed!"); // Sửa lỗi chính tả
    }
}

