using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public GameObject distDisplay;
    public int disRun;
    public bool addingDis = false;
    public bool startCount = false;
    public float disDelay = 0.4f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || (MobileInput.Instance.SwipeUp))
        {
            startCount = true;
        }

        if (addingDis == false  && startCount == true)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }
    }

    IEnumerator AddingDis()
    {
        disRun += 1;
        distDisplay.GetComponent<Text>().text = "" + disRun;
        yield return new WaitForSeconds(disDelay);
        addingDis = false;
    }
}
