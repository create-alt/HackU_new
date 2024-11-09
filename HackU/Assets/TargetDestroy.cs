using UnityEngine;

public class TargetDestroy : MonoBehaviour
{
    public Score_Manager score_manager;
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
        score_manager = GameObject.Find("Score_Manager").GetComponent<Score_Manager>();

        //衝突したオブジェクトが「Target」タグを持っている場合
        if (other.CompareTag("Target"))
        {
            // 的を消す
            Destroy(other.gameObject);

            Score_Manager.score++;
            Show_Score.score++;
        }
    }
}
