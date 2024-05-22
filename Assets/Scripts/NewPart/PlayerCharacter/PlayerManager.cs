using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public PlayerData_SO characterDataSO;

    IEnumerator Start()
    {
        // Gửi yêu cầu GET đến API
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/charactor"))
        {
            // Chờ dữ liệu được trả về từ API
            yield return www.SendWebRequest();

            // Kiểm tra nếu có lỗi xảy ra
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse dữ liệu JSON từ API thành đối tượng PlayerData
                string json = www.downloadHandler.text;
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

                // Cập nhật đối tượng PlayerData_SO với dữ liệu đã lấy được
                characterDataSO.id = playerData.id;
                characterDataSO.charactorName = playerData.charactorName;
                characterDataSO.damage = playerData.damage;
                characterDataSO.currentHealth = playerData.currentHealth;
                characterDataSO.maxHealth = playerData.maxHealth;

                Debug.Log("Player data loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to load player data: " + www.error);
            }
        }
    }
}
