using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterContainer : MonoBehaviour
{
    int waterAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //AudioSource.PlayClipAtPoint(ouchieClip, Camera.main.transform.position);
            Resources.Instance.ApplyDamage(waterAmount);
            GetComponent<PolygonCollider2D>().enabled = false;
            Destroy(this.gameObject);

        }
        else if (other.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
