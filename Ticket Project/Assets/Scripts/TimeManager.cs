using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonMonoBehaviour<TimeManager> {

    public static float DeltaTime { get { return Time.deltaTime* timePer; } }
    public static float timePer = 1;
    public static bool IsTimeStop { get { return timePer <= 0; } }

    protected override void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (timePer == 0) { TimeStart(); }
            else { TimeStop(); }
        }
    }

    public static void SetTime(float value) {
        timePer = value;
    }

    public static void TimeStop() {
        timePer = 0;
    }

    public static void TimeStart() {
        if (StageController.instance && StageController.instance.isGameEnd) { return; }

        timePer = 1;
    }
}
