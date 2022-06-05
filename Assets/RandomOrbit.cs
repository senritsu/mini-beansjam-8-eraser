using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomOrbit : MonoBehaviour
{
    public float distance;
    public float interval;
    private Vector3 _center;
    private Vector3 _target;
    
    private void Awake()
    {
        _center = transform.localPosition;
    }

    private void Start()
    {
        StartCoroutine(OrbitLoop());
    }

    private IEnumerator OrbitLoop()
    {
        while (true)
        {
            _target = _center + (Vector3)Random.insideUnitCircle.normalized * distance;
            
            yield return new WaitForSeconds(interval);
        }
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _target, Time.deltaTime);
    }
}
