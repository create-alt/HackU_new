using UnityEngine;

public class target_moving : MonoBehaviour
{
    private Vector3 start, end;

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
        float maxDistanceDelta = 5.0f * Time.deltaTime;  //1フレームあたりの移動速度
        transform.position = Vector3.MoveTowards(current, end, maxDistanceDelta);

        if(transform.position == end){
            //目標場所にたどり着いたら自分自身を削除する
            Destroy(this.gameObject);
        }
    }

    // デフォルトマテリアルかどうかを判断するヘルパーメソッド
    bool IsDefaultMaterial(Material material)
    {
        // マテリアルのシェーダーが "Standard" か、名前が "Default-Material" であればデフォルトとみなす
        return material.shader.name == "Standard" || material.name == "Default-Material";
    }
}
