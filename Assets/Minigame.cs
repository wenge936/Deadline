using UnityEngine;

public class Minigame : MonoBehaviour
{
    public LineRenderer line;
    public Transform hitbox;
    public Material done, start;
    public float age;
    public AnimationCurve size;
    public bool game;
    public bool started;
    private float a;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started) {
            a += Time.deltaTime;
            float s = size.Evaluate(a);
            hitbox.localScale = new Vector3(s, 1, s);

            var x = s;
            var y = s;

            line.SetPositions(new Vector3[] {new Vector3(x, y, 0), new Vector3(x, -y, 0), new Vector3(-x, -y, 0), new Vector3(-x, y, 0)});
            if (a>age) Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (!game && other.GetComponent<Rigidbody>() && other.GetComponent<Rigidbody>().TryGetComponent(out Player p)) {
            game = true;
            //Done();
            GetComponent<ArrowSequence>().enabled = true;
            GetComponent<ArrowSequence>().GenerateSequence();
            line.material = start;
        }

        if (other.GetComponent<Rigidbody>() && other.GetComponent<Rigidbody>().TryGetComponent(out GravityThing t)) {
            Destroy(other.gameObject);
            if (!started)         Player.main.health--;

        }
    }

    public void Done() {
        started = true;
        line.material = done;
    }
}
