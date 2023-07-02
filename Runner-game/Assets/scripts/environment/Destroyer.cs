using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private GameObject player;
    private string parentName;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        parentName = transform.name;
    }

    private void Update()
    {
        StartCoroutine(DestroySection());
    }

    IEnumerator DestroySection()
    {
        yield return new WaitForSeconds(0.001f);

        if ( parentName == "Section(Clone)" && transform.position.z + 94 < player.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
