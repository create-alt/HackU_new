using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("sellect_stage");
    }
}
