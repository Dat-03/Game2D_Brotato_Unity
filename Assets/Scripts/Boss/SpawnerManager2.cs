using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager2 : MonoBehaviour
{
    public float startTimeBtwSpawn;
    private float timeBtwSpawn;

    public GameObject[] enemies;
    public Spawner[] spawners;

    public WeaponManager weaponManager;

    private Player player;
    int maxEnemy = 5;
    int roundCount = 0;

    private bool hasSpawnedEnemy = false; // Biến để kiểm tra đã spawn Enemy chưa

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0 && !hasSpawnedEnemy)
        {
            if (spawners.Length > 0)
            {
                // Chọn ngẫu nhiên một spawner
                int randIndex = Random.Range(0, spawners.Length);

                // Chọn ngẫu nhiên một loại Enemy
                int randEnemyIndex = Random.Range(0, enemies.Length);

                // Spawn một Enemy tại spawner được chọn
                spawners[randIndex].spawnEnemy(enemies[randEnemyIndex]);

                hasSpawnedEnemy = true; // Đã spawn Enemy

                roundCount++;

                // Kiểm tra nếu người chơi đạt mỗi 10 cấp độ, tăng số lượng enemy
                if (roundCount % 10 == 0)
                {
                    maxEnemy++; // Hoặc có thể tăng giá trị khác nếu muốn
                }
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;

            if (timeBtwSpawn <= 0)
            {
                // Reset biến hasSpawnedEnemy để cho phép spawn Enemy trong lần Update tiếp theo
                hasSpawnedEnemy = false;
                timeBtwSpawn = startTimeBtwSpawn; // Reset thời gian chờ
            }
        }
    }
}
