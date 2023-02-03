using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Root") {
            //Get hit ouchie
        } else if (other.tag == "Border") {
            Destroy(this.gameObject);
        }
    }
}
