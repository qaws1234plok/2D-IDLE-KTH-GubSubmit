using UnityEngine;
using System.Linq;

public enum TaskState
{
    Inactive,
    Running,
    Complete
}


[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    #region Events
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
    public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuceess);
    #endregion

    [Header("Category")]
    [SerializeField] private Category category;

    [Header("Task")]
    [SerializeField] private string codeName;
    [SerializeField] private string description;

    [Header("Setting")]
    [SerializeField] private int needSuccessToCompleted;
    [SerializeField] InitialSuccessValue initialSuccessValue;
    [SerializeField] private bool canReceiverReportsDuringCompletion; 
    // Task가 완료되어도 계속 성공횟수(카운팅)을 할 것인지를 정하는 옵션, Ex 100개를 모아야 클리어 할 수 있는데, 유저가 50개를 버림
    // 만약 이 옵션이 없다면 100개를 모은 이후에는 카운팅을 멈추기 떄문에 설령 50개를 버렸어도 100개로 판정되어 퀘스트가 클리어 될 수 있음
    // 이를 방지하기 위한 것

    // 한 테스크의 타겟이 여러개일 수 있으므로, 배열로 선언
    [Header("Target")]
    [SerializeField] private TaskTarget[] targets;

    [Header("Action")]
    [SerializeField] private TaskAction action;

    private TaskState state;
    private int currectSuccess;

    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;

    public int CurrentSuccess
    {
        get => currectSuccess;

        set
        {
            int prevSuccess = currectSuccess;
            currectSuccess = Mathf.Clamp(value, 0, needSuccessToCompleted);
            if (currectSuccess != prevSuccess)
            {
                state = currectSuccess == needSuccessToCompleted ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currectSuccess, prevSuccess);
            }
        }
    }

    public Category Category => category;
    public string CodeName => codeName;
    public string Description => description;
    public int NeedSuccessToCompleted => needSuccessToCompleted;

    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState);
        }
    }

    public bool IsComplete => State == TaskState.Complete;

    public Quest Owner { get; private set; }

    public void Setup(Quest owner)
    {
        Owner = owner;
    }

    public void Start()
    {
        State = TaskState.Running;
        if (initialSuccessValue)
        {
            CurrentSuccess = initialSuccessValue.Getvalue(this);
        }
    }

    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }


    public void ReceiveReport(int successCount)
    {
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
    }

    public void Complete()
    {
        CurrentSuccess = needSuccessToCompleted;
    }

    // TaskTarget을 통해 이 Task가 성공 횟수를 보고받을 대상인지 아닌지를 판단
    // Setting해놓은 Target들 중에 있다면 True를 없다면 Fasle를 반환
    public bool IsTarget(string category, object target)
        => Category == category &&
        targets.Any(x => x.IsEqual(target)) &&
        (!IsComplete || (IsComplete && canReceiverReportsDuringCompletion));
}