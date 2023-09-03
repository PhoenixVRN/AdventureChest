using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBattle", menuName = "DataBattle")]
public class DataBattle : ScriptableObject
{
  public List<ScriptableObject> alllist;
}
