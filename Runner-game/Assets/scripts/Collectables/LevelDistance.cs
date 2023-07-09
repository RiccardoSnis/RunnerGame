using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public GameObject distDisplay;
    public GameObject distEndDisplay;
    public static int disRun;
    public bool addingDis = false;
    public bool startCount = false;
    public float disDelay = 0.4f;
    public float elapsedTime = 0f;
    public float interval = 0f;
    
private float initialInterval = 0.5f; // Intervallo di tempo iniziale tra gli incrementi (in secondi)
public float intervalDecreaseRate = 0.1f; // Tasso di riduzione dell'intervallo di tempo
private float minInterval = 0.005f; // Intervallo di tempo minimo tra gli incrementi (in secondi)
public float maxSpeed = 25f; // Velocità massima del giocatore
private float timeToMaxSpeed = 600f; // Tempo per raggiungere la velocità massima (in unità di tempo)


    public GameObject thePlayer;

    private void Start()
    {
        thePlayer = GameObject.Find("Ch32_nonPBR@Breathing Idle");
        disRun = 0;

        if(thePlayer == null)
        {
            Debug.LogError("thePlayer non trovato");
        }

        interval = initialInterval;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || (MobileInput.Instance.SwipeUp))
        {
            startCount = true;
        }

        if (addingDis == false  && startCount == true && 
            thePlayer.GetComponent<PlayerMotor>().enabled != false)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }
    }

IEnumerator AddingDis()
{

       disRun += 1;

        distDisplay.GetComponent<Text>().text = disRun.ToString();
        distEndDisplay.GetComponent<Text>().text = disRun.ToString();

        yield return new WaitForSeconds(interval);

        elapsedTime += interval;

        // Calcola l'incremento di velocità in base al tempo trascorso e al tempo per raggiungere la velocità massima
        float acceleration = maxSpeed / timeToMaxSpeed;
        float speedIncrement = acceleration * elapsedTime * 0.1f;

   
        interval = Mathf.Max(initialInterval - speedIncrement, minInterval);


        addingDis = false;
}

}
