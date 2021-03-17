using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//By Andrew
//Using Reference https://www.youtube.com/watch?v=BPu3oXma97Y
//Note that every time a player changes scenes,
//you will need to update SaveData.instance.data.level

public class SaveData : MonoBehaviour
{
    public static SaveData instance;
    public Data data;

    string dataFile = "data.dat";

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if(instance !=this) {
            Destroy(this.gameObject);
        }
    }

    public void Save()
    {
        string filePath = Application.persistentDataPath + "/" + dataFile;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved Data");
    }

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        //these keys save and load the scene number rn
        //but that can be removed once the code itself does
        //Save() should be called whenever you update level
        //so that sceneChangerWorks
        if (Input.GetKeyDown(KeyCode.S)) Save();
        if (Input.GetKeyDown(KeyCode.L)) Load();
    }

    public void Load()
    {
        string filePath = Application.persistentDataPath + "/" + dataFile;
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(filePath))
        {
            FileStream file = File.Open(filePath, FileMode.Open);
            Data loaded = (Data)bf.Deserialize(file);
            data = loaded;
            file.Close();
        }
        
        Debug.Log("Loaded Data");
    }
}

[System.Serializable]
public class Data 
{
    public int level = 1;
    //sceneChanger will append this number to the end of
    //the scene it loads, so please consider naming every
    //scene after the level value (ex scene1 for level=1)
    public Data()
    {
        level = 1;
    }
}