using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="shopMenu", menuName ="Scriptable objects/New Shop Item", order =1)]
public class ShopItemSO : ScriptableObject
{
    public int id;
    public bool isSkin;
    public bool statusBuy;
    public bool isEquipment;
    public string title;
    public string description;
    public int baseCast;
    public Sprite image;
}
