using UnityEngine;
using System.Collections.Generic;

public class GravityThing : MonoBehaviour
{
    public AnimationCurve gravity;
    public Rigidbody rb;
    private float a;
    public FixedJoint stacked;
    public float breakforce, breaktorque;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        a += Time.deltaTime;
    }

    private void FixedUpdate() {
        rb.AddForce(gravity.Evaluate(a) * Vector3.down);
    }

    private void OnCollisionEnter(Collision other) {
        if (!stacked && other.rigidbody && (other.rigidbody.TryGetComponent(out Player p) || (other.rigidbody.TryGetComponent(out GravityThing t) && t.stacked))) {
            stacked = gameObject.AddComponent<FixedJoint>();
            stacked.connectedBody = other.rigidbody;
            stacked.breakForce = breakforce;
            stacked.breakTorque = breaktorque;
        }
    }
}
