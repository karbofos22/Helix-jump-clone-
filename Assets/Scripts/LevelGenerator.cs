using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject rod;
    public GameObject startingPad;
    public GameObject finishPad;
    public GameObject[] rodParts;

    private Vector3 rodBuilderStartingPoint = new(0, 0, 0);
    private Vector3 nextRodBuilderPoint = new(0, 0, 0);
    readonly float offset = 1.5f;
    public bool isLevelGenerated;

    public void RodBuilder()
    {
        rod.SetActive(true);

        var startingRodPart = Instantiate(startingPad, rodBuilderStartingPoint, startingPad.transform.rotation);
        nextRodBuilderPoint.y -= offset;
        startingRodPart.transform.parent = gameObject.transform;

        for (int i = 0; i < 12; i++)
        {
            if (i == 11)
            {
                var lastPad = Instantiate(finishPad, nextRodBuilderPoint, Quaternion.identity);
                lastPad.transform.parent = gameObject.transform;
            }
            else
            {
                var rodPart = Instantiate(rodParts[Random.Range(0, 5)], nextRodBuilderPoint, RandomRotationGenerator());
                nextRodBuilderPoint.y -= offset;
                rodPart.transform.parent = gameObject.transform;
            }
        }
        isLevelGenerated = true;
    }
    Quaternion RandomRotationGenerator()
    {
        return Quaternion.Euler(new Vector3(0, Random.Range(0, 370), 0));
    }
    public void DestroyPads()
    {
        rodBuilderStartingPoint = new(0, 0, 0);
        nextRodBuilderPoint = new(0, 0, 0);

        GameObject.Destroy(GameObject.Find("Starting pad(Clone)"));
        GameObject.Destroy(GameObject.Find("Finish pad(Clone)"));
        var items = GameObject.FindGameObjectsWithTag("pad");
        foreach (var item in items)
        {
            Destroy(item);
        }
    }
}
