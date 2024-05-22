using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMapController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject btnNextMap;
    private int idmap = 0, isdomission = 0, idskin = 0, idweapon = 0;
    public int idmission = 0;
    void Start()
    {
        idmap = PlayerPrefs.GetInt("mapchoose");
        isdomission = PlayerPrefs.GetInt("isdomission");
        idmission = PlayerPrefs.GetInt("idmission");
        idskin = PlayerPrefs.GetInt("idskin");
        idweapon = PlayerPrefs.GetInt("idweapon");
    }

    // Update is called once per frame
    
    void Update()
    {
        if(idmap < idmission) {
            //btnNextMap.SetActive(true);
            Debug.Log("Well done");
        } else {
            //btnNextMap.SetActive(false);
            Debug.Log("False");

        }
    }
    public void nextMap() {
        PlayerPrefs.SetInt("mapchoose", idmap+1);
        if(idmission == idmap +1) {
            isdomission = 0;
        }
        PlayerPrefs.SetInt("isdomission", isdomission);
        PlayerPrefs.SetInt("idmission", idmission);
        PlayerPrefs.SetInt("id_mission", idmission);
        PlayerPrefs.SetInt("idskin", idskin);
        PlayerPrefs.SetInt("idweapon", idweapon);
        string current = "Map"+(idmap+1);
        SceneManager.LoadScene(current);
    }
}
