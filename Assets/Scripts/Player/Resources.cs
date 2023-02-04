using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    bool isControlling = false;
    public int health = 3;
    public float waterMax = 100;
    public float water;
    [Tooltip ("How much water is lost per timer tick")]
    public float waterLoss = 1;
    [Tooltip ("How often water is lost in seconds")]
    public float waterLossTime = 0.2f;
    public float parchedPercent = 0;
    public float stunDuration  = 2;

    // Start is called before the first frame update
    void Start()
    {
        water = waterMax;   
        
    }

    private void FixedUpdate()
    {
        if (isControlling) {

            //Drink water
            if (!IsInvoking(nameof(MakeThirsty))) {
                Invoke(nameof(MakeThirsty), waterLossTime);
            }

            if (water <= water / 2)
            {
                parchedPercent = Mathf.Clamp(1 - (water/(waterMax/2)), 0, 1);
                Debug.Log("Parched: " + parchedPercent);
            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeThirsty(float loss) {
        water -= loss;
        Debug.Log("Water: " + water);
        if(water <= 0) {
            OnDeath();
        }
    }

    void ApplyDamage(int damage) {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }
    void OnDeath()
    {
        //TODO: do death things
    }
}
