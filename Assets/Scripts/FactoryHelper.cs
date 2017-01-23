using UnityEngine;
using System.Collections;

public class FactoryHelper : MonoBehaviour {

    public float targetValue;
    Renderer spriteRend;
    public float fadePace;

    void Start () {
        spriteRend = transform.parent.FindChild ("Sprite").GetComponent<Renderer> ();
    }

    void Update () {
        
        if (spriteRend.material.color.a != targetValue) {
            Color temp = spriteRend.material.color;
            temp.a = Mathf.MoveTowards (temp.a, targetValue, fadePace * Time.deltaTime);
            spriteRend.material.color = temp;
        }
    }

	void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            targetValue = 0;
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if (other.CompareTag ("Player")) {
            targetValue = 1;
        }
    }
}
