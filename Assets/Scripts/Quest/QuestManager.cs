using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class QuestManager : MonoBehaviour
{
    private string baseUrl = "http://localhost:3000/quests/random";


  

    // Hàm này gửi yêu cầu GET đến endpoint /quests/random và xử lý phản hồi
    public IEnumerator GetRandomQuestCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                yield break;
            }

            string jsonResponse = www.downloadHandler.text;
            // Xử lý dữ liệu JSON tại đây (ví dụ: chuyển đổi thành đối tượng nhiệm vụ và hiển thị trên UI)
            Debug.Log("Received quest data: " + jsonResponse);
        }
    }
}
