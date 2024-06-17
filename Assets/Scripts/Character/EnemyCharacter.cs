using UnityEngine;
using System;
using UnityEngine.Events;
public class EnemyCharacter : MonoBehaviour
{
    private Animator animator;
    public Character Character { get; private set; }

    [Header("데미지표시")]
    [SerializeField] private GameObject damageTextPrefab;

    private StageManager stageManager; // 스테이지 매니저
    private DropManager dropManager; // 드랍 매니저

    public static event Action OnEnemyDestroyed;
    public event Action OnDeath; // 적 사망 이벤트
    public UnityEvent onDead;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        Character = GetComponent<Character>();
        stageManager = StageManager.Instance;
        dropManager = DropManager.Instance;
    }

    public void TakeDamage(float damage)
    {
        Character.CurrentHealth -= damage;
        ShowDamageText(damage);
        if (Character.CurrentHealth <= 0)
        {
            OnDeath?.Invoke(); // 사망 이벤트 트리거
            OnEnemyDestroyed?.Invoke(); // 적 파괴 이벤트 트리거
            stageManager.IncrementStageProgress(); // 스테이지 진행도 증가
            dropManager.DropRewards(PlayerCharacter.Instance); // 적이 죽을 때 보상 드랍
            onDead?.Invoke();
        }
    }
    private void ShowDamageText(float damage)
    {
        if (damageTextPrefab != null)
        {
            // 적의 월드 좌표를 화면 좌표로 변환
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
            // Canvas를 찾고, 그 안에 데미지 텍스트를 생성하여 표시
            Canvas canvas = FindObjectOfType<Canvas>();
            GameObject damageTextObject = Instantiate(damageTextPrefab, canvas.transform);
            damageTextObject.transform.position = screenPosition;

            DamageText damageText = damageTextObject.GetComponent<DamageText>();
            damageText.Initialize(damage);
        }
    }
}
