using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundery : MonoBehaviour
{
    public static float leftSide =3.1f;
    public static float rightSide =6.2f;
    public float internalLeft;
    public float internalRight;

    void Update()
    {
        internalLeft = leftSide;
        internalRight = rightSide;

        
    }
}
