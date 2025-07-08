using System.Collections;
using UnityEngine;

public class Weapon_1 : MonoBehaviour
{
    [SerializeField] float shootRate = 0.2f;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject projectile;     //may make it an array later on
    [SerializeField] WeaponHandler weaponHandler;

    float _shootRate;
    InputController inputController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shootRate = shootRate;
        inputController = GameObject.Find("Game Manager").GetComponent<InputController>();
    }

    // Update is called once per frame
    private void Update()
    {
        _shootRate -= Time.deltaTime;
        if (_shootRate <= 0)
            _shootRate = 0;
        Fire();        
    }
    void Fire()
    {
        if (inputController.isFiring && _shootRate <= 0 && weaponHandler.AmountLeft() > 0 && !weaponHandler.isReloading)
        {
            GameObject shot = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            _shootRate = shootRate;
            weaponHandler.Fire();
        }
    }
    public void GetFood(GameObject food)
    {
        projectile = food;
    }
}
