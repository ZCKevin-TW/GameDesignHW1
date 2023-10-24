using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFunction : MonoBehaviour
{
    // Start is called before the first frame update
    private bool visited;
    [SerializeField] private MaterialManager toggler;
    // public AudioClip bang;
    private AudioSource aus;
    void Start()
    {
        aus = GetComponent<AudioSource>();
        visited = false; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == false) return;
        visited = !visited;
        aus.Play();
        if (visited)
            toggler.reveal();
        else
            toggler.hide();
    }

    // Update is called once per frame
}
