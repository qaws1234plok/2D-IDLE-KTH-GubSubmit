using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // 적 프리팹 배열
    [SerializeField] private Transform spawnPoint;  // 스폰 위치
    private GameObject currentEnemy;

    public static int EnemyCount { get; private set; } = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesAtIntervals());
    }

    private IEnumerator SpawnEnemiesAtIntervals()
    {
        while (true)
        {
            if (currentEnemy == null) // 현재 적이 없을 때만 새로운 적을 스폰
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(3f); // 1초 간격으로 체크
        }
    }

    public void SpawnEnemy()
    {
        if (currentEnemy == null)
        {
            EnemyCount++;
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            currentEnemy = Instantiate(enemyPrefabs[randomEnemyIndex],
                                       spawnPoint.position,
                                       spawnPoint.rotation);
            // 적이 죽었을 때 콜백을 설정
            EnemyCharacter enemyCharacter = currentEnemy.GetComponent<EnemyCharacter>();
            enemyCharacter.OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        if (currentEnemy != null)
        {
            // 적의 OnDeath 이벤트 해제
            EnemyCharacter enemyCharacter = currentEnemy.GetComponent<EnemyCharacter>();
            enemyCharacter.OnDeath -= OnEnemyDeath;
            Destroy(currentEnemy);
            EnemyCount--;
            currentEnemy = null;
        }
    }
}
