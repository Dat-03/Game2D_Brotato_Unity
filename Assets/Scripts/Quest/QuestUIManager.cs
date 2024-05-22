using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using SimpleJSON;
using Newtonsoft.Json;

public class QuestUIManager : MonoBehaviour
{
    public Text txt_monstersToKill;
    public Text txt_description;
    public Text txt_gold, txt_curentcoin;
    public Text txt_exp;
    public Text completed;
    public Button buildButton;
    public QuestManager questManager;
    public Button reloadButton; // Thêm nút reload
    public Killed killedCounter;
    private string baseUrl = "http://localhost:3000/quests/random";
    private string apiupdatecoin = "http://localhost:3000/users/coin";
    private int monstersKilled = 0;
    private QuestData questData;
    public TextMeshProUGUI txt_monstersKilled;
    public bool isupdate = false;
    public string nameEmnemy;

    public GameObject infomationQuest;
    private int killed, expReward, completedvalule, monstersToKill;
    public int goldReward;
    public string description;
    public bool isupdatevalue = true;
    public int coin;
    public bool isupdatetextcoin = false;
    private string username;
    private bool isupdatecoin = false;

    void Start()
    {
        coin = PlayerPrefs.GetInt("coin");
        username = PlayerPrefs.GetString("username");
        txt_curentcoin.text = coin+"";
        reloadButton.onClick.AddListener(ReloadQuest); // Thêm sự kiện cho nút reload
        UpdateUIWithQuestData();
        EnemyAI.EnemyKilled += UpdateKilled;
    }
    public void Update()
    {
        if(isupdatetextcoin) {
            isupdatetextcoin = false;
            isupdatecoin = true;
            StartCoroutine(updatecoin());
            txt_curentcoin.text = coin+"";
        }
        killed = infomationQuest.GetComponent<infomationQuest>().curentKilled;
        goldReward = infomationQuest.GetComponent<infomationQuest>().goldReward;
        expReward = infomationQuest.GetComponent<infomationQuest>().expReward;
        completedvalule = infomationQuest.GetComponent<infomationQuest>().completed;
        description = infomationQuest.GetComponent<infomationQuest>().description;
        monstersToKill = infomationQuest.GetComponent<infomationQuest>().monstersToKill;
        ReloadQuest();
    }

    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi object bị hủy
        EnemyAI.EnemyKilled -= UpdateKilled;
    }

    // Hàm được gọi khi có enemy bị giết
    void UpdateKilled()
    {
        txt_monstersKilled.text = EnemyAI.enemiesKilled.ToString();
        Debug.Log("name none");
        if (isupdate)
        {
            Debug.Log("name" + nameEmnemy);
            isupdate = false;
        }
    }
    IEnumerator updatecoin()
    {
       // 
        while(isupdatecoin)
        {
            Debug.Log("isupdatecoint nnnnn" + coin);
            isupdatecoin = false;
            yield return new WaitForSeconds(0.01f);
            JSONObject updateimData = new JSONObject();
            updateimData["username"] = username;
            updateimData["coin"] = coin;
            UnityWebRequest request = new UnityWebRequest(apiupdatecoin, "POST");
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
    public void UpdateMonstersKilled(int amount)
    {
        monstersKilled += amount;
        
    }

 
    void UpdateUIWithQuestData()
    {
        txt_monstersToKill.text = "Monsters to Kill: " + killed +"/"+ monstersToKill;
        txt_description.text = "Description: " + description;
        txt_gold.text = " " + goldReward;
        txt_exp.text = " " + expReward;
        completed.text = "Completed: " + (completedvalule == 1 ? "Yes" : "No");  
    }
    void updateques()
    {
        
        if(isupdatevalue) {
            if (isupdate)
            {
                Debug.Log("name" + nameEmnemy);
                isupdate = false;
            }
            txt_monstersToKill.text = "Monsters to Kill: " ;
            txt_description.text = "Description: ";
            txt_gold.text = " ";
            txt_exp.text = " " ;
            completed.text = "Completed: ";
        }
    }

    public void ReloadQuest()
    {
        UpdateUIWithQuestData();
    }

}

[System.Serializable]
public class QuestData
{
    public string description;
    public int monstersToKill;
    public int goldReward;
    public int expReward;
    public int completed;
}