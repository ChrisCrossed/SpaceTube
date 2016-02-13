using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ChangeStartingButton : MonoBehaviour
{
    public EventSystem eEventSystem;

    public void ChangeButton(GameObject gTarget_)
    {
        eEventSystem.SetSelectedGameObject(gTarget_);
    }
}
