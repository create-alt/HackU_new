using UnityEngine;

public class target_moving : MonoBehaviour
{
    private Vector3 start, end;

    public float rotationSpeed = 100f; // 回転速度を指定

    private bool isRotating = false;

    void Start()
    {
        transform.Rotate(90, 0, 0);
        float y = Random.Range(-5.0f, 5.0f);

        start = new Vector3(-17.0f, y, -10f);
        end   = new Vector3(17.0f, y, -10f);
        transform.position = start;
    }

    void Update(){
        Vector3 current = transform.position;            //現在の玉の位置（玉の出現と発射は原点から行う）
        float maxDistanceDelta = 1.0f * Time.deltaTime;  //1フレームあたりの移動速度
        transform.position = Vector3.MoveTowards(current, end, maxDistanceDelta);

        if(transform.position == end){
            //目標場所にたどり着いたら自分自身を削除する
            Destroy(this.gameObject);
        }

        if (isRotating)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    // 球が円柱に当たったときに回転を開始する
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // 球のタグを"Bullet"に設定して判定
        {
            Debug.Log("attack");
            isRotating = true;
        }
    }

    // 回転を停止する場合の例（オプション）
   
}
