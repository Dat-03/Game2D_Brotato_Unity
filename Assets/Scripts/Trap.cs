using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 10; // Số lượng sát thương gây ra bởi bẫy

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gây sát thương cho nhân vật khi va chạm với bẫy
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDam(damage); // Trừ máu của người chơi
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamageEffect(damage); // Hiển thị hiệu ứng sát thương
                }
                Destroy(gameObject); // Phá hủy đối tượng bẫy
            }
        }
    }
}
