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
    //private string portName = "/dev/ttyUSB0"; // M5StickC Plus2では実際のポートを利用
    private string portName = "COM3"; // M5StickC Plus2では実際のポートを利用
    private int baudRate = 115200; // ボーレート

    private string receivedData = ""; // 受信データを格納
    private object dataLock = new object(); // データロックオブジェクト

    // gyro_xはYに対応、gyro_zはXに対応
    public float roll = 0f, pitch = 0f, yaw = 0f;
    public float accX = 0f, accY = 0f, accZ=0f;
    public float gyroX=0f, gyroY = 0f, gyroZ=0f;
    public float init_gyroX = 0f, init_gyroY = 0f;
    public int is_fire = 0;
    public bool is_first = true;

    void Start()
    {
        // シリアルポートを開く
        serialPort = new SerialPort(portName, baudRate);
        // シリアルポートが開けるか確認する
        try
        {
            Debug.Log("Serial port opened");
            serialPort.Open();
        }
        catch (Exception ex)
        {
            Debug.LogError($"シリアルポートを開けませんでした: {ex.Message}");
            return;
        }

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
            else
            {
                //Debug.Log("cannot received");
            }
        }

        if (dataToProcess != null)
        {
            // 受け取ったデータの処理
            Debug.Log("data original: " + dataToProcess);
            ProcessData(dataToProcess);
        }
        else
        {
            //Debug.Log("Cannot get data");
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
            catch (Exception ex)
            {
                Debug.Log($"データ読み込みエラー: {ex.Message}");
            }
        }
    }

    private void ProcessData(string data)
    {
        // データの処理
        string[] values = data.Split("sep");

        // cursorに渡すデータとして成形する
        if (values.Length >= 4) // 必要な値の数を確認
        {
            //roll = float.Parse(values[0]) * 3f;
            //pitch = float.Parse(values[1]) * 3f;
            //yaw = float.Parse(values[2]) * 3f;
            is_fire = int.Parse(values[0]);

            //accX = float.Parse(values[4]);
            //accY = float.Parse(values[5]);
            //accZ = float.Parse(values[6]);

            gyroX = float.Parse(values[1]) * Time.deltaTime * 0.6f;
            //gyroY = float.Parse(values[8]);
            gyroZ = float.Parse(values[3]) * Time.deltaTime * 0.6f;

            //pitch = pitch + accX * Time.deltaTime;



            //Debug.Log($"data: {data}");

            if(is_first)
            {
                is_first = false;

                init_gyroX = gyroX;
                init_gyroY = gyroY;
            }
        }
        else
        {
            Debug.Log("不正なデータフォーマット: " + data);
        }
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
            serialPort.Close(); // シリアルポートを閉じる
        }
    }
}