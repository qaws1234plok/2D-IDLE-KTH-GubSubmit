using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/GameObjectTarget", fileName = "GameObjectTarget")] 
public class GameObjectTarget : TaskTarget
{
    [SerializeField] private GameObject value;

    public override object Value => value;

    // 게임오브젝트의 이름으로 비교중, 추후 더 나은 비교방식이 있는지를 찾을 것
    public override bool IsEqual(object target)
    {
        var targetAsGameObject = target as GameObject;
        if (targetAsGameObject == null)
        {
            return false;
        }
        return targetAsGameObject.name.Contains(value.name);
    }
}
