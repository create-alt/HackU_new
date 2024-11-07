using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sellect_easystage : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("onClick");
        SceneManager.LoadScene("easy_stage");
    }
}