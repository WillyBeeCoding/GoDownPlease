using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public static Resources Instance; //a static

    //Update the UI whenever health is set
    private int _health;// dont use
    public int Health { 
        get { return _health; }
        set{
            _health = Mathf.Clamp(value, 0, 3);
            //GameManager.Instance.AdjustHealthUI(); //TODO: unedit
        }

    }

    //Update the UI whenever water is set
    private int _water; //dont use
    public int Water{
        get { return _water; }
        set{
            _water = Mathf.Clamp(value, 0, waterMax);
            //GameManager.Instance.AdjustHealthUI();  //TODO: unedit
        }
    }

    public int waterMax = 100;
    private int waterLow;
    [Tooltip ("How much water is lost per timer tick")]
    public int waterLoss = 1;
    [Tooltip ("How often water is lost in seconds")]
    public float waterLossTime = 0.33f;
    public float parchedPercent = 0f;
    public float stunDuration  = 2;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Water = waterMax;
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
                GetParchedAmount();                            
            }
        }
    }

    public float GetParchedAmount()
    {
        if (Water <= waterMax / 2)
        {
            parchedPercent = Mathf.Clamp01(1 - ((float)Water / (waterMax / 2)));
            Debug.Log("Parched amount: " + parchedPercent);            
        }
        return parchedPercent;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DrinkWater(int amount)
    {
        Water += amount;
        //GameManager.Instance.AdjustWaterUI;
    }
    
    public void MakeThirsty()
    {
        Mathf.Clamp(Water -= waterLoss,0,waterMax);
        Debug.Log("Water: " + Water);
        if(Water <= 0) {
            OnDeath();
        }
    }
    public void ApplyDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
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
