using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    public Vector2 startPos;
    public float speed; //unused
    public float followStr = 2.5f;
    public float linearDrag = 20;
    public float centerPos;//unused
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
        rb.freezeRotation = true;

    }
    //KEEP MOVEMENT ON UPDATE FOR RESPONSIVENESS
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tempVec = new Vector2(mousePos.x, transform.parent.position.y);
        // mouse follow
        Vector2 targ = tempVec - (Vector2)transform.position;
        rb.AddForce((targ * followStr - rb.velocity) * rb.mass);

        //Root trail
        if (Vector3.Distance(lastNode, rb.position) > 0.04f & useObjectTrail)
        {
            GameObject tempTrail = Instantiate(rootTrail);
            tempTrail.transform.SetPositionAndRotation(trailHolder.transform.position, trailHolder.transform.rotation);
            lastNode = transform.position;

        }
    }

    // KEEP ROTATION ON FIXEDUPDATE FOR ACCURACY
    void FixedUpdate()
    {
        //calculate velocity manually due to parenting
        Vector2 worldVel = ((Vector2)transform.position - prevPosition);
        prevPosition = transform.position;

        //Rotation
        Quaternion targRot = Quaternion.FromToRotation(-Vector3.up, worldVel);
        transform.localRotation = Quaternion.Lerp(transform.rotation, targRot, 0.3f);
    }
}
