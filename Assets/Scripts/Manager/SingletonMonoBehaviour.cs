using UnityEngine;
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static readonly object padlock = new object();
    private static bool isApplicationQuitting = false;
    public static T Instance
    {
        get
        {
            if (isApplicationQuitting)
            {
                return null;
            }
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return instance;
            }
        }
    }

    // 일반적인 경우
    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
        }
    }

    protected virtual void OnDestroy()
    {
        isApplicationQuitting = true;
    }
}