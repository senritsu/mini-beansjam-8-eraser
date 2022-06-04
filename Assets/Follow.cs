using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = target.transform.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    }
}
