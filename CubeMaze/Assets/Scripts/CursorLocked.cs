using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLocked : MonoBehaviour
{
    void Start()
    {
        // ��� �������� ������� � ������ ������
        Cursor.lockState = CursorLockMode.Locked;
    }
}
