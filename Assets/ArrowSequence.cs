using UnityEngine;
using System.Collections.Generic;

using System;

public class ArrowSequence : MonoBehaviour
{
    private List<KeyCode> arrow_key_codes = new List<KeyCode> {
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow
    };
    private List<KeyCode> arrow_sequence = new List<KeyCode>();
    private int current_sequence_index = 0;
    private float timer = 0;
    private int SEQUENCE_LENGTH = 4;
    private float COMPLETION_TIME = 15f;
    public GameObject go;
    public List<GameObject> arrows;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
    }

    public void GenerateSequence()
    {   
        arrow_sequence.Clear();
        current_sequence_index = 0;
        List<int> sequence = new List<int>();
        for (int i = 0; i < SEQUENCE_LENGTH; i++) {
            // Add random indices from arrow_key_codes. This generates a random arrow key sequence
            int j = UnityEngine.Random.Range(0, 4);
            arrow_sequence.Add(arrow_key_codes[j]);

            GameObject newImage = Instantiate(arrows[j]);
            newImage.transform.SetParent(go.transform, false);
        }
        Debug.Log("Sequence is " + string.Join(", ", arrow_sequence));
    }


    // Update is called once per frame
    void Update()
    {
        // Update the failure timer
        if (timer > 0) {
            timer -= Time.deltaTime;
            // Check if time is up
            if (timer <= 0) {
                current_sequence_index = 0;
                timer = 0;
                Debug.Log("Sequence Failed");
                Destroy(GetComponent<Minigame>().gameObject);
            }
        }
        // Keep checking for arrow key presses as long as the sequence has not been finished
        if (current_sequence_index < arrow_sequence.Count) {
            foreach (KeyCode key in arrow_key_codes) {
                if (Input.GetKeyDown(key)) {
                    // Check if the current key press matches the current key in the sequence
                    if (key == arrow_sequence[current_sequence_index]) {
                        // Move on to the next key in the sequence
                        current_sequence_index++;
                        if (current_sequence_index == 1) {
                            // Start the timer on the first key press
                            timer = COMPLETION_TIME;
                        }
                        Debug.Log("key no " + current_sequence_index + " in sequence hit");
                        Destroy(go.transform.GetChild(0).gameObject);
                    } else {
                        // Reset the sequence
                        current_sequence_index = 0;
                        Debug.Log("Sequence Failed");
                        Destroy(GetComponent<Minigame>().gameObject);
                    }
                    break;
                }
            }
        } else {
            Debug.Log("Sequence Completed");
            this.enabled = false;
            GetComponent<Minigame>().Done();
            GetComponent<Minigame>().started = true;
        }
        
    }
}
