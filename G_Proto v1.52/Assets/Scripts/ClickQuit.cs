using UnityEngine;
using System.Collections;

public class ClickQuit : MonoBehaviour
{
    // for thefucks
    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
    }

    public void ClickClose()
    {
        Application.Quit();
    }
}

