using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public float startTimeBtwSpawn;
    private float timeBtwSpawn;

    public GameObject[] enemies;
    public Spawner[] spawners; // Sửa kiểu dữ liệu từ List thành Spawner[]

    public WeaponManager weaponManager;

    private Player player;
    int maxEnemy = 5;
    int roundCount = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            int randEnemyCount = UnityEngine.Random.Range(2, maxEnemy);
            if (weaponManager.Enemies.Count <= 5)
                randEnemyCount = UnityEngine.Random.Range(maxEnemy - 2, maxEnemy);

            List<int> randomIndex = GetRandomIndices(spawners.Length, randEnemyCount);

            foreach (int index in randomIndex)
            {
                int randEnemy = UnityEngine.Random.Range(0, enemies.Length);
                spawners[index].spawnEnemy(enemies[randEnemy]); 
            }
            timeBtwSpawn = startTimeBtwSpawn;

            roundCount++;

            // Kiểm tra nếu người chơi đạt mỗi 10 cấp độ, tăng số lượng enemy
            if (roundCount % 10 == 0)
            {
                maxEnemy++; // Hoặc có thể tăng giá trị khác nếu muốn
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    // Phương thức này không cần thay đổi, vì nó được sử dụng để lấy ra các chỉ số ngẫu nhiên từ một danh sách indices
    public List<int> GetRandomIndices(int n, int k)
    {
        List<int> allIndices = new List<int>();
        for (int i = 0; i < n; i++)
        {
            allIndices.Add(i);
        }

        List<int> randomIndices = new List<int>();

        int remainingItems = n;
        for (int i = 0; i < k; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, remainingItems);
            randomIndices.Add(allIndices[randomIndex]);

            allIndices[randomIndex] = allIndices[remainingItems - 1];
            remainingItems--;
        }

        return randomIndices;
    }
}
