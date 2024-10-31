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
        //衝突したオブジェクトが「Target」タグを持っている場合
        if (other.CompareTag("Target"))
        {
            // 的を消す
            Destroy(other.gameObject);

            
        }
    }
}
