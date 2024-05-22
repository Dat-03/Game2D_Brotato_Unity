using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;
    int gameMode = 0;
    public int timer;
   

    private void Start()
    {
        gameMode = PlayerPrefs.GetInt("gameMode");
        StartCoroutine(StartTimer());
       
    }

    IEnumerator StartTimer()
    {
        int showTimer = 0;
       
        int maxTimer = 0;
        
        if (gameMode == 0) maxTimer = 100; // Chỉnh thời gian qua màn 
        int second, minute;
        while (true)
        {
            timer++;
            if (gameMode == 0)
            {
                showTimer = maxTimer - timer;
                if (timer >= maxTimer)
                {
                    SceneManager.LoadScene(3);  // chuyển qua màn khác ở đây 
                    yield break;
                }
            }
            else
            {
                showTimer = timer;
            }
            
            second = showTimer % 60;
            minute = (showTimer / 60) % 60;
            textTimer.text = minute.ToString() + ":" + second.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

  
}
