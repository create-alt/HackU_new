using UnityEngine;
using TMPro;

public class Score_Manager : MonoBehaviour
{

    public int score = 0;
    [SerializeField] private TextMeshProUGUI score_object;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // �e�L�X�g�̕\�������ւ���
        score_object.text = "score : " + score.ToString();
    }
}
