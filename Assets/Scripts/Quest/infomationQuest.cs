using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.PlayerLoop.PreUpdate;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class infomationQuest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelsubmit;
    public bool isupdate = false;
    public string name = "";
    public string username;
    public int enemy1 = 0, enemy2 = 0, enemy3 = 0, enemy4 = 0;
    private int zEnemy1 = 10, zEnemy2 = 20, zEnemy3 = 30, zEnemy4 = 40;
    public int idmission, monstersToKill, goldReward, expReward, completed;
    public string description;
    private string url = "http://localhost:3000/quest/";
    private string apimission = "";
    private string apiupdatemission = "http://localhost:3000/users/id_mission";
    private bool isgetmission = false;
    private bool isupdatemission = false;
    public int curentKilled = 0;
    private bool isnextmap;
    public GameObject panelnexmap;
    private int idmap, isdomission, idskin, idweapon;
    private int coinui;

    void Start()
    {
        idmission = PlayerPrefs.GetInt("id_mission");
        username = PlayerPrefs.GetString("username");
        Debug.Log("username " + username);
        apimission = url + idmission;
        isgetmission = true;
        StartCoroutine(getMission());
        isnextmap = false;
        idmap = PlayerPrefs.GetInt("mapchoose");
        isdomission = PlayerPrefs.GetInt("isdomission");
        idmission = PlayerPrefs.GetInt("idmission");
        idskin = PlayerPrefs.GetInt("idskin");
        idweapon = PlayerPrefs.GetInt("idweapon");
    }

    // Update is called once per frame
    void Update()
    {
        if (isupdate && isnextmap == false)
        { 
            if (name == "Enemy1" && idmission == 1 && enemy1 < monstersToKill)
            {
                enemy1++;
                curentKilled = enemy1;
            }
            else if (name == "Enemy1" && idmission == 1 && enemy1 == monstersToKill)
            {
                idmission++;
                isnextmap = true;
                isupdatemission = true;
                StartCoroutine(UpdateMission());
            }
            if (name == "Enemy2" && idmission == 2 && enemy2 < monstersToKill)
            {
                enemy2++;
                curentKilled = enemy2;
                Debug.Log("nv2");
            }
            else if (name == "Enemy2" && idmission == 2 && enemy2 == monstersToKill)
            {
                idmission++;
                isnextmap = true;
                isupdatemission = true;
                StartCoroutine(UpdateMission());
            }
            if (name == "Enemy3" && idmission == 3 && enemy3 < monstersToKill)
            {
                enemy3++;
                curentKilled = enemy3;
            }
            else if (name == "Enemy3" && idmission == 3 && enemy3 == monstersToKill)
            {
                idmission++;
                isnextmap = true;
                isupdatemission = true;
                StartCoroutine(UpdateMission());
            }
            if (name == "Enemy4" && idmission == 4 && enemy4 < monstersToKill)
            {
                enemy4++;
                curentKilled = enemy4;
            }
            else if (name == "Enemy4" && idmission == 4 && enemy4 == monstersToKill)
            {
                idmission++;
                isnextmap = true;
                isupdatemission = true;
                StartCoroutine(UpdateMission());
            }
            isupdate = false;

        }
        if(panelnexmap == false){
            panelnexmap.SetActive(false);
        } else {
            panelnexmap.SetActive(true);
        }
    }
    IEnumerator getMission()
    {
        // 
        while (isgetmission)
        {
            isgetmission = false;
            yield return new WaitForSeconds(1f);
            JSONObject updateimData = new JSONObject();
            UnityWebRequest request = new UnityWebRequest(apimission, "PATCH");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(updateimData.ToString());
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {

            }
            else
            {
                string responseText = request.downloadHandler.text;
                // Lấy các giá trị từ JSON.
                JSONNode jsonData = JSON.Parse(responseText);
                Debug.Log("Error This here !!!!!");
                monstersToKill = int.Parse(jsonData["monstersToKill"].Value);
                description = jsonData["description"].Value;
                goldReward = int.Parse(jsonData["goldReward"].Value);
                expReward = int.Parse(jsonData["expReward"].Value);
                completed = int.Parse(jsonData["completed"].Value);
            }
        }

    }
    IEnumerator UpdateMission()
    {
        while (isupdatemission)
        {
            GameObject quesui = GameObject.Find("Quest_mission");
            coinui = quesui.GetComponent<QuestUIManager>().coin;
            coinui = coinui+goldReward;
            quesui.GetComponent<QuestUIManager>().coin = coinui;
            quesui.GetComponent<QuestUIManager>().isupdatetextcoin = true;
            panelsubmit.SetActive(true);
            isupdatemission = false;
            GameObject missionnextmap = GameObject.Find("nextmapcontroller");
            missionnextmap.GetComponent<NextMapController>().idmission = idmission;
            yield return new WaitForSeconds(1f);
            JSONObject updateimData = new JSONObject();
            updateimData["username"] = username;
            updateimData["id_mission"] = idmission;
            UnityWebRequest request = new UnityWebRequest(apiupdatemission, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(updateimData.ToString());
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {

            }
            else
            {
                Time.timeScale = 0f;
                apimission = url + idmission;
                isgetmission = true;
                StartCoroutine(getMission());
                
            }
        }
    }
    public void onNextMap() {
        PlayerPrefs.SetInt("mapchoose", idmission);
        PlayerPrefs.SetInt("isdomission", idmission);
        PlayerPrefs.SetInt("idmission", idmission);
        PlayerPrefs.SetInt("idskin", idskin);
        PlayerPrefs.SetInt("idweapon", idweapon);
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("id_mission", idmission);
        PlayerPrefs.SetInt("coin", coinui);
        //id user
        string current = "Map"+idmission;
        SceneManager.LoadScene(current);
    }
}
