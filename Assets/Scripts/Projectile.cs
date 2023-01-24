using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f;
    float timerLife;
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        timerLife = lifeTime;   
    }

    // Update is called once per frame
    void Update()
    {
        timerLife -= Time.deltaTime;

        if (timerLife < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if(e != null)  
        {
            e.Fix();
        }

        Destroy(gameObject);
    }
}
