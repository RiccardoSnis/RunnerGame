using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject charModel;

    private bool isStumbling = false;

    void OnTriggerEnter(Collider other)
    {
        if (!thePlayer.GetComponent<PlayerMotor>().enabled)
        {
            return; // Se il PlayerMotor non è abilitato, significa che il personaggio è già in una fase di collisione
        }

            if (!isStumbling)
            {
                isStumbling = true;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                thePlayer.GetComponent<PlayerMotor>().enabled = false;

                StartCoroutine(PerformStumbleBackwards());
            }
        
    }

   private IEnumerator PerformStumbleBackwards()
{
    Animator animator = charModel.GetComponent<Animator>();
    animator.Play("Stumble Backwards");

    float cutoffTime = 0.18f;

    float remainingTime = animator.GetCurrentAnimatorStateInfo(0).length - cutoffTime;
    float normalizedTime = cutoffTime / animator.GetCurrentAnimatorStateInfo(0).length;
    animator.Play("Stumble Backwards", 0, normalizedTime);

    yield return new WaitForSeconds(cutoffTime);

    animator.SetFloat("StumbleBackwardsSpeed", animator.GetCurrentAnimatorStateInfo(0).length / remainingTime);

    float groundHeight = FindGroundHeight();
    StartCoroutine(SmoothMoveToGround(groundHeight));

   
    isStumbling = false;
}





    private IEnumerator SmoothMoveToGround(float targetHeight)
    {
        Vector3 startPos = thePlayer.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, targetHeight, startPos.z);
        float duration = 0.4f; // Durata dell'interpolazione
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            thePlayer.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
    }

    private float FindGroundHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(thePlayer.transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            return hit.point.y;
        }

        return thePlayer.transform.position.y;
    }
}
