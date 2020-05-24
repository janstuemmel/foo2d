using UnityEngine;

public class Grappling : MonoBehaviour {

    public float maxCamFieldOfView = 70f;
    public float minCamFieldOfView = 50f;

    private Camera cam;
    private LineRenderer rope;
    private Rigidbody2D rb;
    private GameObject hinge;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rope = GetComponent<LineRenderer>();
        cam = Camera.main;
        cam.fieldOfView = minCamFieldOfView;
    }

    void LateUpdate() {
        DrawRope();
    }

    void Update() {
        
        // float vel = Mathf.Max(Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.y));

        // if (vel > 8f) {
        //     if (cam.fieldOfView < maxCamFieldOfView) {
        //         cam.fieldOfView += 1f;
        //     }
        // } else {
        //     if (cam.fieldOfView > minCamFieldOfView) {
        //         cam.fieldOfView -= 1f;
        //     }            
        // }

        if(Input.GetMouseButtonDown(0)) {
 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, GetMouseDirection(), Mathf.Infinity, 1 << LayerMask.NameToLayer("Hinge"));

            if (hit.collider != null) {   
                hinge = hit.collider.gameObject;
                SpringJoint2D joint = hinge.GetComponentInParent<SpringJoint2D>();
                joint.connectedBody = rb;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (hinge != null) {
                hinge.GetComponentInParent<SpringJoint2D>().connectedBody = null;
                hinge = null;
            }
        }
    }

    private void DrawRope() {

        if (hinge != null) {
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
