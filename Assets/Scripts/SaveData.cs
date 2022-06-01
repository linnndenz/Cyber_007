
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�浵
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; set; }
    //ǰ��Ŵ浵����**************************************************
    //
    public int passedLevel;
    public bool isGetRose;
    public bool isGetRob;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();//����ʱ�Զ�����
        } else {
            Destroy(gameObject);
        }
    }
    //�浵���**************************************************
    public void SaveGame()
    {
        //�����浵�ļ���
        if (!Directory.Exists(Application.dataPath + "/Saves")) {
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        }

        BinaryFormatter fomatter = new BinaryFormatter();//�����ݼ���Ϊ������
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