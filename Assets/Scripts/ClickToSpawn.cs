using UnityEngine;
using System.Collections;

public class ClickToSpawn : MonoBehaviour {
    public GameObject prefab;
    public float requiredEmptyRadius;

    const int Left = 0;

    void Update() {
        if (Input.GetMouseButtonDown(Left)) {
            Vector2 position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Physics2D.OverlapCircle(position, requiredEmptyRadius)) {
                Instantiate(prefab, position, Quaternion.identity);
            }
        }
    }
}
