using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
public class reset : MonoBehaviour
{
    public bool reset_micon = true;
    public int is_reset = 0;

    private SerialPort serialPort;
    private Thread serialThread;
    private bool keepReading;

    private string portName = "COM4"; // M5StickC Plus2�ł͎��ۂ̃|�[�g�𗘗p
    private int baudRate = 115200; // �{�[���[�g

    private string receivedData = ""; // ��M�f�[�^���i�[
    private object dataLock = new object(); // �f�[�^���b�N�I�u�W�F�N�g

    void Start()
    {
        if (reset_micon)
        {
            // �V���A���|�[�g���J��
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 500; // �^�C���A�E�g�ݒ�

            try
            {
                Debug.Log("Serial port opened");
                serialPort.Open();
                keepReading = true;  // �f�[�^�̓ǂݎ����J�n����
                serialThread = new Thread(ReadSerial);  // �V�����X���b�h���쐬
                serialThread.Start();  // �X���b�h���J�n
            }
            catch (Exception ex)
            {
                Debug.LogError($"�V���A���|�[�g���J���܂���ł���: {ex.Message}");
                return;
            }
        }
    }

    void Update()
    {
        if (reset_micon)
        {
            // ���C���X���b�h�ŉ�ʕ\����Q�[���I�u�W�F�N�g�̍X�V���s��
            string dataToProcess = null;

            lock (dataLock)
            {
                if (!string.IsNullOrEmpty(receivedData))
                {
                    dataToProcess = receivedData;
                    receivedData = ""; // ��M�f�[�^���N���A
                }
                else
                {
                    Debug.Log("reset cannot received");
                }
            }

            if (dataToProcess != null)
            {
                // �󂯎�����f�[�^�̏���
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

    // �V���A���f�[�^����M����X���b�h�̏���
    void ReadSerial()
    {
        while (keepReading && serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();

                lock (dataLock)
                {
                    receivedData = data; // ��M�����f�[�^���i�[
                }
            }
            catch (TimeoutException)
            {
                Debug.Log("timeout");
                // �^�C���A�E�g�����i�K�v�ɉ����āj
            }
            catch (Exception ex)
            {
                Debug.Log($"�f�[�^�ǂݍ��݃G���[: {ex.Message}");
            }
        }
    }

    void ProcessData(string data)
    {
        Debug.Log(data);
        is_reset = int.Parse(data);

        if (is_reset == 1)
        {
            Cursor target_script = GameObject.Find("Cursor").GetComponent<Cursor>();
            target_script.X = 0;
            target_script.Y = 0;

            target_script.transform.position = new Vector3(0, 0, 5f);
        }

    }

    void OnApplicationQuit()
    {
        // �X���b�h���~���A�V���A���|�[�g�����
        keepReading = false;

        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join(); // �X���b�h���I������܂őҋ@
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close(); // �V���A���|�[�g�����
        }
    }
}
