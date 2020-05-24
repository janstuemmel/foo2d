using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera cam;

    public float grappleDistance = 20f;

    private LineRenderer rope;
    private GameObject hinge;
    private Rigidbody2D rb;
    private Vector3 startPosition;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rope = GetComponent<LineRenderer>();
        startPosition = transform.position;
    }

    void LateUpdate() {
        DrawRope();
    }

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            StartGrapple();
        }
        
        if (Input.GetMouseButtonUp(0)) {
            StopGrapple();
        }
    }

    public void Spawn() {
        transform.position = startPosition;
    }

    void StartGrapple() {

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, 
            GetMouseDirection(),
            grappleDistance,
            1 << LayerMask.NameToLayer("Hinge")
        );

        if (hit.collider) {
            hinge = hit.collider.gameObject;
            hinge.GetComponentInParent<SpringJoint2D>().connectedBody = rb;
        }   
    }
    
    void StopGrapple() {
        if (hinge) {
            hinge.GetComponentInParent<SpringJoint2D>().connectedBody = null;
            hinge = null;
        }
    }

    void DrawRope() {
        if (hinge) {
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, hinge.transform.position);
        } else {
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, transform.position);
        }
    }

    Vector2 GetMouseDirection() {
        
        // define z position: https://answers.unity.com/questions/331558/screentoworldpoint-not-working.html
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector2 worldPosition = cam.ScreenToWorldPoint(screenPosition);
        
        return new Vector2(
            worldPosition.x - transform.position.x,
            worldPosition.y - transform.position.y
        );
    }
}
