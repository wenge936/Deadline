using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player main;
    private Rigidbody rb;
    public AnimationCurve SpeedOverVel;
    public AnimationCurve SpeedOverDot;
    public float vel;
    public Animator anim;
    public int health = 5;
    public RawImage im;
    public List<Texture> sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) SceneManager.LoadScene("Menu");
        im.texture = sprites[health - 1];
    }

    void FixedUpdate() {
        Vector3 dir = GetMoveDir();
        anim.SetBool("R", rb.linearVelocity.magnitude > 1);
        float dot = Vector3.Dot(rb.linearVelocity, dir);
        var force = SpeedOverDot.Evaluate(dot) * SpeedOverVel.Evaluate(rb.linearVelocity.magnitude) * vel * dir;
        rb.AddForce(force, ForceMode.Acceleration);

    }

    private Vector3 GetMoveDir() {
        float horizontal = Input.GetAxis("Horizontal"); // Gets input from arrow keys or A/D
        float vertical = Input.GetAxis("Vertical");     // Gets input from arrow keys or W/S

        Vector3 moveDir = new Vector3(horizontal, 0, vertical); 
        return moveDir.normalized;
    }
}
