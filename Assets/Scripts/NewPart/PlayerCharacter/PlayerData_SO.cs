using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/CharacterData")]
public class PlayerData_SO : ScriptableObject
{
    public int id;
    public string charactorName;
    public int damage;
    public int currentHealth;
    public int maxHealth;

  

    // URL của API backend
    private string apiUrl = "http://localhost:3000/character";

    // Hàm để lấy dữ liệu nhân vật từ API dựa trên id_charactor
    public IEnumerator FetchCharacterDataById(int id_charactor, Action<PlayerData> callback)
    {
        string urlWithId = apiUrl + "?id_charactor=" + id_charactor; // Thêm id_charactor vào URL
        UnityWebRequest request = UnityWebRequest.Get(urlWithId);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to fetch character data: " + request.error);
        }
        else
        {
            // Phân tích phản hồi JSON
            string jsonResponse = request.downloadHandler.text;
            PlayerData characterData = JsonUtility.FromJson<PlayerData>(jsonResponse);

            // Gọi callback với dữ liệu nhân vật đã lấy được
            callback?.Invoke(characterData);
        }
    }
}
