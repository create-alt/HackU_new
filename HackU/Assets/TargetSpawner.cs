using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // 的のプレハブ
    public float spawnAreaWidth = 5.0f; // 出現範囲の幅
    public float spawnAreaHeight = 5.0f; // 出現範囲の高さ
    public float targetLifetime = 1.5f; // 的の存在時間
    public float spawnIntervalMin = 0.5f; // 的の最小出現間隔
    public float spawnIntervalMax = 2.0f; // 的の最大出現間隔

    private float gameDuration = 60.0f; // ゲームの総時間
    public float elapsedTime = 0.0f; // 経過時間

    void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    IEnumerator SpawnTargets()
    {
        while (elapsedTime < gameDuration)
        {
            SpawnTarget();
            float spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval; // 経過時間を累積する
        }
        SceneManager.LoadScene("end");
    }

    void SpawnTarget()
    {
        // 出現位置をランダムに決定
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
            Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2),
            0f
        );

        // 的を生成
        Quaternion rot = Quaternion.Euler(0, 0, 90);
        GameObject target = Instantiate(targetPrefab, randomPosition, rot);

        // 的を一定時間後に消す
        Destroy(target, targetLifetime);
    }
    
}