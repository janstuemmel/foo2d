using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour {

    public string blockTag;
    public string lavaTag;
    public GameObject menu;

    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == blockTag) {
            StartCoroutine(Dissolve(other));
        }

        if(other.gameObject.tag == lavaTag) {

            // destroy parent player object
            // Destroy(transform.parent.gameObject);
            
            // show menu on death
            menu.SetActive(true);
        }
    }

    IEnumerator Dissolve(Collider2D other) {

        Material mat = other.GetComponent<SpriteRenderer>().material;

        float fade = 1f;

        while (fade >= 0f) {
            
            fade -= Time.deltaTime * 5;
            
            mat.SetFloat("_Fade", fade);

            yield return null;
        }

        Destroy(other.gameObject);
    }
}
