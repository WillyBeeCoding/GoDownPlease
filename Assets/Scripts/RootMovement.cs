using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMovement : MonoBehaviour
{
    public float rootSpeed;
    private Rigidbody2D rb;
    private Vector2 rootDirection;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() {
        float directionX = Input.GetAxisRaw("Horizontal");
        rootDirection = new Vector2(directionX, 0).normalized;
    }

    private void FixedUpdate() {
        Debug.Log("X POS " + transform.localPosition.x);
        rb.velocity = new Vector2(rootDirection.x * rootSpeed, 0);
    }
}
