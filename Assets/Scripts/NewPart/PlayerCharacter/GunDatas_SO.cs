using System;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Character", menuName = "Gun/GunData")]
public class GunDatas_SO : ScriptableObject
{
    public int id;
    public string gunName;
    public int gunPrice;

    public GunDatas_SO(int id, string gunName, int gunPrice)
    {
        this.id = id;
        this.gunName = gunName;
        this.gunPrice = gunPrice;
    }
}
