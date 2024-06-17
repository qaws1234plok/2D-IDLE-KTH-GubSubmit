using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[System.Serializable]
public class PlayerData
{
    public string characterName;
    public float maxHealth;
    public float currentHealth;
    public float attackPower;
    public int currentExperience;
    public int currentLevel;
    public int currentGold;
    public int experienceToNextLevel;
}

public class PlayerCharacter : SingletonMonoBehaviour<PlayerCharacter>
{
    [field: Header("Anime들")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input {  get; private set; }
    public Character Character { get; private set; }
    [SerializeField] private PlayerStat stat; // 추가된 부분

    private int currentExperience;
    private int currentLevel;
    private int currentGold;
    private int experienceToNextLevel;

    private PlayerStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        Animator = GetComponentInChildren<Animator>();
        Character = GetComponent<Character>();
        Input = GetComponent<PlayerController>();
        AnimationData.Initialize();

        // 초기화
        currentLevel = stat.level;
        currentExperience = 0;
        currentGold = stat.gold;
        experienceToNextLevel = stat.level * 1000;

        // 상태 머신 초기화 및 첫 상태 설정
        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    public void TakeDamage(float damage)
    { 
        Character.CurrentHealth -= damage;
    }
    public PlayerStateMachine GetStateMachine()
    {
        return stateMachine;
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        while (currentExperience >= experienceToNextLevel)
        {
            currentExperience -= experienceToNextLevel;
            LevelUp();
        }
    }

    public void Heal()
    {
        if(currentGold >= 1000)
        {
            Character.CurrentHealth = Character.MaxHealth;
            currentGold -= 1000;
        }
        else
        {
            ErrorMessageText.Instance.ShowErrorMessage("골드가 부족합니다.");
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        experienceToNextLevel = currentLevel * 100;
        Character.MaxHealth += currentLevel * 10; // 레벨업마다 최대 체력 증가
        Character.AttackPower += currentLevel * 2; // 레벨업마다 공격력 증가
        Character.CurrentHealth = Character.MaxHealth; // 레벨업 시 체력 회복
    }

    public int GetCurrentExperience()
    {
        return currentExperience;
    }

    public int GetExperienceToNextLevel()
    {
        return experienceToNextLevel;
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public int GetGold()
    {
        return currentGold;
    }
}
