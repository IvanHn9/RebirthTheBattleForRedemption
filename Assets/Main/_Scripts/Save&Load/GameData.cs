using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int currency;

    public SerializableDictionary<string,bool> skillTree;
    public SerializableDictionary<string, bool> coin;
    public SerializableDictionary<string, bool> chest;
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentId;
    public SerializableDictionary<string, bool> checkpoints;
    public string closestCheckpointId;
    public string sceneName;
    public SerializableDictionary<string, float> volumeSettings;
    public GameData()
    {
        this.currency = 0;
        skillTree = new SerializableDictionary<string,bool>();
        coin = new SerializableDictionary<string,bool>();
        chest = new SerializableDictionary<string,bool>();
        inventory = new SerializableDictionary<string, int>();
        equipmentId = new List<string>();
        closestCheckpointId = string.Empty;
        checkpoints = new SerializableDictionary<string, bool>();
        sceneName = string.Empty;
        volumeSettings = new SerializableDictionary<string, float>();
    }
}
