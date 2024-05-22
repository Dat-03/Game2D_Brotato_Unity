using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor.U2D.Animation;

public class UISelectCharacterPanel : MonoBehaviour
{
    [Header("MENUS")]
    public GameObject canv_Main;
    public GameObject canv_SelectCharacter;

    public GameObject selectHightlight;
    public List<GameObject> Characters;
    public List<GameObject> Weapons;

    public AllPlayerData_SO AllPlayerData_SO;
    public List<PlayerData> playerDatas = new List<PlayerData>();
    public List<GunData> gunDatas = new List<GunData>();
    public AllGunDatas AllGun;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI health;
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI gunPrice;
    public GameObject StartGameButton;

    // URL của API backend
    private string apiUrl = "http://localhost:3000/character"; // Điều chỉnh URL tùy thuộc vào đường dẫn thực tế của bạn

    private void Start()
    {
        ClosePanel(canv_SelectCharacter);
        StartCoroutine(FetchCharacterData());
    }

    // Coroutine để lấy dữ liệu nhân vật từ API backend
    private IEnumerator FetchCharacterData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to fetch character data: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            AllPlayerData_SO = JsonUtility.FromJson<AllPlayerData_SO>(jsonResponse);
            AllGun = JsonUtility.FromJson<AllGunDatas>(jsonResponse);



            // Sau khi cập nhật dữ liệu, hiển thị nhân vật đầu tiên
            SelectCharacter(0);
            SelectWeapon(0);
        }
    }

    public void ClosePanel(GameObject panel)
    {
        CanvasGroup group = panel.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
    }

    public void OpenPanel(GameObject panel)
    {
        CanvasGroup group = panel.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
    }

    public void OpenSelectCharacterCanvas()
    {
        OpenPanel(canv_SelectCharacter);
        canv_Main.SetActive(false);
    }

    public void SelectMode(int index)
    {
        PlayerPrefs.SetInt("gameMode", index);
    }

    public void SelectCharacter(int index)
    {
        Debug.Log(Characters[index].transform.position);
        selectHightlight.transform.position = Characters[index].transform.position;
        PlayerPrefs.SetInt("characterPreference", index);

        if (index < playerDatas.Count && playerDatas[index] != null)
        {

            health.text = playerDatas[index].maxHealth.ToString();
            damage.text = playerDatas[index].damage.ToString();
            nameText.text = playerDatas[index].charactorName;
            StartGameButton.SetActive(true);
        }
        else
        {
            StartGameButton.SetActive(false);
            nameText.text = "Locked";
            health.text = "0";
            damage.text = "0";
        }
    }

    public void SelectWeapon(int index)
    {
        Debug.Log(Weapons[index].transform.position);
        selectHightlight.transform.position = Weapons[index].transform.position;
        PlayerPrefs.SetInt("gunPreference", index);

        if (index < gunDatas.Count && gunDatas[index] != null)
        {

            gunPrice.text = gunDatas[index].gunPrice.ToString();
            gunName.text = gunDatas[index].gunName;
            StartGameButton.SetActive(true);
        }
        else
        {
            StartGameButton.SetActive(false);
            gunName.text = "Locked";
            gunPrice.text = "1000";
           
        }
    }
}
