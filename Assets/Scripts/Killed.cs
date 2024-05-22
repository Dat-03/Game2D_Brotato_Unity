using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class Killed : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int currentKilled = 0;
    private string apiupdate = "http://localhost:3000/quest/66013a2966e4b4b766efd955";
    public bool isdead = false;
    public string nameEnemy;


    private void Start()
    {
        text.text = "0";
        // Đăng ký sự kiện khi enemy bị giết
        EnemyAI.EnemyKilled += UpdateKilled;
        StartCoroutine(UpdateKilledd());

    }
    private void Update()
    {
        if(isdead)
        {
            Debug.Log(nameEnemy);
            isdead = false;
        }
    }
    IEnumerator UpdateKilledd()
    {
       // 
        while(true)
        {
            
            yield return new WaitForSeconds(1f);
            JSONObject updateimData = new JSONObject();
            updateimData["description"] = "Kill Enemy 3";
            updateimData["monstersToKill"] = 10;
            updateimData["goldReward"] = 10;
            updateimData["expReward"] = 10;
            updateimData["completed"] = 10;
            UnityWebRequest request = new UnityWebRequest(apiupdate, "PATCH");
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
                // network error
            }
        }
        
    }

    public void UpdateKilled()
    {
        currentKilled++;
        text.text = EnemyAI.enemiesKilled.ToString();
    }
    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi object bị hủy
        EnemyAI.EnemyKilled -= UpdateKilled;
    }
}
