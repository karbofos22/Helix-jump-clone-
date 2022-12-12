using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private GameManager gameManager;

    readonly float camSpeed = 5.8f;

    [HideInInspector]
    public Vector3 startingPoint = new(0, 0.86f, -4.449f);
    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (gameManager.isGameActive)
        {
            CameraMovement();
        }
    }
    void CameraMovement()
    {
        if (playerController.yPosForCam == 0 || gameManager.isGameOver)
        {
            transform.position = startingPoint;
        }
        else
        {
            Vector3 targetPos = new(transform.position.x, playerController.yPosForCam, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, camSpeed * Time.deltaTime);
        }
    }
    void TimeManager()
    {

    }
}
