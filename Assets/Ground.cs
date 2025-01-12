using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.rigidbody && other.rigidbody.TryGetComponent(out GravityThing t)) {
            Destroy(other.gameObject);
        Player.main.health--;
        }
    }
}
