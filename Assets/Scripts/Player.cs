using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int health = 20;
    public int exp = 40;
    public int gold = 1000;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public SpriteRenderer characterSR;
    Animator animator;

    public float dashBoost = 2f;
    private float dashTime;
    public float DashTime;
    private bool once;

    public Vector3 moveInput;
    private int count_Gold = 0;
    public TextMeshProUGUI txt_countGold;
    public GameObject damPopUp;
    public LosePanel losePanel;
    public GameObject pauseGame;
    private bool resumeGame = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Time.timeScale = 1;
    }
   
   
    void Update()
    {
        // Movement
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        //transform.position += moveSpeed * Time.deltaTime * moveInput;
        rb.velocity = moveInput * moveSpeed;
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.P))
        {
            showMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            animator.SetBool("Roll", true);
            moveSpeed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once)
        {
            animator.SetBool("Roll", false);
            moveSpeed -= dashBoost;
            once = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        // Rotate Face
        if (moveInput.x != 0)
        {
            if (moveInput.x < 0)
                characterSR.transform.localScale = new Vector3(-1, 1, 1);
            else
                characterSR.transform.localScale = new Vector3(1, 1, 1);
        }

        
    }

    public void TakeDamageEffect(int damage)
    {
        if (damPopUp != null)
        {
            GameObject instance = Instantiate(damPopUp, transform.position
                    + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
            Animator animator = instance.GetComponentInChildren<Animator>();
            animator.Play("red");
        }
        if (GetComponent<Health>().isDead)
        {
            losePanel.Show();
        }
    }

    public void showMenu()
    {
        if (resumeGame)
        {
            pauseGame.SetActive(true);
            Time.timeScale = 0;
            resumeGame = false;
        }
        else
        {
            pauseGame.SetActive(false);
            Time.timeScale = 1;
            resumeGame = true;
        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void Logout()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator SendBattleInfoToAPI()
    {
        string url = "http://localhost:3000/quests";
        BattleInfo battleInfo = new BattleInfo(health, exp, gold);
        string jsonData = JsonUtility.ToJson(battleInfo);

        using (UnityWebRequest www = UnityWebRequest.Put(url, jsonData))
        {
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Battle info sent to API successfully.");
            }
            else
            {
                Debug.LogError("Error sending battle info to API: " + www.error);
            }
        }
    }




    // Xử lý khi player va chạm với đồng xu vàng
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "gold")
        {
            count_Gold += 1;
            // txt_countGold.text = count_Gold + "";
            Destroy(collision.gameObject);
        }
        // Kiểm tra xem va chạm với đối tượng cục đá hay không
        else if (collision.CompareTag("TopLeftWall"))
        {
            Vector3 currentPosition = transform.position;
            Vector3 rockPosition = collision.transform.position;
            float offset = 0.1f; // Độ lệch để tránh va chạm liên tục

            // Đặt lại vị trí của player nếu nó va chạm với đá
            if (currentPosition.x < rockPosition.x)
                transform.position = new Vector3(rockPosition.x - offset, currentPosition.y, currentPosition.z);
            else
                transform.position = new Vector3(rockPosition.x + offset, currentPosition.y, currentPosition.z);
        }
    }


  

}

// Class để lưu thông tin về trận đấu (health, exp, gold)
[System.Serializable]


public class BattleInfo
{
    public int health;
    public int exp;
    public int gold;

    public BattleInfo(int health, int exp, int gold)
    {
        this.health = health;
        this.exp = exp;
        this.gold = gold;
    }
}
