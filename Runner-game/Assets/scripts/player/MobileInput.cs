using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private const float DEADZONE = 100;   // Define Deadzone area to check if valid swipe

    public static MobileInput Instance { set; get; } // Able to refernce from outside

    // Define public Variables
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown; // bool to determine on/off status
    private Vector2 swipeDelta, startTouch;  // Start touch

    public bool Tap {get {return tap;}}
    public Vector2 SwipeDelta { get { return swipeDelta;}}
    public bool SwipeLeft {get {return swipeLeft;}}
    public bool SwipeRight {get {return swipeRight;}}
    public bool SwipeUp {get {return swipeUp;}}
    public bool SwipeDown {get {return swipeDown;}}

    public void Awake(){
        Instance = this;
    }

    private void Update(){

        //reset all the booleans to be false
        tap=swipeLeft=swipeRight=swipeUp=swipeDown=false;  //Nothing active so set all to false

        //check for the inputs from mouse or mobile
        #region Standalone inputs for mouse input     //Define the beginning of the mouse
        if(Input.GetMouseButtonDown(0)){     //if left mouse button clicked
            tap=true;                        //Set Tap to true 
            startTouch=Input.mousePosition;  //Set Starttouch equals to current mouse position
        }

        else if (Input.GetMouseButtonUp(0)){
            startTouch=swipeDelta=Vector2.zero;
        }
        #endregion

        #region Mobile inputs
        if(Input.touches.Length !=0){
            if(Input.touches[0].phase == TouchPhase.Began){
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled){ // cancel if end touch phase pr its cancelled phase
                startTouch = swipeDelta=Vector2.zero;
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if(startTouch != Vector2.zero){
            if(Input.touches.Length !=0){
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if(Input.GetMouseButton(0)){
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if(swipeDelta.magnitude > DEADZONE){
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x)> Mathf.Abs(y)){

                if(x<0){
                    swipeLeft = true;
                }
                else{
                    swipeRight = true;
                }
            }   
            else{
                if(y<0){
                    swipeDown = true;
                }
                else{
                    swipeUp = true;
                }
            }

            startTouch = swipeDelta = Vector2.zero;
        }

    }
    
}
