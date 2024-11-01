using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
// using System.IO.Ports;


public class Cursor : MonoBehaviour
{

    [SerializeField] GameObject goal_serial;
    Vector3 target_serial;

    BluetoothReceiver target_script;
    private float X=0f, Y=0f, Z=0f, gyro=0f;
    private int is_fire=0;

    // Start is called before the first frame update
    void Start()
    {
        goal_serial = GameObject.Find("Receiver");

        // スクリーン座標をワールド座標に変換する
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 5f)); // マウス座標は2次元なので3次元目を明示的に指定する
        // ワールド座標をゲームオブジェクトの座標に設定する
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
        try
        {
            target_script = GameObject.Find("Reciever").GetComponent<BluetoothReceiver>();
            X -= target_script.gyroZ;
            Y -= target_script.gyroX;
            Z = target_script.yaw;

            //Debug.Log(target_script.yaw);

            is_fire = target_script.is_fire;

            // X, Yの範囲を制限
            // X = Mathf.Clamp((float)X, -30f, 30f);
            X = Mathf.Clamp((float)X, -40f, 40f);

            //Y = (float)Math.Round(Y, 2, MidpointRounding.AwayFromZero);
            Y = Mathf.Clamp((float)Y, -23f, 23f);

            // 現在のオブジェクトの位置
            // Vector3 currentPos = transform.position;

            // スクリーン座標をワールド座標に変換
            //Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, (float)Z, 30f));

            transform.position = new Vector3(X,(float)Y, 5f);

            // 補間して滑らかに移動
            // transform.position = Vector3.Lerp(currentPos, targetPos, 0.1f); // 第三引数は補間の割合
        }
        catch (System.Exception e)
        {
            Debug.Log("Error reading serial port");
        }

    }
}
