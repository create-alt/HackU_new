using UnityEngine;
using UnityEngine.SceneManagement;
public class StratButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("save");
    }
}
