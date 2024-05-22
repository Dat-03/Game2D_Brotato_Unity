using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<RawImage> listrawimage;
    public Texture imagelog, imageunlog, imgmission;
    public int idmission = 1;
    public int idskin = 0;
    public int idweapon = 0;
    public string username;
    private int coin = 10;

    void Start()
    {
        username = PlayerPrefs.GetString("username");
        idmission = PlayerPrefs.GetInt("id_mission");
        coin = PlayerPrefs.GetInt("coin");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < listrawimage.Count; i++){
            if(i<idmission-1) {
                listrawimage[i].texture = imageunlog;
            } else if(i>idmission-1){
                listrawimage[i].texture = imagelog;
            } else {
                listrawimage[i].texture = imgmission;
            }
        }
    }
    public void gomap(int i) {
        if(i == idmission) {
            PlayerPrefs.SetInt("mapchoose", i);
            PlayerPrefs.SetInt("isdomission", 1);
            PlayerPrefs.SetInt("idmission", idmission);
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.SetInt("id_mission", idmission);
            SceneManager.LoadScene(5);
        } else if(i < idmission) {
            PlayerPrefs.SetInt("mapchoose", i);
            PlayerPrefs.SetInt("isdomission", 0);
            PlayerPrefs.SetInt("idmission", idmission);
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.SetInt("id_mission", idmission);
            SceneManager.LoadScene(5);
        }
    }
}
