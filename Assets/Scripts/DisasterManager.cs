using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DisasterManager : MonoBehaviour
{
    private float disasterProgress;
    private int disasterType;
    public float disasterDuration = 120.0f;

    public void Start() {
        /* triggerNewDisaster(); */
    }
    public void Update() {
        disasterProgress += Time.deltaTime;
        if (disasterProgress >= disasterDuration) {
            triggerNewDisaster();
        }
    }

    public void triggerNewDisaster() {
        Debug.Log("dis type="+ disasterType);
        disasterProgress = 0.0f;
        disasterType = ++disasterType % 3;
    }


}
