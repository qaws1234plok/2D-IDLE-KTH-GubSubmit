using System.Numerics;
using UnityEngine;

public class AnimationEventInvoker : MonoBehaviour
{
    private EnemyCharacter enemyCharacter;

    private void Start()
    {
        enemyCharacter = GetComponentInParent<EnemyCharacter>();
    }

    public void InvokeAttackEnemy()
    {
        // 현재 필드에 존재하는 적을 찾음
        var enemy = FindObjectOfType<EnemyCharacter>();
        if (enemy != null)
        {
            // 부모 PlayerCharacter의 공격력을 가져와서 적에게 데미지를 줌
            BigInteger attackPower = PlayerCharacter.Instance.Character.AttackPower;
            enemy.TakeDamage(attackPower);
            AudioManager.Instance.PlayerAttackSound();
        }
    }

    public void InvokeStopAttack()
    {
        var stateMachine = PlayerCharacter.Instance.GetStateMachine();
        if (stateMachine.currentState is PlayerIdleState idleState)
        {
            idleState.SetAttackFalse();
        }
    }

    public void InvokeEnemyAttackisPlayer()
    {
        var player = FindObjectOfType<PlayerCharacter>();
        if ( player != null && enemyCharacter != null)
        {
            BigInteger enemyAttackPower = enemyCharacter.Character.AttackPower;
            player.TakeDamage(enemyAttackPower);
        }
    }
}
