using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trsfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Update()
    {
        // Kiểm tra xem player có tồn tại không
        if (player != null)
        {
            // Tạo một Vector3 mới từ vị trí của player và gán cho transform.position
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        else
        {
            // Hiển thị cảnh báo nếu player không tồn tại
            Debug.LogWarning("Player object is not assigned.");
        }
    }
}
