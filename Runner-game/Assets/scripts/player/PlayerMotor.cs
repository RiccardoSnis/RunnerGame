using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 2f;
    private const float TURN_SPEED = 0.05f;
    private float jumpForce = 6f;
    private float gravity = 12f;
    private float verticalVelocity;

    private Animator anim;
    private CharacterController controller;
    public GameObject Player;
    private int desideredLane = 1;
    private bool isRunning = false;
    private bool isSliding = false;

    private CameraSwitcher theCameraSwitcher;

    private float originalSpeed = 10f;
    private float speed = 9.0f;

    //audio
    [SerializeField] public AudioSource coinSource = null;
    
    [SerializeField] public AudioSource effectSource = null;

    [SerializeField] public AudioSource runSource = null;
    // Start is called before the first frame update
    void Start()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        float effectValue = PlayerPrefs.GetFloat("EffectValue");
        runSource.volume = volumeValue;
        effectSource.volume = effectValue;
        coinSource.volume = effectValue;
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        theCameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.S) || (MobileInput.Instance.SwipeUp))
        {
            theCameraSwitcher.SwitchPriority();
            StartRunning();
        }

        if (!isRunning)
        {
            return;
        }

        if((MobileInput.Instance.SwipeLeft) || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            MoveLane(false);
        }
        if((MobileInput.Instance.SwipeRight) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            MoveLane(true);
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if(desideredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desideredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }


        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).x * speed;

        if(controller.isGrounded)
        {
            anim.SetBool("Grounded", true);
            verticalVelocity = -0.1f;

            if((MobileInput.Instance.SwipeUp) || (Input.GetKeyDown(KeyCode.UpArrow)))
            {
                if (isSliding)
                {
                    anim.SetBool("Sliding", false);
                    isSliding = false;
                }
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }
            else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartSliding();
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);
            if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
            {
                verticalVelocity = -jumpForce;
            }
        }


        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);

        Vector3 dir = controller.velocity;
        if(dir != Vector3.zero)
        {
            dir.y=0;
            transform.forward = Vector3.Lerp(transform.forward,dir,TURN_SPEED);
        }
    }

    private void MoveLane(bool goingRight)
    {
        desideredLane += (goingRight) ? 1 : -1;
        desideredLane = Mathf.Clamp(desideredLane, 0 ,2);

    }

    public void StartRunning()
    {
        isRunning=true;
        anim.SetTrigger("StartRunning");

    }

    private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        isSliding = true;
        controller.height /=2;
        controller.center = new Vector3(controller.center.x, controller.center.y /2,controller.center.z);
        Invoke("StopSliding", 1.0f);
    }

    private void StopSliding()
    {
        anim.SetBool("Sliding", false);
        isSliding = false;
        controller.height *=2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }
}
