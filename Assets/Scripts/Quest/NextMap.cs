using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{
    // Start is called before the first frame update
    private int mapchoose,isdomission,idmission;
    public string username;
    void Start()
    {
        idmission = PlayerPrefs.GetInt("id_mission");
        username = PlayerPrefs.GetString("username");
        mapchoose = PlayerPrefs.GetInt("mapchoose");
        isdomission = PlayerPrefs.GetInt("isdomission");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onnextmap(){
        PlayerPrefs.SetInt("id_mission",idmission);
        PlayerPrefs.SetString("username",username);
        PlayerPrefs.SetInt("mapchoose",mapchoose);  
        PlayerPrefs.SetInt("isdomission",isdomission);
        PlayerPrefs.SetInt("idmission",idmission);
        SceneManager.LoadScene(6);
    }
}
