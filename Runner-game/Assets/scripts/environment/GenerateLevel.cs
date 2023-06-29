using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public string startSectionTag = "StartSection";
    public int zPos = 50;
    public bool startSectionGenerated = false;

    void Start()
    {
        GenerateStartSection();
        StartCoroutine(GenerateRandomSections());
    }

    void GenerateStartSection()
    {
        GameObject[] startSections = GameObject.FindGameObjectsWithTag(startSectionTag);

        if (startSections.Length > 0)
        {
            int randomIndex = Random.Range(0, startSections.Length);
            Instantiate(startSections[randomIndex], new Vector3(-2.87f, 0, zPos), Quaternion.identity);
            zPos += 93;
            startSectionGenerated = true;
        }
        else
        {
            Debug.LogError("No start sections found with tag: " + startSectionTag);
        }
    }

    IEnumerator GenerateRandomSections()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            if (startSectionGenerated)
            {
                int randomSectionIndex = Random.Range(0, section.Length);
                Instantiate(section[randomSectionIndex], new Vector3(-2.87f, 0, zPos), Quaternion.identity);
                zPos += 93;
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
