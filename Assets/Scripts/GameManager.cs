using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Gameplay,
    GameOver,
    Scoreboard,
    Options
}

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;  
    public GameState gameState;

    static public int currentScore;
    static public int highScore;
    public int lives;

    public GameObject startCanvas;
    public GameObject overlayCanvas;

    private GameObject dividingBar;
    private GameObject distanceSign;
    private GameObject healthSign;
    private GameObject healthGauge;
    private GameObject waterSign;
    private GameObject waterGauge;

    public GameObject mainCamera;
    public Resources resources;
    public bool gameStarted;
    public float cameraSpeed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindUIAssets();
        UpdateGameState(GameState.MainMenu);  //calls the function ones
        resources = Resources.Instance;
    }

    //public static event Action<GameState> ChangeGameState;
    // Update is called once per frame
    void Update()
    {
        if (CompareState(GameState.Gameplay))
        {
            transform.position += new Vector3(0, -cameraSpeed * Time.deltaTime, 0);
        }
    }

    // Wrapper for the unity editor to understand lol
    public void UpdateGameState(int state) {
        UpdateGameState((GameState) state);
    }

    public void UpdateGameState(GameState state)
    {
        Instance.gameState = state;
        switch (state)
        {
            case GameState.MainMenu: //set when we are in the main menu
                FadeInMenu();
                break;
            case GameState.Gameplay: // set when we are playing the game actively
                FadeOutMenu();
                break;
            case GameState.GameOver: // set on death
                break;
            case GameState.Scoreboard: // set if we view the scoreboard, if we want to do anything fancy.  Optional.
                break;
            default:
                break;
        }

        //ChangeGameState?.Invoke(state); 
        //was going to try to do invokes for state changes, but thats too complicated right now
    }

    //a quicker way to check if we are in a state instead of using actions.
    //If you want to use actions then change this up.
    public bool CompareState(GameState check)
    {
        return check == Instance.gameState;
    }

    public void FadeInMenu()
    {
        StartCoroutine(FadeInMenuAnim());
    }

    public void FadeOutMenu()
    {
        StartCoroutine(FadeOutMenuAnim());
    }

    public void FadeInOverlay()
    {
        StartCoroutine(FadeOutMenuAnim());
    }

    private IEnumerator FadeInMenuAnim()
    {
        yield return new WaitForEndOfFrame();
        LeanTween.alphaCanvas(startCanvas.GetComponent<CanvasGroup>(), 1f, 1.5f).setEaseInQuad();
        yield return new WaitForSeconds(1.5f);
        startCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private IEnumerator FadeOutMenuAnim()
    {
        yield return new WaitForEndOfFrame();
        startCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        LeanTween.alphaCanvas(startCanvas.GetComponent<CanvasGroup>(), 0f, 1f);
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocalY(mainCamera, 0f, 1f).setEaseInOutQuad();
        LeanTween.value(mainCamera, mainCamera.GetComponent<Camera>().orthographicSize, 6f, 1f).setEaseInOutQuad().setOnUpdate((float flt) => {
            mainCamera.GetComponent<Camera>().orthographicSize = flt;
        });
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeInOverlayAnim());
        // UpdateGameState(GameState.Gameplay);
    }

    private IEnumerator FadeInOverlayAnim()
    {
        yield return new WaitForEndOfFrame();

        // Collects children of the Canvas
        List<GameObject> overlayParts = new List<GameObject>();
        foreach (Transform child in overlayCanvas.transform) { overlayParts.Add(child.gameObject); }
        
        // Fades in the Overlay Canvas
        LeanTween.alphaCanvas(overlayCanvas.GetComponent<CanvasGroup>(), 1f, 0.2f);

        // Expands the divider bar
        Vector2 oldDelta = dividingBar.GetComponent<RectTransform>().sizeDelta;
        LeanTween.value(dividingBar, oldDelta.x, 750f, 1f).setEaseOutQuad().setOnUpdate((float flt) => {
            dividingBar.GetComponent<RectTransform>().sizeDelta = new Vector2(flt,  oldDelta.y);
        });

        // Pans up all the signs
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocalY(distanceSign, -20f, 0.7f).setEaseOutExpo();
        yield return new WaitForSeconds(0.3f);
        LeanTween.moveLocalY(waterSign, -20f, 0.7f).setEaseOutExpo();
        LeanTween.moveLocalY(healthSign, -20f, 0.7f).setEaseOutExpo();
        yield return new WaitForSeconds(0.8f);
        LeanTween.moveLocalX(waterGauge, -0f, 1f).setEaseOutBounce();
        LeanTween.moveLocalX(healthGauge, -0f, 1f).setEaseOutBounce();

    }

    private void FindUIAssets() {
        dividingBar = GameObject.Find("Dividing Bar");
        distanceSign = GameObject.Find("Distance Sign");
        healthSign = GameObject.Find("Health Sign");
        healthGauge = GameObject.Find("Health Gauge");
        waterSign = GameObject.Find("Water Sign");
        waterGauge = GameObject.Find("Water Gauge");
    }

}
