using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelDefinition : ScriptableObject
{
    public Color backgroundColor;
    public PlantDefinition[] plants;
    public GameObject[] enemies;
}
