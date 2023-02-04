using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    private GameObject text;
    
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>().gameObject;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        text.transform.localPosition = new Vector3(0f, -4f, 0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        text.transform.localPosition = new Vector3(0f, -2f, 0f);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        text.transform.localPosition = new Vector3(0f, -8f, 0f);
    }

}
