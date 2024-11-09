using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // 60秒待つための変数
    private float timeToWait = 60f;
    private float elapsedTime = 0f;

    void Update()
    {
        // 経過時間を計測
        elapsedTime += Time.deltaTime;

        // 60秒経過したかチェック
        if (elapsedTime >= timeToWait)
        {
            // 別のシーンに移動
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // 次のシーンの名前を指定します (例えば "NextScene"という名前のシーン)
        SceneManager.LoadScene("end");
    }
}
