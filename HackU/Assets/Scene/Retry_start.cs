using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry_start : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("onClick");
        Score_Manager.score = 0;
        Show_Score.score = 0;
        Gotitle();
    }
    void Gotitle()
    {
        SceneManager.LoadScene("start");
    }
}
