using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
// using System.IO.Ports;


public class Cursor : MonoBehaviour
{

    public bool use_micon = true; //マイコンを使用するならtrue、使用しないならfalse

    [SerializeField] GameObject goal_serial;
    Vector3 target_serial;

    BluetoothReceiver target_script;
    public float X = 0f, Y = 0f, Z = 0f, gyro = 0f;
    public float minus_X = 0, minus_Y = 0;
    private int is_fire = 0;

    // Start is called before the first frame update
    void Start()
    {
        goal_serial = GameObject.Find("Receiver");

        // スクリーン座標をワールド座標に変換する
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, -1.5f)); // マウス座標は2次元なので3次元目を明示的に指定する
        // ワールド座標をゲームオブジェクトの座標に設定する
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (use_micon)
        {
            try
            {
                target_script = GameObject.Find("Reciever").GetComponent<BluetoothReceiver>();

                float delta_X = target_script.gyroZ;
                float delta_Y = target_script.gyroX;

                

                X -= delta_X;
                Y -= delta_Y; 


                is_fire = target_script.is_fire;

                // X, Yの範囲を制限
                X = Mathf.Clamp((float)X, -40f, 40f);

                Y = Mathf.Clamp((float)Y, -23f, 23f);

                // 現在のオブジェクトの位置
                // Vector3 currentPos = transform.position;

                transform.position = new Vector3(X, Y, -1.5f);

            }
            catch (System.Exception e)
            {
                Debug.Log("Error reading serial port");
            }
        }
        else
        {
            // マウスの座標を取得し、それに基づいてオブジェクトの位置を更新する
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 14f; // カメラからの距離を設定
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // オブジェクトの位置をマウスのワールド座標に設定
            transform.position = worldPosition;
        }



    }
}