using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{

	// Use this for initialization
    public void ClickLoad(string Level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Level);
    }
}
