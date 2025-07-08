using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float damageAmount = 1f;
    [SerializeField] float lifeTime = 5f;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }
    void FixedUpdate()
    {
        rb.linearVelocity = transform.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<EnemyHealth>())
        {
            FoodType foodType = collision.GetComponentInChildren<FoodType>();
            string foodName = foodType.getName();
            //food tag being all means any projectile can damage enemy
            if (foodName == "all")
            {
                Debug.Log("All");
                collision.GetComponent<EnemyHealth>().DamageHealth(damageAmount);
            }
            else if (gameObject.CompareTag(foodName))   //checking if projectile's tag is same as expected food tag
            {
                collision.GetComponent<EnemyHealth>().DamageHealth(damageAmount);
            }
        }

        Destroy(gameObject);
    }

}
