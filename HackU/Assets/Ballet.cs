using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballet : MonoBehaviour
{

    [SerializeField] GameObject goal_base;
    
    Vector3 target_base;
    

    public GameObject BluetoothReceiver;

    // Start is called before the first frame update
    void Start()
    {
        goal_base = GameObject.Find("Cursor");
        target_base = goal_base.transform.position;  //目標位置(カーソルを出現させ、カーソルの座標を目標位置とする。)
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 current = transform.position;            //現在の玉の位置（玉の出現と発射は原点から行う）
        float maxDistanceDelta = 300.0f * Time.deltaTime;  //1フレームあたりの移動速度
        transform.position = Vector3.MoveTowards(current, target_base, maxDistanceDelta);
        
        if(transform.position == target_base){
            //目標場所にたどり着いたら自分自身を削除する
            Destroy(this.gameObject);
        }
        
    }
}