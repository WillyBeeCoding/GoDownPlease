using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehaviour : MonoBehaviour
{
    public Vector2 startPos;
    public float followStr = 15f;
    public float linearDrag = 5f;
    public bool useObjectTrail = false;
    public bool useRenderTrail = true;
    public float wobble;
    public float wobbleMax = 1.5f; //TODO: Smooth this out

    Rigidbody2D rb;
    public GameObject rootTrail;
    public GameObject trailHolder;

    Vector2 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        prevPosition = transform.position;
    }

    //KEEP MOVEMENT ON UPDATE FOR RESPONSIVENESS
    private void Update()
    {

    }

    // KEEP ROTATION ON FIXEDUPDATE FOR ACCURACY
    void FixedUpdate()
    {
        rb.drag = linearDrag;
        //calculate velocity manually due to parenting
        Vector2 worldVel = ((Vector2)transform.position - prevPosition);
        prevPosition = transform.position;

        

        if (GameManager.Instance.CompareState(GameState.Gameplay))
        {
            if (!IsInvoking(nameof(GetWobble)))
            {
                Invoke(nameof(GetWobble), 0.3f);
                wobble = GetWobble();
            }

            // create temp vec to lock mouse at Y and still retain physics
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 tempVec = new(mousePos.x + wobble, transform.parent.transform.position.y);

            // mouse follow
            Vector2 targ = tempVec - (Vector2)transform.position;
            rb.AddForce((targ * followStr - worldVel) * rb.mass);

            //Rotation
            Quaternion targRot = Quaternion.FromToRotation(-Vector3.up, worldVel);
            transform.rotation = Quaternion.Lerp(transform.rotation, targRot, 1f);
        }
    }

    public float GetWobble()
    {
        float temp = Resources.Instance.GetParchedAmount() * wobbleMax;
        return Random.Range(-temp, temp);//weebls wobble TODO: Smooth this out
    }
}
