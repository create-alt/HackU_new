using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    private bool isShooting = false; // コルーチンが実行中かどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
    }

    private IEnumerator Hoge(){
        
        isShooting = true;

        while(Input.GetMouseButton(0)){    
            //balletプレハブをGameObject型で取得
            GameObject obj = (GameObject)Resources.Load("ballet");

            //balletプレハブをもとにインスタンスを生成
            //元のオブジェクトと被らないように座標を調整
            Instantiate(obj, new Vector3(0.0f, 0.0f, 2.5f), Quaternion.identity);

            yield return new WaitForSeconds(0.3f);
        }

        isShooting = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && !(isShooting)){
            //コルーチンの実行
            StartCoroutine(Hoge());
        }
    }
}
