
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//存档
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; set; }
    //前面放存档数据**************************************************
    //
    public int passedLevel;
    public bool isGetRose;
    public bool isGetRob;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();//加载时自动读档
        } else {
            Destroy(gameObject);
        }
    }
    //存档相关**************************************************
    public void SaveGame()
    {
        //创建存档文件夹
        if (!Directory.Exists(Application.dataPath + "/Saves")) {
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        }

        BinaryFormatter fomatter = new BinaryFormatter();//将数据加密为二进制
        FileStream file;
        var json = JsonUtility.ToJson(Instance);
        file = File.Create(Application.dataPath + "/Saves/PlayerData.txt");
        fomatter.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter fomatter = new BinaryFormatter();
        FileStream file;
        if (File.Exists(Application.dataPath + "/Saves/PlayerData.txt")) {
            file = File.Open(Application.dataPath + "/Saves/PlayerData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)fomatter.Deserialize(file), Instance);
            file.Close();
        }
    }
}