using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public AudioClip ouchieClip;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Root") {
            AudioSource.PlayClipAtPoint(ouchieClip, Camera.main.transform.position);
        } else if (other.tag == "Border") {
            Destroy(this.gameObject);
        }
    }
}
