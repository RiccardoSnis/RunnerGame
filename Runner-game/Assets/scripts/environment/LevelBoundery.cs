using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundery : MonoBehaviour
{
    public static float leftSide =1.9f;
    public static float rightSide =5.0f;
    public float internalLeft;
    public float internalRight;

    void Update()
    {
        internalLeft = leftSide;
        internalRight = rightSide;

    }
}
