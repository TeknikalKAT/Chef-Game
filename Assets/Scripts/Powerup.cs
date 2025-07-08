using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] int powerIndex = 1;
    [SerializeField] float maxHealth = 5f;
    WeaponManager weaponManager;
    [SerializeField] float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        weaponManager = GameObject.Find("WeaponsManager").GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            PowerUp();
        }
    }
    void PowerUp()
    {
        weaponManager.Powerup(powerIndex);
        Destroy(gameObject);
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
    }
}
