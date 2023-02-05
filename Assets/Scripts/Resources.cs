using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public static Resources Instance; //a static
    public AudioClip thing;
    private int _health;// dont use
    private int _water; //dont use

    // Health-related stats
    public int healthMax = 3;
    public float stunDuration  = 2;

    // Water-related stats
    public int waterMax = 100;
    private int waterLow;
    [Tooltip ("How much water is lost per timer tick")]
    public int waterLoss = 1;
    [Tooltip ("How often water is lost in seconds")]
    public float waterLossTime = 0.33f;
    private float parchedPercent = 0f;

    //Update the UI whenever health is set
    public int Health { 
        get { return _health; }
        set {
            _health = Mathf.Clamp(value, 0, 3);
            GameManager.Instance.AdjustHealthUI();
        }
    }

    //Update the UI whenever water is set

    public int Water {
        get { return _water; }
        set {
            _water = Mathf.Clamp(value, 0, waterMax);
            GameManager.Instance.AdjustWaterUI();
        }
    }

    private void Awake() => Instance = this;
    
    void Start() {
        Water = waterMax;
        Health = healthMax;
        waterLow = waterMax / 2;
    }

    private void FixedUpdate() {
        if (GameManager.Instance.CompareState(GameState.Gameplay)) {
            //CONSUME LIQUID
            if (!IsInvoking(nameof(MakeThirsty))) {
                Invoke(nameof(MakeThirsty), waterLossTime);
                GetParchedAmount();
            }
        }
    }

    public float GetParchedAmount() {
        parchedPercent = Mathf.Clamp01(1 - ((float)Water / waterLow));
        //Debug.Log("Parched amount: " + parchedPercent*100f + "%");
        return parchedPercent;
    }

    public void DrinkWater(int amount) {
        Water += amount;
    }
    
    public void MakeThirsty() {
        Mathf.Clamp(Water -= waterLoss, 0, waterMax);
        Debug.Log("Water: " + Water);
        if (Water <= 0) { OnDeath(true); }
    }

    public void ApplyDamage(int damage) {
        Mathf.Clamp(Health -= damage, 0, healthMax);
        Debug.Log("Health: " + Health);
        if (Health <= 0) { OnDeath(false); }
    }

    void OnDeath(bool parched)
    {
        GameManager.Instance.UpdateGameState(GameState.GameOver);
        GameManager.Instance.SetScoreValues();
        Time.timeScale = 0.5f;  // :D
        if (thing != null) {
            AudioSource.PlayClipAtPoint(thing, Camera.main.transform.position);
        }
        StartCoroutine(GameManager.Instance.FadeInGameOverAnim(parched));
    }

    void ResetGame() {
        Water = waterMax;
        Health = healthMax;
    }
}
