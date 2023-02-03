using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{

    public Vector2 startPos;
    public float speed; //unused
    public float sensitivity = 25;
    public float linearDrag = 4;
    public float centerPos;//unused
    public bool useObjectTrail = false;
    public bool useRenderTrail = true;

    Rigidbody2D rb;
    Vector2 lastNode;
    public GameObject rootTrail;
    public GameObject trailHolder;
    float trailPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.drag = linearDrag;

        lastNode = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // mouse follow
        Vector2 targ = mousePos - (Vector2)transform.position;
        rb.AddForce((targ*sensitivity - rb.velocity) * rb.mass);


        //Rotation
        //Vector2 neo = new Vector2(rb.velocity.x,0);
        Quaternion targRot = Quaternion.FromToRotation(-Vector3.up, rb.velocity);
        transform.localRotation = Quaternion.Lerp(transform.rotation, targRot, 0.3f);

        //Root trail

        if(Vector3.Distance(lastNode,rb.position) > 0.04f & useObjectTrail)
        {
            GameObject tempTrail = Instantiate(rootTrail);
            tempTrail.transform.position = trailHolder.transform.position;
            tempTrail.transform.rotation = trailHolder.transform.rotation;
            lastNode = transform.position;

        }


    }
}
