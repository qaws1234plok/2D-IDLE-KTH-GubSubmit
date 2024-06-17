using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[Serializable]
public class PlayerAnimationData
{
    [Header("평상시")]
    [SerializeField] private string noneParameterName = "@None";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string findParameterName = "Find";

    [Header("오토모드 활성화")]
    [SerializeField] private string autoAttackParameterName = "AutoAttack";

    // 애니메이션 파라미터 해시 값 저장
    public int NoneParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int FindParameterHash { get; private set; }
    public int AutoAttackParameterHash { get; private set; }
    public void Initialize()
    {
        // 파라미터 이름을 해시 값으로 변환
        NoneParameterHash = Animator.StringToHash(noneParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        FindParameterHash = Animator.StringToHash(findParameterName);
        AutoAttackParameterHash = Animator.StringToHash(autoAttackParameterName);
    }
}
