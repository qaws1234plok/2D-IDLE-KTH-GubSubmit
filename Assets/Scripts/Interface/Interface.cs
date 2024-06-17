public interface IDamageble
{
    public void TakeDamage(int damage);
}

public interface IExperienceHandler
{
    void GainExperience(float amount);
    void LevelUp();
    int GetExperienceForNextLevel(int level);
    int PlayerLevel { get; }
    float CurrentExperience { get; }
    float ExperienceForNextLevel { get; }
}

public interface IState
{
    public void Enter(); // 상태진입
    public void Exit();  // 상태해제
    public void HandleInput();
    public void Update();
}