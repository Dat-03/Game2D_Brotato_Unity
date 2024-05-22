using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer2 : MonoBehaviour
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

        if (gameMode == 0) maxTimer = 10; // Chỉnh thời gian qua màn 
        int second, minute;
        while (true)
        {
            timer++;
            if (gameMode == 0)
            {
                showTimer = maxTimer - timer;
                if (timer >= maxTimer)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 3)
                    {
                        PlayerPrefs.SetInt("PassedScene3", 1); // Lưu trạng thái đã qua Scene 3
                        PlayerPrefs.Save();
                    }
                    SceneManager.LoadScene(4);  // chuyển qua màn khác ở đây 
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
