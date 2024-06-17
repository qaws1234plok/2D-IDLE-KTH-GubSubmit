using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    public bool IsAutoGoldActive
    {
        get { return PlayerCharacter.Instance.IsAutoGoldActive; }
        private set { PlayerCharacter.Instance.IsAutoGoldActive = value; }
    }
    public bool IsAutoAttackMode
    {
        get { return PlayerCharacter.Instance.IsAutoAttackMode; }
        private set { PlayerCharacter.Instance.IsAutoAttackMode = value; }
    }

    [SerializeField] private GameObject destroyButtonAutoGoldBtn;
    [SerializeField] private GameObject destroyButtonAutoAttackBtn;

    protected override void Awake()
    {
        base.Awake();
    }

    public void BuyAutoGoldMachine()
    {
        if (PlayerCharacter.Instance.IsAutoGoldActive)
        {
            ErrorMessageText.Instance.ShowErrorMessage("이미 구입 했습니다");
            destroyButtonAutoGoldBtn.SetActive(false); // 버튼 비활성화
            return;
        }

        if (!PlayerCharacter.Instance.IsAutoGoldActive && PlayerCharacter.Instance.GetGold() >= 30000)
        {
            PlayerCharacter.Instance.AddGold(-30000);
            IsAutoGoldActive = true;
            destroyButtonAutoGoldBtn.SetActive(false); // 버튼 비활성화
        }
        else
        {
            ErrorMessageText.Instance.ShowErrorMessage("골드가 부족합니다.");
        }
    }

    public void BuyAutoAttackMod()
    {
        if (PlayerCharacter.Instance.IsAutoAttackMode)
        {
            ErrorMessageText.Instance.ShowErrorMessage("이미 구입 했습니다");
            destroyButtonAutoAttackBtn.SetActive(false); // 버튼 비활성화
            return;
        }

        if (!PlayerCharacter.Instance.IsAutoAttackMode && PlayerCharacter.Instance.GetGold() >= 300000)
        {
            PlayerCharacter.Instance.AddGold(-300000);
            IsAutoAttackMode = true;
            destroyButtonAutoAttackBtn.SetActive(false); // 버튼 비활성화
        }
        else
        {
            ErrorMessageText.Instance.ShowErrorMessage("골드가 부족합니다.");
        }
    }
}
