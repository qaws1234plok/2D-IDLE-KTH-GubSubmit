
using UnityEngine;

[CreateAssetMenu(menuName = "캐릭터 능력치 생성", fileName = "플레이어능력치", order = 2)]
public class PlayerStat : CharacterStat
{
    public int level;
    public int gold;
}