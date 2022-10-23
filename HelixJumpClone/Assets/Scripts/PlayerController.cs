using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody playerRb;
    public ParticleSystem deathParticle;
    private AudioSource jumpSound;

    [HideInInspector]
    public float yPosForCam;

    private float speed = 3.1f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        jumpSound = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            playerRb.useGravity = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Killer pad"))
        {
            gameManager.isGameOver = true;
            gameManager.isGameActive = false;
            deathParticle.Play();
        }
        else if(collision.gameObject.CompareTag("Starting pad") && gameManager.isGameActive)
        {
            playerRb.velocity = Vector3.up * speed;
            jumpSound.Play();
            yPosForCam = 0;
        }
        else if (collision.gameObject.CompareTag("pad") && gameManager.isGameActive)
        {
            playerRb.velocity = Vector3.up * speed;
            jumpSound.Play();
        }
        else if (collision.gameObject.CompareTag("Finish pad"))
        {
            gameManager.isGameActive = false;
            gameManager.isNextLevel = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        yPosForCam = other.transform.position.y + 1f;
    }
    private void OnTriggerExit(Collider other)
    {
        if (gameManager.isGameActive)
        {
            gameManager.scores += 100;

            foreach (Transform pad in other.transform)
            {
                pad.GetComponent<Rigidbody>().isKinematic = false;
                pad.GetComponent<Rigidbody>().AddExplosionForce(90, other.transform.position, 5);
                gameManager.musicController.PlayOneShot(gameManager.padDestroySound, 0.3f);
            }
            other.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
