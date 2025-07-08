using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;

    [SerializeField] SatisfactionBarBehaviour satisfactionBar;
    Transform leavingSpot;
    [SerializeField] float health;
    [SerializeField] float interval = 2f;
    [SerializeField] float timer = 0f;
    [SerializeField] bool hit;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        satisfactionBar = GetComponentInChildren<SatisfactionBarBehaviour>();
        satisfactionBar.SetSatisfaction(health, maxHealth);
    }

    void Update()
    {
        if (health <= 0)
            health = 0;
        if (health >= maxHealth)
        {
            health = maxHealth;
            hit = false;
        }

        if (hit)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                IncreaseHealth(1f);
                timer = 0f;
            }
        }
    }
    public float HealthValue()
    {
        return health;
    }
    public void DamageHealth(float damageAmount)
    {
        hit = true;
        timer = 0f;
        health -= damageAmount;
        satisfactionBar.SetSatisfaction(health, maxHealth);
    }

    void IncreaseHealth(float increaseAmount)
    {
        health += increaseAmount;
        satisfactionBar.SetSatisfaction(health, maxHealth);
    }

}
