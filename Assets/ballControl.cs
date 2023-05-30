using UnityEngine;
using Unity.Netcode;

public class ballControl : MonoBehaviour {

    public float impulse = 1f;
    public float frictionCoffecient3d = 0.1f;
    public float gravity3d = 9.8f;

    private void Update() {
        FrictionSimulate3d();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 resultantVector = new Vector2(0f,0f);
        foreach(ContactPoint2D contact in collision.contacts) {
            Debug.Log(contact.point);
            //GetComponent<RectTransform>().anchoredPosition
            resultantVector += ((Vector2)transform.position - contact.point).normalized;
            //GetComponent<Rigidbody2D>().AddForce((GetComponent<RectTransform>().anchoredPosition - contact.point).normalized * force);
        }
        resultantVector += GetComponent<Rigidbody2D>().velocity.normalized;
        GetComponent<Rigidbody2D>().velocity = resultantVector * impulse;
    }

    private void FrictionSimulate3d() {
        Vector2 deceleration = -1 * GetComponent<Rigidbody2D>().velocity.normalized * frictionCoffecient3d * gravity3d;
        GetComponent<Rigidbody2D>().velocity += deceleration * Time.deltaTime;
    }
}