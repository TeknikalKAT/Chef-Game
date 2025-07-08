using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            GameOver();
    }

    public void HurtPlayer(float damageAmount)
    {
        health -= damageAmount;
    }
    void GameOver()
    {
        player.enabled = false;
        Weapon_1[] weapons = GameObject.FindObjectsByType<Weapon_1>(FindObjectsSortMode.None);
        foreach(Weapon_1 weapon in weapons)
        { 
            weapon.enabled = false; 
        }
        Debug.Log("Game Over!!");
    }
}
