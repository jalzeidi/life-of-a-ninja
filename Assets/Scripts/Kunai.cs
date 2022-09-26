using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public AudioClip slice;

    Rigidbody2D rigidbody2d;
    AudioSource source;
    float lifeTime = 3.0f;
    float timer = 0.0f;

    // Awake is called as soon as the object is instantiated
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > lifeTime) 
        {
            Destroy(gameObject);
        }
    }

    public void Throw(Vector2 direction, float force) 
    {
        rigidbody2d.AddForce(direction * force);
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Kunai Hit");
        source.PlayOneShot(slice);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

    public void SetAudioSource(AudioSource source)
    {
        this.source = source;
    }
}
