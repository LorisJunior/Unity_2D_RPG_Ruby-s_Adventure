using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AudioClip fixClip;
    public ParticleSystem smokeEffect;
    public float speed = 3f;
    public bool vertical = false;
    public float changeTime = 3f;

    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;

    Animator animator;

    bool broken = true;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if(timer < 0)    
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    void FixedUpdate() 
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;
        
        if(vertical)
        {
            position.y = position.y + speed * Time.deltaTime * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * Time.deltaTime * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
        audioSource.PlayOneShot(fixClip);
    }
}
