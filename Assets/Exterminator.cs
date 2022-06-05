using System.Collections;
using UnityEngine;

public class Exterminator : MonoBehaviour
{
    public void Exterminate()
    {
        var plants = FindObjectsOfType<Plant>();

        StartCoroutine(Exterminate(plants));
    }

    private IEnumerator Exterminate(Plant[] plants)
    {
        foreach (var plant in plants)
        {
            plant.Die();
            yield return null;
        }   
    }
}
