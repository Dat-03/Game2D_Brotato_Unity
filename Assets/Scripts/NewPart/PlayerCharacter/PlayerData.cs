using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    public int id;
    public string charactorName;
    public int damage;
    public int currentHealth;
    public int maxHealth;
  

    public static PlayerData GetDataInstance(PlayerData_SO dataSO)
    {
        return new PlayerData()
        {
            id = dataSO.id,
            charactorName = dataSO.charactorName,
            damage = dataSO.damage,
            maxHealth = dataSO.maxHealth,
            currentHealth = dataSO.currentHealth,
        
        };
    }
}
