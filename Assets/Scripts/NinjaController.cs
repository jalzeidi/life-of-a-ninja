using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public GameObject hitBoxPrefab;
    public GameObject kunaiPrefab;
    public float speed;
    public float throwForce;
    public AudioClip swing;

    Rigidbody2D rigidbody2d;
    float horizontal;
    Vector2 lookDirection;
    Animator animator;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        lookDirection = new Vector2(1, 0);
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        
        Vector2 move = new Vector2(horizontal, 0);

        if(!Mathf.Approximately(move.x, 0.0f))
        {
            lookDirection.Set(move.x, 0);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look", lookDirection.x);
        animator.SetFloat("Speed", move.magnitude);

        if(Input.GetKeyUp(KeyCode.H))
        {
            Attack();
        }
        if(Input.GetKeyUp(KeyCode.J))
        {
            Throw();
        }
    }

    void FixedUpdate() 
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        source.PlayOneShot(swing);
        Vector2 hitBoxOffset = new Vector2(0.8f * lookDirection.x, 0.3f);
        GameObject hitBox = Instantiate(hitBoxPrefab, rigidbody2d.position + hitBoxOffset, Quaternion.identity);
        Destroy(hitBox, 0.5f);
    }

    void Throw()
    {
        Quaternion kunaiOrientation = lookDirection.x > 0 ? Quaternion.identity: Quaternion.Euler(0, 0, 180);
        GameObject kunaiObject = Instantiate(kunaiPrefab, rigidbody2d.position + new Vector2(1.2f * lookDirection.x, -0.25f), kunaiOrientation);
        Kunai kunai = kunaiObject.GetComponent<Kunai>();
        kunai.SetAudioSource(source);
        kunai.Throw(lookDirection, throwForce);
        animator.SetTrigger("Throw");
    }
}
