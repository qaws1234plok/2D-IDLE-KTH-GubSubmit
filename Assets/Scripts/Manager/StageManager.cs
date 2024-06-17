using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    [SerializeField] private TextMeshProUGUI currentStageNumText;
    public int stageNum;
    public Image stageClearbar;
    [SerializeField] private TextMeshProUGUI maxStageText;

    [SerializeField] private int maxStageNum;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        StageUpdate();
        stageClearbar.fillAmount = GetPercentage();
        stageClearUpdate();
    }

    public void StageUpdate()
    {
        currentStageNumText.text = "Stage " + stageNum;
    }

    public void stageClearUpdate()
    {
        maxStageText.text = (GetPercentage() * 100).ToString("F0") + "%";
    }

    public void IncrementStageProgress()
    {
        stageNum++;
    }

    private float GetPercentage()
    {
        return (float)stageNum / maxStageNum;
    }
}
