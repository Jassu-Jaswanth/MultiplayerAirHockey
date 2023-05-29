using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    Rect playerBounds;

    private void Start() {
        playerBounds = new Rect(new Vector2(transform.position.x,transform.position.y) - (new Vector2(50f,50f)),new Vector2(100f, 100f));
    }

    private void Update() {
        if (Input.GetMouseButton(0) && playerBounds.Contains(Input.mousePosition)) {
            playerBounds = new Rect(new Vector2(transform.position.x, transform.position.y) - (new Vector2(50f, 50f)), new Vector2(100f, 100f));
            this.transform.position = Input.mousePosition;
            Debug.Log(Input.mousePosition);
        }
    }
}
