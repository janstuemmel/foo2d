using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    
    public float smoothing = 10f;
    public Vector3 offset;

    private Rigidbody2D rb;
    private Transform cam;

    void Start() {
        rb = player.GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Transform>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // StartCoroutine(Shake(0.15f, 0.1f));
        }
    }

    void FixedUpdate() {

        float vel = Mathf.Max(Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.y));

        Vector3 desiredPosition = offset + player.transform.position;

        desiredPosition.z -= Mathf.Clamp(vel * 0.1f, 0f, 6f);
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing * Time.fixedDeltaTime);
        
        transform.position = smoothedPosition;
    }

    private IEnumerator Shake(float duration, float magnitude) {
        
        Vector3 originalPosition = cam.position;
        
        float elapsed = 0f;

        while (elapsed < duration) {

            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            cam.position = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cam.position = originalPosition;
    }
}
