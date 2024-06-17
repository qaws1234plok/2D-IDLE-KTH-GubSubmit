using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    public bool IsAutoGoldActive { get; private set; }
    public bool IsAutoAttackMode {  get; private set; }

    [SerializeField] private GameObject destroyButtonAutoGoldBtn;
    [SerializeField] private GameObject destroyButtonAutoAttackBtn;

    protected override void Awake()
    {
        base.Awake();
    }

    public void BuyAutoGoldMachine()
    {
        if (PlayerCharacter.Instance.GetGold() >= 30000)
        {
            PlayerCharacter.Instance.AddGold(-30000);
            IsAutoGoldActive = true;
            Destroy(destroyButtonAutoGoldBtn);
        }
        else
        {
            ErrorMessageText.Instance.ShowErrorMessage("골드가 부족합니다.");
        }
    }

    public void BuyAutoAttackMod()
    {
        if (PlayerCharacter.Instance.GetGold() >= 300000)
        {
            PlayerCharacter.Instance.AddGold(-300000);
            IsAutoAttackMode = true;
            Destroy(destroyButtonAutoAttackBtn);
        }
        else
        {
            ErrorMessageText.Instance.ShowErrorMessage("골드가 부족합니다.");
        }
    }
}
