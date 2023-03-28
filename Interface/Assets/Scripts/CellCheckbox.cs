using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCheckbox : MonoBehaviour
{
    public static void SwitchLocalCheckbox(int index)
    {
        InterfaceManager manager = GameObject.FindObjectOfType<InterfaceManager>();
        manager.AllObjectsInInterface[index].IsChange = true;
        manager.AllObjectsInInterface[index].IsCheckBoxChange = true;
    }

    public static void SwitchGlobalCheckbox()
    {
        InterfaceManager manager = GameObject.FindObjectOfType<InterfaceManager>();
        foreach (var item in manager.AllObjectsInInterface)
        {
            item.IsChange = true;
            item.IsCheckBoxChange = true;
        }
    }
}
