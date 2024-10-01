using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    // ターゲットとなるオブジェクトを指定するための変数
    public Transform target;

    // 回転のオフセット
    public Vector3 rotationOffset = new Vector3(90, 0, 0); // 90度x軸方向にオフセット

    void Update()
    {
        // ターゲットが設定されていれば、オブジェクトをその方向に向かせる
        if (target != null)
        {
            // ターゲットの方向を計算
            Vector3 targetDirection = target.position - transform.position;

            // ターゲット方向に向かせつつ回転のオフセットを追加
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
        }
    }
}