using UnityEngine;
using System.Collections;

public class ClickQuit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Chris Test
        // More Chris Test
        Cursor.visible = true;
    }

    public void ClickClose()
    {
        Application.Quit();
    }
}

