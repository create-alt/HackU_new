using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.IO.Ports;


public class Cursor : MonoBehaviour
{

    [SerializeField] GameObject goal_serial;
    Vector3 target_serial;

    BluetoothReceiver target_script;
    private double target_x, target_y;
    private double X=0, Y=0;

    // Start is called before the first frame update
    void Start()
    {
        goal_serial = GameObject.Find("Receiver");

        // スクリーン座標をワールド座標に変換する
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 30f)); // マウス座標は2次元なので3次元目を明示的に指定する
        // ワールド座標をゲームオブジェクトの座標に設定する
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
        try
        {
            target_script = goal_serial.GetComponent<BluetoothReceiver>();
            target_x = target_script.X;
            target_y = target_script.Y;

            X += target_x * Time.deltaTime;
            Y += target_y * Time.deltaTime;
            
            //target_x,yの値を閾値で区切って値の変化を一定にしてみる
            if(target_x )

            // X, Yの範囲を制限
            X = Mathf.Clamp((float)X, -30f, 30f);
            Y = Mathf.Clamp((float)Y, -16f, 18f);

            // 現在のオブジェクトの位置
            Vector3 currentPos = transform.position;

            // スクリーン座標をワールド座標に変換
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3((float)X, (float)Y, 30f));

            // 補間して滑らかに移動
            transform.position = Vector3.Lerp(currentPos, targetPos, 0.1f); // 第三引数は補間の割合
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error reading serial port: " + e.Message);
        }

    }
}
