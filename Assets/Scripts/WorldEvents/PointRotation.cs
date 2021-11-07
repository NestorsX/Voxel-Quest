using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotation : MonoBehaviour
{
    public float yAngle;

    void Update()
    {
        gameObject.transform.Rotate(0f, yAngle, 0f, Space.Self);
    }
}
