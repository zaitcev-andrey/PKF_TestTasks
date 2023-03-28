using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityManager : MonoBehaviour
{
    // � ���� ������ ����� ����������� ��������� ������������ ��� 
    // ���������� � �������� �������� �� �����
    private InterfaceManager manager;

    private void Start()
    {
        manager = GameObject.FindObjectOfType<InterfaceManager>();
    }

    public void ChangeOpacityButton(int opacity)
    {
        foreach (var item in manager.AllObjectsInInterface)
        {
            if(item.IsCheckBoxTurnOn)
            {
                item.NewOpacity = opacity;
            }
        }
    }
}
