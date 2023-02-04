using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public static Resources Instance;

    //bool isControlling = true;
    public int health = 3;
    public float waterMax = 100;
    public float waterLow;
    public float water;
    [Tooltip ("How much water is lost per timer tick")]
    public float waterLoss = 1;
    [Tooltip ("How often water is lost in seconds")]
    public float waterLossTime = 0.33f;
    public float parchedPercent = 0;
    public float stunDuration  = 2;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        water = waterMax;
        waterLow = waterMax / 2;
        
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CompareState(GameState.Gameplay))
            {
                //Drink water
                if (!IsInvoking(nameof(MakeThirsty)))
                {
                    Invoke(nameof(MakeThirsty), waterLossTime);
                }

                if (water <= waterMax / 2)
                {
                    parchedPercent = Mathf.Clamp01(1 - (water / (waterMax / 2)));
                    Debug.Log("Parched: " + parchedPercent);
                }

            }

    }
    // Update is called once per frame
    void Update()
    {

    }
    
    public void MakeThirsty()
    {
        Mathf.Clamp(water -= waterLoss,0,waterMax);
        Debug.Log("Water: " + water);
        if(water <= 0) {
            OnDeath();
        }
    }
    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }
    void OnDeath()
    {
        //TODO: do death things
        Debug.Break();
    }
}
