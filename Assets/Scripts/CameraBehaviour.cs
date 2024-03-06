
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x + 1, target.position.y + 0.5f, transform.position.z);
    }
}
