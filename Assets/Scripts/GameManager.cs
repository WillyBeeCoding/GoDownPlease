using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public GameObject playerRoot;

    static public int currentScore;
    static public int highScore;

    public GameObject startCanvas;
    public GameObject overlayCanvas;
    public GameObject gameOverCanvas;
    public GameObject blackScreenCanvas;

    private GameObject mainMenuButtons;
    private GameObject dividingBar;
    private GameObject distanceSign;
    private GameObject healthSign;
    private GameObject healthGauge;
    private GameObject waterSign;
    private GameObject waterGauge;
    private GameObject gameOverCard;
    private GameObject newScoreDisp;
    private GameObject highScoreDisp;
    private GameObject gameOverButtons;

    public GridLooper currentGridLoop;

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
            currentScore = (int)(Time.time * 3.3f);
            overlayCanvas.GetComponentInChildren<TextMeshProUGUI>().text = currentScore.ToString();
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
            case GameState.Gameplay: // set when we start playing the game actively
                SpawnPlayer(new Vector2(0,0), true);
                //SpawnPlayerWithForce(new Vector2(0,0), new Vector2(0,-1), 100);
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

    private GameObject SpawnPlayer(Vector2 pos, bool isActive)
    {
        GameObject temp = Instantiate(playerRoot, transform, false);
        temp.SetActive(isActive);
        try {
            temp.GetComponentInChildren<TrailRenderer>(true).emitting = true;
        } catch (Exception e) { 
            Debug.LogError("You ain't got no trail dummy ------ " + e); 
        }
        return temp;
    }
    
    // private GameObject SpawnPlayerWithForce(Vector2 pos, Vector2 dir, float mag)
    // {
    //     GameObject temp = SpawnPlayer(pos, true);
    //     temp.GetComponent<Rigidbody2D>().AddForce(dir * mag);
    //     return temp;
    // }

    public void AdjustWaterUI() {
        if (GameManager.Instance.gameState == GameState.Gameplay) {
            float position = -146f * (1f - (Resources.Instance.Water / 100f));
            LeanTween.moveLocalX(waterGauge, position, 0.5f).setEaseOutQuad();
        }
    }

    public void AdjustHealthUI() {
        if (GameManager.Instance.gameState == GameState.Gameplay) {
            float position = -146f * (1f - (Resources.Instance.Health / 3f));
            LeanTween.moveLocalX(healthGauge, position, 0.5f).setEaseOutQuad();
        }
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

    private IEnumerator FadeInMenuAnim()
    {
        yield return new WaitForEndOfFrame();
        LeanTween.alphaCanvas(startCanvas.GetComponent<CanvasGroup>(), 1f, 1.5f).setEaseInQuad();
        yield return new WaitForSeconds(1.5f);
        LeanTween.moveLocalY(mainMenuButtons, -240f, 0.5f).setEaseOutExpo();
        yield return new WaitForSeconds(0.5f);
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
        LeanTween.moveLocalX(waterGauge, 0f, 1f).setEaseOutBounce();
        LeanTween.moveLocalX(healthGauge, 0f, 1f).setEaseOutBounce();

        // Starts moving the grid
        StartGrid();
        yield return new WaitForSeconds(1f);
        UpdateGameState(GameState.Gameplay);
    }

    public IEnumerator FadeInGameOver(bool parched)
    {
        yield return new WaitForEndOfFrame();
        StopGrid(parched);
        LeanTween.alphaCanvas(blackScreenCanvas.GetComponent<CanvasGroup>(), 1f, 2f);//.setEaseInQuad();
        yield return new WaitForSeconds(2f);
        UpdateGameState(GameState.GameOver);
        mainCamera.GetComponent<Camera>().orthographicSize = 3f;
        LeanTween.moveLocal(mainCamera, new Vector3(1.5f, 4f, mainCamera.transform.position.z), 0.01f);
        LeanTween.alphaCanvas(overlayCanvas.GetComponent<CanvasGroup>(), 0f, 0.01f);
        LeanTween.alphaCanvas(gameOverCanvas.GetComponent<CanvasGroup>(), 1f, 0.01f);
        LeanTween.moveLocalY(gameOverCard, gameOverCard.transform.localPosition.y + 10f, 1f).setEaseInOutQuad().setLoopPingPong();
        yield return new WaitForSeconds(0.1f);
        LeanTween.alphaCanvas(blackScreenCanvas.GetComponent<CanvasGroup>(), 0f, 2f);//.setEaseInQuad();
        yield return new WaitForSeconds(2f);
        LeanTween.moveLocalY(gameOverButtons, -240f, 0.5f).setEaseOutExpo();
        yield return new WaitForSeconds(0.5f);
        gameOverCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void StartGrid() {
        LeanTween.value(currentGridLoop.gameObject, currentGridLoop.backgroundSpeed, 3f, 5f).setEaseInOutQuad().setOnUpdate((float flt) => {
            currentGridLoop.backgroundSpeed = flt;
        });
    }

    private void StopGrid(bool parched) {
        float speed = parched ? 2f : 0f;
        Debug.LogWarning("PARCHED " + parched);
        LeanTween.value(currentGridLoop.gameObject, currentGridLoop.backgroundSpeed, 0f, speed).setEaseInOutQuad().setOnUpdate((float flt) => {
            currentGridLoop.backgroundSpeed = flt;
        });
    }

    private void FindUIAssets() {
        mainMenuButtons = GameObject.Find("Main Menu Buttons");
        dividingBar = GameObject.Find("Dividing Bar");
        distanceSign = GameObject.Find("Distance Sign");
        healthSign = GameObject.Find("Health Sign");
        healthGauge = GameObject.Find("Health Gauge");
        waterSign = GameObject.Find("Water Sign");
        waterGauge = GameObject.Find("Water Gauge");
        gameOverCard = GameObject.Find("Game Over Card");
        newScoreDisp = GameObject.Find("New Score Display");
        highScoreDisp = GameObject.Find("High Score Display");
        gameOverButtons = GameObject.Find("Game Over Buttons");
    }
}
