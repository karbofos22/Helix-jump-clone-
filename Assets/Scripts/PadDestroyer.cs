using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
