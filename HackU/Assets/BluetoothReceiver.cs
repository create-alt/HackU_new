using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class BluetoothReceiver : MonoBehaviour
{
    private SerialPort serialPort;
    private Thread serialThread;
    private bool keepReading;
    
    // シリアル通信の設定
    public string portName = "COM4"; // ポート番号
    public int baudRate = 115200;    // ボーレート

    private string receivedData = ""; // 受信データを格納
    private object dataLock = new object(); // データロックオブジェクト

    //gyro_xはYに対応、gyro_zはXに対応
    public float X=0f, Y=0f;

    void Start()
    {
        // シリアルポートを開く
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();

        // スレッドを開始
        keepReading = true;
        serialThread = new Thread(ReadSerial);
        serialThread.Start();
    }

    void Update()
    {
        // メインスレッドで画面表示やゲームオブジェクトの更新を行う
        string dataToProcess = null;
        
        lock (dataLock)
        {
            if (!string.IsNullOrEmpty(receivedData))
            {
                dataToProcess = receivedData;
                receivedData = ""; // 受信データをクリア
            }
        }

        if (dataToProcess != null)
        {
            // 受け取ったデータの処理
            Debug.Log("gyro_x, gyro_z: " + dataToProcess);
            ProcessData(dataToProcess);
        }
    }

    // シリアルデータを受信するスレッドの処理
    void ReadSerial()
    {
        while (keepReading && serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                
                lock (dataLock)
                {
                    receivedData = data; // 受信したデータを格納
                }
            }
            catch (TimeoutException)
            {
                Debug.Log("timeout");
                // タイムアウト処理（必要に応じて）
            }
        }
    }

    private void ProcessData(string data)
    {
        // データの処理
        string[] values = data.Split("sep");

        //cursorに渡すデータとして成形する
        X = float.Parse(values[0]) - 13;
        Y = float.Parse(values[1]) - 4;

        if(X < 5.0f && X > -5.0f) X = 0;
        if(Y < 5.0f && Y > -5.0f) Y = 0;

        Debug.Log($"Accelerometer data: x={X}, y={Y}");
        
    }

    void OnApplicationQuit()
    {
        // スレッドを停止し、シリアルポートを閉じる
        keepReading = false;

        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join(); // スレッドが終了するまで待機
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
