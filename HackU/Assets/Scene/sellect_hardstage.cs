using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sellect_hardstage : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("onClick");
        SceneManager.LoadScene("hard_stage");
    }
}
