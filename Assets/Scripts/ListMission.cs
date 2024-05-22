using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.PreUpdate;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ListMission : MonoBehaviour
{
    // Start is called before the first frame update
    private string baseUrl = "http://localhost:3000/quest/";
    public Text text_monstersToKill;
    public Text text_description;
    public Text text_gold;
    public Text text_exp;
    public int list_monstersToKill;
    public string list_description = "";
    public int list_gold;
    public int list_exp;
    public bool isgetmission = false;
    public int idmission;
    public string username, url;
    public GameObject Panel;
    private int coin = 10;



    void Start()
    {
        idmission = PlayerPrefs.GetInt("id_mission");
        username = PlayerPrefs.GetString("username");
        coin = PlayerPrefs.GetInt("coin");
        url = baseUrl + idmission;
        Debug.Log(url);
        isgetmission = true;
        StartCoroutine(getMission());
    }
    IEnumerator getMission()
    {

        while (isgetmission)
        {
            isgetmission = false;
            yield return new WaitForSeconds(1f);
            JSONObject updateimData = new JSONObject();
            UnityWebRequest request = new UnityWebRequest(url, "PATCH");
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
                list_monstersToKill = int.Parse(jsonData["monstersToKill"].Value);
                list_description = jsonData["description"].Value;
                list_gold = int.Parse(jsonData["goldReward"].Value);
                list_exp = int.Parse(jsonData["expReward"].Value);
                UpdateTech();
            }
        }

    }

    public void UpdateTech()
    {
        text_description.text =  "Misson: "+ list_description ;
        text_monstersToKill.text =   "Killed: " + list_monstersToKill;
        text_gold.text = list_gold + "";
        text_exp.text = list_exp + "";
    }
    public void onOpenPanel()
    {
        Panel.SetActive(true);
    }
    public void onClosePanel()
    {
        Panel.SetActive(false);
    }
    void Update()
    {
        UpdateTech();


    }
    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
    public void LoadScene2()
    {
        Debug.Log("id" + idmission);
        PlayerPrefs.SetInt("id_mission", idmission);
        SceneManager.LoadScene(5);
    }
    public void SceneShop()
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("id_mission", idmission);
        PlayerPrefs.SetInt("coin", coin);
        SceneManager.LoadScene(7);
    }

}