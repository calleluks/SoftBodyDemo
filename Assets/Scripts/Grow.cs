using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour {
    public float movementSmoothTime = 0.2f;
    public float movementMaxSpeed = 1;
    public float acceleration = 2;
    public float scaleStep = 5;
    public float massStep = 10;
    public GameObject explosion;

    new Transform transform;
    new Rigidbody2D rigidbody;
    bool growing = true;
    float startTime;
    float startScale;
    float startMass;
    const int Left = 0;
    Vector3 velocity = Vector3.zero;

    void Start() {
        GetComponent<Renderer>().material.color =
            HSV(Random.value, 0.99f, 0.99f);
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
        startTime = Time.time;
        startScale = transform.localScale.x;
        startMass = rigidbody.mass;
    }

    void Update() {
        if (growing) {
            if (Input.GetMouseButtonUp(Left)) {
                StopGrowing();
                return;
            }

            float time = Time.time - startTime;
            float growth = Mathf.Log(time / (1 / acceleration) + 1);
            float scale = scaleStep * growth + startScale;
            transform.localScale = new Vector2(scale, scale);
            rigidbody.mass = massStep * growth + startMass;

            Vector3 targetPosition =
                (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.SmoothDamp(transform.position,
                    targetPosition, ref velocity, movementSmoothTime,
                    movementMaxSpeed);
        }
    }

    void StopGrowing() {
        growing = false;
        rigidbody.isKinematic = false;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (growing) {
            StopGrowing();
        }
    }

    Color HSV(float h, float s, float v) {
        int hi = (int)(h * 6);
        float f = h * 6 - hi;
        float p = v * (1 - s);
        float q = v * (1 - f * s);
        float t = v * (1 - (1 - f) * s);

        switch (hi) {
            case 0:
                return new Color(v, t, p);
            case 1:
                return new Color(q, v, p);
            case 2:
                return new Color(p, v, t);
            case 3:
                return new Color(p, q, v);
            case 4:
                return new Color(t, p, v);
            default:
                return new Color(v, p, q);
        }
    }
}
