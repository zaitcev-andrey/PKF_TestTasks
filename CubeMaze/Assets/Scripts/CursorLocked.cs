using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLocked : MonoBehaviour
{
    void Start()
    {
        // для фиксации курсора в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
    }
}
