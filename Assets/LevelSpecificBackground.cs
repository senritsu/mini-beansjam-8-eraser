using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpecificBackground : MonoBehaviour
{
    public LevelDefinition levelDefinition;
    private void Awake()
    {
        var camera = GetComponent<Camera>();
        camera.backgroundColor = levelDefinition.backgroundColor;
    }
}
