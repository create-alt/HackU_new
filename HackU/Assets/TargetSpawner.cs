using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; // �I�̃v���n�u
    public float spawnAreaWidth = 5.0f; // �o���͈͂̕�
    public float spawnAreaHeight = 5.0f; // �o���͈͂̍���
    public float targetLifetime = 1.5f; // �I�̑��ݎ���
    public float spawnIntervalMin = 0.5f; // �I�̍ŏ��o���Ԋu
    public float spawnIntervalMax = 2.0f; // �I�̍ő�o���Ԋu

    private float gameDuration = 60.0f; // �Q�[���̑�����
    public float elapsedTime = 0.0f; // �o�ߎ���

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
            elapsedTime += spawnInterval; // �o�ߎ��Ԃ�ݐς���
        }
        SceneManager.LoadScene("end");
    }

    void SpawnTarget()
    {
        // �o���ʒu�������_���Ɍ���
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
            Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2),
            0f
        );

        // �I�𐶐�
        GameObject target = Instantiate(targetPrefab, randomPosition, Quaternion.identity);

        // �I����莞�Ԍ�ɏ���
        Destroy(target, targetLifetime);
    }
    
}