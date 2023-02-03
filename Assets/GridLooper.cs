using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridLooper : MonoBehaviour
{
    public float backgroundSpeed;
    public Renderer backgroundRenderer;
    public bool panUp;

    // Update is called once per frame
    void Update()
    {
        if (panUp && transform.localPosition.y < 15f) {
            transform.position += new Vector3(0, backgroundSpeed * Time.deltaTime, 0);
        } else if (transform.localPosition.y < 15f) {
            backgroundRenderer.material.mainTextureOffset -= new Vector2(0, backgroundSpeed * Time.deltaTime);
        }
    }

}
