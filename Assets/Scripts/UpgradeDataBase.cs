using System;
using UnityEngine;


[Serializable]
public class DatawhoIsBeingBeaten
{
    public TypeСreature whoIsHitting;
    public int quantity;
}

// Атака
[Serializable]
public class Hero
{
    public TypeСreature whoBeats;
    public DatawhoIsBeingBeaten[] whoIsHitting;
}



[CreateAssetMenu(fileName = "UpgradeDataBase", menuName = "ScriptableObjects/UpgradeDataBase")]
public class UpgradeDataBase : ScriptableObject
{
    public Hero[] heros;

    public Hero[] GetUpgrade()
    {
        return heros;
    }
}
