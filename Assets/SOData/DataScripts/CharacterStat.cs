using UnityEngine;


[CreateAssetMenu(menuName = "캐릭터 능력치 생성", fileName = "능력치", order = 1)]
public class CharacterStat : ScriptableObject
{
    public string characterName;
    public float maxHealth;
    public float currentHealth;
    public float attackPower;
}
