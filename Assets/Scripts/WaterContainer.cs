using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterContainer : MonoBehaviour
{
    int waterAmount = 45;
    public AudioClip soundClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            AudioSource.PlayClipAtPoint(soundClip, Camera.main.transform.position, 0.2f);
            Resources.Instance.DrinkWater(waterAmount);
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
