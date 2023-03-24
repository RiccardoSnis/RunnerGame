using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3;
    public float leftRightSpeed = 7;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            if(this.gameObject.transform.position.x > LevelBoundery.leftSide)
            transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);

        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            if(this.gameObject.transform.position.x < LevelBoundery.rightSide)
            transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
        }

        
    }
}
