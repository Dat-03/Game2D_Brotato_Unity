using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GunData
{
    public int id;
    public string gunName;
    public int gunPrice;
    


    public static GunData GetDataInstance(GunDatas_SO dataSO)
    {
        return new GunData()
        {
            id = dataSO.id,
            gunName = dataSO.gunName,
            gunPrice = dataSO.gunPrice,
          

        };
    }
}
