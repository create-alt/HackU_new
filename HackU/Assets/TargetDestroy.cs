using UnityEngine;

public class TargetDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //�Փ˂����I�u�W�F�N�g���uTarget�v�^�O�������Ă���ꍇ
        if (other.CompareTag("Target"))
        {
            // �I������
            Destroy(other.gameObject);

            
        }
    }
}
