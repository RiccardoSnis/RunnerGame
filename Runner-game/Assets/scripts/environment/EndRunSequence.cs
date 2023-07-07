using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class EndRunSequence : MonoBehaviour
{
    public GameObject liveCoins;
    public GameObject liveDist;
    public GameObject endScreen;
    public GameObject fadeOut;
    public PlayfabManager playFab;
    public GameObject levelControl;


    void Start(){
        //playFab = levelControl.GetComponent<PlayfabManager>();
        StartCoroutine(EndSequence());   
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(2);
        playFab.SendLeaderboard(LevelDistance.disRun);
        playFab.SendLeaderboard2(CollectableControl.coinCount);
        liveCoins.SetActive(false);
        liveDist.SetActive(false);
        endScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        CollectableControl.coinCount = 0;
        SceneManager.LoadScene(0);
        
    }
}
