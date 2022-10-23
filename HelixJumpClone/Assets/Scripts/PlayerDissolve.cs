using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDissolve : MonoBehaviour
{
    private Material material;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameManager.isGameOver)
        {
            material.SetFloat("_SwitcherX", -1);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Killer pad"))
        {
            StartCoroutine(Dissolve());
        }
    }
    IEnumerator Dissolve()
    {
        float dissolveValue = -1f;
        Debug.Log("Dissolve coroutine started");
        while (dissolveValue < 0.7f)
        {
            dissolveValue += Time.deltaTime / 0.02f;
            yield return new WaitForSeconds(0.07f);
            dissolveValue = Mathf.Clamp01(dissolveValue);

            material.SetFloat("_SwitcherX", dissolveValue);

        }
        yield return null;
    }
}
