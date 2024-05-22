using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using SimpleJSON;

public class ShopManager : MonoBehaviour
{
    public int coins = 500;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public bool isSkin;
    public Button[] myPurchaseBtns;
    public List<RawImage> imagesList;
    public int idSkinUesed = 1;
    public int idWeaponUesed = 1;
    public string username;
    public int id_mission;
    private int mapchoose;
    private int isdomission;
    private int coin = 10;
    private bool isupdatecoin = false;
    private string apiupdatecoin = "http://localhost:3000/users/coin";
    private void Start() 
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        LoadPanels();
        coins = 500;
        username = PlayerPrefs.GetString("username");
        id_mission = PlayerPrefs.GetInt("id_mission");
        mapchoose = PlayerPrefs.GetInt("mapchoose");  
        isdomission = PlayerPrefs.GetInt("isdomission");
        coin = PlayerPrefs.GetInt("coin");
        coinUI.text = coin.ToString();
    }

    private void Update()
    {
        
    }
    public void AddCoins()
    {
        coins++;
        coinUI.text = coins.ToString();
     
    }
    /*public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (coins >= shopItemSO[i].baseCast)
                myPurchaseBtns[i].interactable = true;
            else
                myPurchaseBtns[i].interactable = false;
        }
    }*/

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemSO[i].description;
            shopPanels[i].coinTxt.text = shopItemSO[i].baseCast.ToString();
            RawImage rawImage = shopPanels[i].GetComponentInChildren<RawImage>();
            if (rawImage != null)
            {
                rawImage.texture = shopItemSO[i].image.texture;
            }
            else
            {
                Debug.LogWarning("RawImage not found in ShopTemplate!");
            }
        }
    }



    public void PurchaseItem(int btnNo)
    {
        if (coin >= shopItemSO[btnNo].baseCast)
        {
            
            coin -= shopItemSO[btnNo].baseCast;
            coinUI.text = coin.ToString();
            shopItemSO[btnNo].statusBuy = true;
            isupdatecoin = true;
            StartCoroutine(updatecoin());
        }
    }
    IEnumerator updatecoin()
    {
       // 
        while(isupdatecoin)
        {
            isupdatecoin = false;
            yield return new WaitForSeconds(0.1f);
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
        }
        
    }

    public void OnUsedSkin(int id)
    {
        idSkinUesed = id;
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (shopItemSO[i].id == idSkinUesed && shopItemSO[i].isSkin == true)
            {
                shopPanels[i].textEquipment.text = "Used";
            }
            else
            {
                if (shopItemSO[i].isSkin)
                    shopPanels[i].textEquipment.text = "Use";
            }
        }
    }
    public void OnUsedWeapon(int id)
    {
        idWeaponUesed = id;
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (shopItemSO[i].id == idWeaponUesed && shopItemSO[i].isSkin == false)
            {
                shopPanels[i].textEquipment.text = "Used";
            }
            else
            {
                if(!shopItemSO[i].isSkin)
                {
                    shopPanels[i].textEquipment.text = "Use";
                }
                
            }
        }
     }

    public void SceneBack()
    {
        SceneManager.LoadScene(1);
    }

    public void ScenePlay()
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("id_mission", id_mission);
        PlayerPrefs.SetInt("idskin", idSkinUesed);
        PlayerPrefs.SetInt("idweapon", idWeaponUesed);
        PlayerPrefs.SetInt("mapchoose", mapchoose);
        PlayerPrefs.SetInt("isdomission", isdomission);
        PlayerPrefs.SetInt("coin", coin);
        string map = "Map" + mapchoose;
        SceneManager.LoadScene(map);
    }
}
