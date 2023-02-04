using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum GameState
{
    MainMenu,
    Gameplay,
    GameOver,
    Scoreboard
}

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;  
    public GameState gameState;

    static public int score;
    public int lives;
    public GameObject startCanvas;
    public GameObject mainCamera;
    public Resources resources;
    public bool gameStarted;
    public float cameraSpeed;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
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

    public void UpdateGameState(GameState state)
    {
        Instance.gameState = state;
        switch (state)
        {
            case GameState.MainMenu: //set when we are in the main menu
                FadeInMenu();
                break;
            case GameState.Gameplay: // set when we are playing the game actively
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
        UpdateGameState(GameState.Gameplay);
    }

}
