using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> lweapon1, lweapon2, lweapon3;
    public int idweapon = 1;
    void Start()
    {
        idweapon = PlayerPrefs.GetInt("idweapon");
    }

    // Update is called once per frame
    void Update()
    {
        if(idweapon == 1) {
            for(int i = 0; i < 4; i++) {
                lweapon1[i].SetActive(true);
                lweapon2[i].SetActive(false);
                lweapon3[i].SetActive(false);
            }
        } else if(idweapon == 2) {
            for(int i = 0; i < 4; i++) {
                lweapon1[i].SetActive(false);
                lweapon2[i].SetActive(true);
                lweapon3[i].SetActive(false);
            }
        } else if(idweapon == 3) {
            for(int i = 0; i < 4; i++) {
                lweapon1[i].SetActive(false);
                lweapon2[i].SetActive(false);
                lweapon3[i].SetActive(true);
            }
        }
    }
}
