using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int score;
    public int lives;
    public GameObject startCanvas;
    public GameObject mainCamera;
    public bool gameStarted;
    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        FadeInMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted) {
            transform.position += new Vector3(0, -cameraSpeed * Time.deltaTime, 0);
        }
    }

    public void FadeInMenu() {
        StartCoroutine(FadeInMenuAnim());
    }

    public void FadeOutMenu() {
        StartCoroutine(FadeOutMenuAnim());
    }

    private IEnumerator FadeInMenuAnim() {
        yield return new WaitForEndOfFrame();
        LeanTween.alphaCanvas(startCanvas.GetComponent<CanvasGroup>(), 1f, 1.5f).setEaseInQuad();
        yield return new WaitForSeconds(1.5f);
        startCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private IEnumerator FadeOutMenuAnim() {
        yield return new WaitForEndOfFrame();
        startCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        LeanTween.alphaCanvas(startCanvas.GetComponent<CanvasGroup>(), 0f, 1f);
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocalY(mainCamera, 0f, 1f).setEaseInOutQuad();
        LeanTween.value(mainCamera, mainCamera.GetComponent<Camera>().orthographicSize, 6f, 1f).setEaseInOutQuad().setOnUpdate((float flt) => {
            mainCamera.GetComponent<Camera>().orthographicSize = flt;
        });
        yield return new WaitForSeconds(1f);
        gameStarted = true;
    }


}
