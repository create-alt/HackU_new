using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioSource hitSound;



    private bool isShooting = false; // コルーチンが実行中かどうかのフラグ
    private bool checker = false;


    BluetoothReceiver target_script;
    private bool use_micon;

    // Start is called before the first frame update
    void Start()
    {
    }

    private IEnumerator Hoge()
    {

        isShooting = true;


        while (isShooting)
        {
            //while (Input.GetMouseButton(0)){    
            //balletプレハブをGameObject型で取得
            GameObject obj = (GameObject)Resources.Load("ballet");

            //balletプレハブをもとにインスタンスを生成
            //元のオブジェクトと被らないように座標を調整
            Instantiate(obj, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0f);

            isShooting = false;
        }

        checker = false;

    }

    // Update is called once per frame
    void Update()
    {
        target_script = GameObject.Find("Reciever").GetComponent<BluetoothReceiver>();
        use_micon = GameObject.Find("Cursor").GetComponent<Cursor>().use_micon;
        if (use_micon)
        {
            if (target_script.is_fire == 1)
            {
                checker = true;
                Debug.Log("checker is true");
                hitSound.Play();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                checker = true;
                Debug.Log("checker is true");
                hitSound.Play();
            }
        }



        if (checker && !(isShooting))
        {
            //コルーチンの実行
            StartCoroutine(Hoge());
        }
    }
}
