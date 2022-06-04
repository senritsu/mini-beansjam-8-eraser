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
