using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera MainCamera;
    public Camera AddCamera;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            MainCamera.enabled = !MainCamera.enabled;
            AddCamera.enabled = !AddCamera.enabled;
        }
    }
}
