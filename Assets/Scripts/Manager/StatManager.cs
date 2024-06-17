using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class StatManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentStatText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] Image expBar;
    [SerializeField] Image healthBar;
    [SerializeField] private TextMeshProUGUI upgradeGold;
    [SerializeField] private TextMeshProUGUI autoUpgradeGold;
    [SerializeField] Image goldAutoBar;

    private int upgradeCount { get => PlayerCharacter.Instance.upgradeCount; set => PlayerCharacter.Instance.upgradeCount = value; }
    private int autoUpgradeCount { get => PlayerCharacter.Instance.autoUpgradeCount; set => PlayerCharacter.Instance.autoUpgradeCount = value; }
    private int autoGold = 1000;
    private float autoGoldInterval = 5.0f;
    public ParticleSystem EffectParticle;

    private void Awake()
    {
        EffectParticle = GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartCoroutine(AutoGoldProduction());
        Debug.Log(upgradeCount);
    }

    private void Update()
    {
        UpdateStat();
        expBar.fillAmount = GetExperiencePercentage();
        healthBar.fillAmount = GetHealthPercentage();
    }

    private void UpdateStat()
    {
        currentStatText.text = $"Name: {PlayerCharacter.Instance.Character.CharacterName}\n" +
                               $"Level: {PlayerCharacter.Instance.GetLevel()}\n" +
                               $"Max Health: {PlayerCharacter.Instance.Character.MaxHealth}\n" +
                               $"Current Health: {PlayerCharacter.Instance.Character.CurrentHealth}\n" +
                               $"Attack Power: {PlayerCharacter.Instance.Character.AttackPower}";

        goldText.text = $"Gold: {PlayerCharacter.Instance.GetGold().ToString("N0")}G";

        upgradeGold.text = $"공격력 업그레이드\n {(100 * upgradeCount)}G 소모 ";
        autoUpgradeGold.text = $"자동 골드 업그레이드\n {(100 * autoUpgradeCount)}G 소모 ";

    }
    private float GetHealthPercentage()
    {
        return PlayerCharacter.Instance.Character.CurrentHealth / PlayerCharacter.Instance.Character.MaxHealth;
    }
    private float GetExperiencePercentage()
    {
        return (float)PlayerCharacter.Instance.GetCurrentExperience() / PlayerCharacter.Instance.GetExperienceToNextLevel();
    }

    public void AttackPowerUpgrade()
    {
        if (PlayerCharacter.Instance.GetGold() >= 100 * upgradeCount)
        {
            float upgradePower = PlayerCharacter.Instance.Character.AttackPower * 10f;
            PlayerCharacter.Instance.Character.AttackPower = upgradePower;
            PlayerCharacter.Instance.AddGold(-100 * upgradeCount);
            upgradeCount++;
            AudioManager.Instance.PlayerUpgradeSound();

            ParticleSystem particleSystem = EffectParticle;
            particleSystem.transform.position = PlayerCharacter.Instance.GetComponent<PlayerCharacter>().transform.position;
            ParticleSystem.EmissionModule em = particleSystem.emission;
            em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(5)));
            ParticleSystem.MainModule mm = particleSystem.main;
            mm.startSpeedMultiplier = 10f;
            particleSystem.Play();

            StartCoroutine(StopParticleSystemAfterDelay(particleSystem, 1f)); // 1초 후에 파티클 시스템 정지
        }
        else
        {
            ErrorMessageText.Instance.ShowErrorMessage("골드가 부족합니다.");
        }
    }

    public void UpgradeAutoGold()
    {
        int upgradeCost = 100 * autoUpgradeCount;
        if (PlayerCharacter.Instance.GetGold() >= upgradeCost && ShopManager.Instance.IsAutoGoldActive)
        {
            PlayerCharacter.Instance.AddGold(-upgradeCost);
            autoUpgradeCount++;
        }
        else
        {
            ErrorMessageText.Instance.ShowErrorMessage("골드가 부족합니다.");
        }
        if (!ShopManager.Instance.IsAutoGoldActive)
        {
            ErrorMessageText.Instance.ShowErrorMessage("자동 골드 모드를 구입하세요.");
        }
    }

    IEnumerator AutoGoldProduction()
    {
        while (true)
        {
            if (ShopManager.Instance.IsAutoGoldActive)
            {
                float elapsedTime = 0f;
                while (elapsedTime < autoGoldInterval)
                {
                    elapsedTime += Time.deltaTime;
                    goldAutoBar.fillAmount = elapsedTime / autoGoldInterval;
                    yield return null;
                }
                PlayerCharacter.Instance.AddGold(autoGold * autoUpgradeCount);
            }
            else
            {
                goldAutoBar.fillAmount = 0;
                yield return null;
            }
        }
    }

    IEnumerator StopParticleSystemAfterDelay(ParticleSystem particleSystem, float delay)
    {
        yield return new WaitForSeconds(delay);
        particleSystem.Stop();
    }
}
