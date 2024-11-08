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
        SceneManager.LoadScene("start");
    }
}
