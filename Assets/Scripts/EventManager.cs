using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent<bool> TimerIsDone = new UnityEvent<bool>();
    public static UnityEvent<string, float> OnTap = new UnityEvent<string, float>();

    public static void CallTimerIsDone(bool timerisDone)
    {
        TimerIsDone.Invoke(timerisDone);
    }

    public static void CallOnTap(string piece, float angle)
    {
        OnTap.Invoke(piece, angle);
    }
}
