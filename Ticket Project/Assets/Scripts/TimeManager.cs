using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonMonoBehaviour<TimeManager> {

    public static float DeltaTime { get { return Time.deltaTime* timePer; } }
    public static float timePer = 1;

    public static void SetTime(float value) {
        timePer = value;
    }

    public static void TimeStop() {
        timePer = 0;
    }

    public static void TimeStart() {
        timePer = 1;
    }
}
