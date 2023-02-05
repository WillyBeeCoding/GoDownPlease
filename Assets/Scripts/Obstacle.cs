using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 1;
    public AudioClip ouchieClip;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            AudioSource.PlayClipAtPoint(ouchieClip, Camera.main.transform.position);
            Resources.Instance.ApplyDamage(damage);
            GetComponent<PolygonCollider2D>().enabled = false;

        } else if (other.CompareTag("Border")) {
            Destroy(this.gameObject);
        }
    }
}
