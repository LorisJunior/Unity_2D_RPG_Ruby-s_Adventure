using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public bool vertical = false;
    public float changeTime = 3f;
    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update() 
    {
        timer -= Time.deltaTime;

        if(timer < 0)    
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    void FixedUpdate() 
    {
        Vector2 position = rigidbody2d.position;
        
        if(vertical)
        {
            position.y = position.y + speed * Time.deltaTime * direction;
        }
        else
        {
            position.x = position.x + speed * Time.deltaTime * direction;
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
}
