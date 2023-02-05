using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    private GameObject text;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>().gameObject;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        AudioSource.PlayClipAtPoint(hoverSound, Camera.main.transform.position, 0.1f);
        text.transform.localPosition = new Vector3(0f, -4f, 0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        text.transform.localPosition = new Vector3(0f, -2f, 0f);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, 0.1f);
        text.transform.localPosition = new Vector3(0f, -8f, 0f);
    }

}
