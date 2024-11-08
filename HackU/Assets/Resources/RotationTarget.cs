using UnityEngine;

public class RotationTarget : MonoBehaviour
{
    private bool is_rotation = false;
    private float rotationSpeed = 10f;
    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(is_rotation){
            // target.Rotate(target.Rotation + rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            is_rotation = true;
            target = other.transform;
        }
    }
}
