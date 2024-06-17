using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoadManager : SingletonMonoBehaviour<SaveAndLoadManager>
{
    string savePath;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        savePath = Application.persistentDataPath;
    }

    public void SavePlayerData(string json)
    {
        File.WriteAllText(savePath + "/PlayerData.txt", json);
        Debug.Log("저장완료 " + savePath);
    }

    public PlayerData LoadPlayerData()
    {
        if (!File.Exists(savePath + "/PlayerData.txt")) 
            return null;

        string saveData = File.ReadAllText(savePath + "/PlayerData.txt");
        return JsonUtility.FromJson<PlayerData>(saveData);
    }
}
