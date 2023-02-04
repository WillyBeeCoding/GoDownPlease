using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    public Vector2 startPos;
    public float followStr = 12f;
    public float linearDrag = 10.0f;
    public bool useObjectTrail = false;
    public bool useRenderTrail = true;

    Rigidbody2D rb;
    Vector2 lastNode;
    public GameObject rootTrail;
    public GameObject trailHolder;

    float trailPos;
    Vector2 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.drag = linearDrag;

        lastNode = transform.position;
        prevPosition = transform.position;
        

    }
    //KEEP MOVEMENT ON UPDATE FOR RESPONSIVENESS
    private void Update()
    {
        

    }

    // KEEP ROTATION ON FIXEDUPDATE FOR ACCURACY
    void FixedUpdate()
    {
        //calculate velocity manually due to parenting
        Vector2 worldVel = ((Vector2)transform.position - prevPosition);
        prevPosition = transform.position;


        //create temp vec to lock mouse at Y and still retain physics
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tempVec = new(mousePos.x, transform.parent.transform.position.y);
        // mouse follow
        Vector2 targ = tempVec - (Vector2)transform.position;
        rb.AddForce((targ * followStr - worldVel) * rb.mass);


        //Rotation
        Quaternion targRot = Quaternion.FromToRotation(-Vector3.up, worldVel);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targRot, 1f);
    }
}
