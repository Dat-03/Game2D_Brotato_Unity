using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinmanager : MonoBehaviour
{
    // Start is called before the first frame update
    private int idskin = 3;
    private int idweapon = 0;
    public GameObject player;
    public List<GameObject> listSkin;
    void Start()
    {
        idskin = PlayerPrefs.GetInt("idskin");
        idweapon = PlayerPrefs.GetInt("idweapon");
        for (int i = 0; i < listSkin.Count; i++)
        {
            if(idskin == i)
            {
                listSkin[i].SetActive(true);
            } else
            {
                listSkin[i].SetActive(false);
            }
        }
        player.GetComponent<WeaponManager>().idweapon = idweapon;


    }
}
