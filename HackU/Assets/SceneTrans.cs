using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // 60�b�҂��߂̕ϐ�
    private float timeToWait = 60f;
    private float elapsedTime = 0f;

    void Update()
    {
        // �o�ߎ��Ԃ��v��
        elapsedTime += Time.deltaTime;

        // 60�b�o�߂������`�F�b�N
        if (elapsedTime >= timeToWait)
        {
            // �ʂ̃V�[���Ɉړ�
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // ���̃V�[���̖��O���w�肵�܂� (�Ⴆ�� "NextScene"�Ƃ������O�̃V�[��)
        SceneManager.LoadScene("end");
    }
}
