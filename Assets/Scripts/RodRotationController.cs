using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodRotationController : MonoBehaviour
{
    private GameManager gameManager;
    readonly float rotateSpeed = 220f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
        }
        
    }
}
