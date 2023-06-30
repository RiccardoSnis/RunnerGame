using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunSequence : MonoBehaviour
{
    public GameObject liveCoins;
    public GameObject liveDist;
    public GameObject endScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndSequence());   
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(3);
        liveCoins.SetActive(false);
        liveDist.SetActive(false);
        endScreen.SetActive(true);

    }
}
