using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "All Gun", menuName = "Gun/GunDatas")]

public class AllGunDatas : ScriptableObject
{
    public List<GunDatas_SO> guns;

    // Phương thức để thêm một nhân vật mới vào danh sách
    public void AddGun(GunDatas_SO newGun)
    {
        guns.Add(newGun);
    }

    
}
