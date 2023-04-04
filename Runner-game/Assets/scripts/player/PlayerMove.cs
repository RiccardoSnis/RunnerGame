using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7;
    public float leftRightSpeed = 7;
    public bool isJumping = false;
    public bool comingDown = false;
    public bool isSliding = false;
    public bool comingUp = false;
    public GameObject playerObject;
    static public bool canMove = false;
 

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

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) && isSliding==false){
            if (isJumping == false ) {
                isJumping = true;
                playerObject.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence());
            }
        } 
        if(isJumping == true) {
            if(comingDown == false){
                transform.Translate(Vector3.up * Time.deltaTime * 3, Space.World);
            }
            if(comingDown == true){
                transform.Translate(Vector3.up * Time.deltaTime * -3, Space.World);
            }
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && isJumping==false){
            if(isSliding == false){
                isSliding = true;
                playerObject.GetComponent<Animator>().Play("Running Slide");
                StartCoroutine(SlideSequence());
            }
        }
    }


    IEnumerator JumpSequence(){
        yield return new WaitForSeconds(0.45f);
        comingDown = true;
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        comingDown = false;
        playerObject.GetComponent<Animator>().Play("Standard Run");
    }

    IEnumerator SlideSequence(){
        yield return new WaitForSeconds(1.1f);
        isSliding = false;
        playerObject.GetComponent<Animator>().Play("Standard Run");
    }

}
