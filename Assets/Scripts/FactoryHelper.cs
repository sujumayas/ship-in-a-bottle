using UnityEngine;
using System.Collections;

public class FactoryHelper : MonoBehaviour {

    public float targetValue;
    Renderer spriteRend;
    public float fadePace;

    public GameObject targetClickeable;

    void Start () {
        spriteRend = transform.parent.FindChild ("Sprite").GetComponent<Renderer> ();
    }

    void Update () {
        
        if (spriteRend.material.color.a != targetValue) {
            Color temp = spriteRend.material.color;
            temp.a = Mathf.MoveTowards (temp.a, targetValue, fadePace * Time.deltaTime);
            spriteRend.material.color = temp;
        }

        if ((spriteRend.material.color.a == 0) && !targetClickeable.activeInHierarchy) {
            targetClickeable.SetActive (true);
        } else if ((spriteRend.material.color.a != 0) && targetClickeable.activeInHierarchy) {
            targetClickeable.SetActive (false);
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
