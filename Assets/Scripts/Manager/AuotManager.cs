using UnityEngine;

public class AuotManager : SingletonMonoBehaviour<AuotManager>
{
    public bool IsAutoModeOn { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    public void AutoOnOffAbleCheck()
    {
        if (ShopManager.Instance.IsAutoAttackMode)
        {
            OnOff();
        }
        else
        {
            ErrorMessageText.Instance.ShowErrorMessage("오토 모드를 구입하세요");
        }
    }

    private void OnOff()
    {
        if (!IsAutoModeOn)
        {
            IsAutoModeOn = true;
            Debug.Log("오토모드 활성화");
            ErrorMessageText.Instance.ShowErrorMessage("오토모드 활성화");
        }
        else
        {
            IsAutoModeOn = false;
            ErrorMessageText.Instance.ShowErrorMessage("오토모드 비활성화");
        }
    }
}
