using UnityEngine;
using TMPro;

public class Show_Score : MonoBehaviour
{
    public static int score = 0;
    [SerializeField] private TextMeshProUGUI score_object;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // テキストの表示を入れ替える
        score_object.text = "score : " + score.ToString();
    }
}