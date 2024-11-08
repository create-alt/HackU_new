using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StratButton : MonoBehaviour
{
    public bool reset_micon = true;

    private SerialPort serialPort;
    private Thread serialThread;
    private bool keepReading;

    private string portName = "COM4"; // M5StickC Plus2では実際のポートを利用
    private int baudRate = 115200; // ボーレート

    private string receivedData = ""; // 受信データを格納
    private object dataLock = new object(); // データロックオブジェクト

    
    BluetoothReceiver receiver = GameObject.Find("Reciever").GetComponent<BluetoothReceiver>();

    private void Start()
    {
        if (reset_micon)
        {
            // シリアルポートを開く
            serialPort = new SerialPort(portName, baudRate);

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
        }
        
    }

    void Update()
    {
        if (reset_micon)
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
                    Debug.Log("cannot received");
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
                Debug.Log("Cannot get data");
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Return))
            {
                Cursor target_script = GameObject.Find("Cursor").GetComponent<Cursor>();
                target_script.X = 0;
                target_script.Y = 0;

                target_script.transform.position = new Vector3(0, 0, 5f);
            }
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
        int is_reset = int.Parse(data);

        if (is_reset == 0)
        {
            Cursor target_script = GameObject.Find("Cursor").GetComponent<Cursor>();
            target_script.X = 0;
            target_script.Y = 0;

            target_script.transform.position = new Vector3(0, 0, 5f);
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
