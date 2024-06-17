using UnityEngine;

public class DropManager : SingletonMonoBehaviour<DropManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        EnemyCharacter.OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void OnDisable()
    {
        EnemyCharacter.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    private void HandleEnemyDestroyed()
    {
        // 플레이어 캐릭터를 찾고, 보상 드랍 메서드 호출
        if (PlayerCharacter.Instance != null)
        {
            DropRewards(PlayerCharacter.Instance);
        }
    }

    public void DropRewards(PlayerCharacter player)
    {
        int goldAmount = CalculateGold();
        int experienceAmount = CalculateExperience();

        player.AddGold(goldAmount);
        player.AddExperience(experienceAmount);
    }


    private int CalculateGold()
    {
        // 스테이지 레벨에 비례하여 최소치와 최대치 설정
        int minGold = StageManager.Instance.stageNum * 50;
        int maxGold = StageManager.Instance.stageNum * 100;

        return Random.Range(minGold, maxGold);
    }

    private int CalculateExperience()
    {
        int minExp = StageManager.Instance.stageNum * 10;
        int maxExp = StageManager.Instance.stageNum * 20;

        return Random.Range(minExp, maxExp);
    }
}
