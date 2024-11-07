using UnityEngine;
using UnityEngine.SceneManagement;
public class StratButton : MonoBehaviour
{

    void Update()
    {
        //エンターキーが入力された場合「true」
        if (Input.GetKey(KeyCode.Return))
        {
            Cursor target_script = GameObject.Find("Cursor").GetComponent<Cursor>();
            BluetoothReceiver receiver = GameObject.Find("Reciever").GetComponent<BluetoothReceiver>();

            target_script.X = 0;
            target_script.Y = 0;

            target_script.minus_X = receiver.gyroX * 0.8f;
            target_script.minus_Y = receiver.gyroZ * 0.8f;

            target_script.transform.position = new Vector3(0, 0, 5f);
        }
    }
}
