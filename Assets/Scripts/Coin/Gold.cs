using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public Text text;
    public int currentGold = 10; 
    public bool isrot;
    private int coin;

    private void Start()
    {
        coin = PlayerPrefs.GetInt("coin");
        if(!isrot){
            text.text = coin+"";
        }
        text.text = coin+"";
        Invoke("DestroySelf", 3f);
    }

    public void Update()
    {
        // text.text = currentGold.ToString();
    }
    private void DestroySelf()
    {
        if(isrot) {
            GameObject ggold = GameObject.Find("Count_Gold");
            int gold1 = ggold.GetComponent<Gold>().currentGold;
            ggold.GetComponent<Gold>().currentGold = gold1+10;
            Destroy(gameObject);
        }
    }
}