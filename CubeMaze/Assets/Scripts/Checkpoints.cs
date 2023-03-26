using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoints : MonoBehaviour
{
    public Transform PlayerTransform;

    public Transform CheckPoint1Transform;
    public Transform CheckPoint2Transform;
    public Transform FinishTransform;

    public Text Message;
    public GameObject Timer;
    public Text TimerFinish;

    private float finishTime;
    private string message = "Пройдите 3 уровня, чтобы выбраться из лабиринта";
    private bool isIncreaseTimer = true;
    private float counter = 0f;

    void Update()
    {
        float distanceToCheckpoint1 = Vector3.Distance(CheckPoint1Transform.position, PlayerTransform.position);
        float distanceToCheckpoint2 = Vector3.Distance(CheckPoint2Transform.position, PlayerTransform.position);
        float distanceToFinish = Vector3.Distance(FinishTransform.position, PlayerTransform.position);

        if (distanceToCheckpoint1 < 1.5)
        {
            message = "Первый уровень пройден, так держать!";
            isIncreaseTimer = true;
        }

        if (distanceToCheckpoint2 < 1.5)
        {
            message = "Второй уровень пройден, Остался последний!!";
            isIncreaseTimer = true;
        }

        if (distanceToFinish < 1.5)
        {
            message = "Поздравляю тебя, ты прошёл все уровни!!!";
            finishTime = GameObject.FindObjectsOfType<ChangeTimer>()[0].Timer;
            Timer.SetActive(false);
            TimerFinish.text = finishTime.ToString();
            isIncreaseTimer = true;
        }

        if (counter > 5f)
        {
            message = "";
            counter = 0f;
            isIncreaseTimer = false;
        }
        if (isIncreaseTimer)
            counter += Time.deltaTime;
            
        Message.text = message;
    }
}
