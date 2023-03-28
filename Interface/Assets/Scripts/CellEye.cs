using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEye : MonoBehaviour
{
    public static void SwitchLocalEye(int index)
    {
        InterfaceManager manager = GameObject.FindObjectOfType<InterfaceManager>();
        manager.AllObjectsInInterface[index].IsChange = true;
        manager.AllObjectsInInterface[index].IsEyeChange = true;
    }

    public static void SwitchGlobalEye()
    {
        InterfaceManager manager = GameObject.FindObjectOfType<InterfaceManager>();
        foreach (var item in manager.AllObjectsInInterface)
        {
            item.IsChange = true;
            item.IsEyeChange = true;
        }
    }
}
