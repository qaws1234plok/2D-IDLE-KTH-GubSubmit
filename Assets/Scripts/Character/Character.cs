using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string characterName;
    private float maxHealth;
    private float currentHealth;
    private float attackPower;

    [SerializeField] private CharacterStat stat;

    public string CharacterName
    {
        get { return characterName; }
    }
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public float AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }


    private void Awake()
    {
        StatInitialize();
    }

    private void StatInitialize()
    {
        if (stat != null)
        {
            // 캐릭터 스텟 초기화
            characterName = stat.characterName;
            maxHealth = stat.maxHealth;
            currentHealth = stat.maxHealth;
            attackPower = stat.attackPower;
        }
    }

    private void Update()
    {
    }
}
