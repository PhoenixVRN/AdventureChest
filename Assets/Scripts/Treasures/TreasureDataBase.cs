using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreasuteDataBase", menuName = "ScriptableObjects/TreasuteDataBase")]
public class TreasuteDataBase : ScriptableObject
{
    public TreasureBase[] TreasureBases;
}

public enum TreasureType
{
    SlashingSword,
    Mascot,
    RodOfPower,
    ThievesTools,
    Scroll,
    RingOfInvisibility,
    DragonScales,
    Elixir,
    DragonBait,
    CityPortal
}
 