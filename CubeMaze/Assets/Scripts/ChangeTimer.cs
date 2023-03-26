using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTimer : MonoBehaviour
{
    public float Timer = 0f;
    public Text TimerText;

    void Start()
    {
        TimerText.text = Timer.ToString();
    }

    void Update()
    {
        Timer += Time.deltaTime;
        TimerText.text = Mathf.Round(Timer).ToString();
    }
}
