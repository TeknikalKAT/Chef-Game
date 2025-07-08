using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponHandler[] weaponHandlers;
    [SerializeField] int currentGunNumber;
    [SerializeField] float dualWeaponTimer = 20f;
    InputController inputController;
    //[Header("Power Up Timers")]
    //float spreadWeaponTimer = 0f;
    float _dualWeaponTimer = 0f;
    bool _dualMode;
    public bool dualMode
    {
        get { return _dualMode; }
        set { _dualMode = value; }
    }
    void Start()
    {
        _dualMode = false;
        currentGunNumber = 0;
        inputController = GameObject.Find("Game Manager").GetComponent<InputController>();
        ActiveWeapon();
    }
    // Update is called once per frame
    void Update()
    {
        _dualWeaponTimer -= Time.deltaTime;
        if (_dualWeaponTimer <= 0)
            _dualWeaponTimer = 0;
        SwitchWeapons();
    }
    void ActiveWeapon()
    {
        for (int i = 0; i < weaponHandlers.Length; i++)
        {
            if (i == currentGunNumber)
                weaponHandlers[i].isActive = true;
            else
                weaponHandlers[i].isActive = false;
        }
    }
    void SwitchWeapons()
    {
        if (inputController.forwardSwitch)
            ForwardSwitch();
        if(inputController.backwardSwitch)
            BackwardSwitch();
    }
    public void ForwardSwitch()
    {
        StartCoroutine(SwitchTime());    
        if ((currentGunNumber + 1) != weaponHandlers.Length)
                currentGunNumber += 1;
            else
                currentGunNumber = 0;
            ActiveWeapon();   
    }
    IEnumerator SwitchTime()
    {
        yield return new WaitForSeconds(1);
    }
    public void BackwardSwitch()
    {
        StartCoroutine(SwitchTime());
        if ((currentGunNumber - 1) != -1)
             currentGunNumber -= 1;
       else
           currentGunNumber = weaponHandlers.Length - 1;
       ActiveWeapon();
    }


    //void HandlingTimes()
    //{
    //    spreadWeaponTimer -= Time.deltaTime;
    //    dualWeaponTimer -= Time.deltaTime;
    //    if (spreadWeaponTimer <= 0)
    //    {
    //        //we may have to change the projectiles in here or something
    //        spreadWeaponTimer = 0;
    //    }
    //    if (dualWeaponTimer <= 0)
    //        DefaultGun();
    //}
    //void DefaultGun()
    //{
    //    guns[0].SetActive(true);
    //    for (int i = 1; i < guns.Length; i++)
    //        guns[i].SetActive(false);

    //}


    //knowing what powerup to use
    public void Powerup(int index)
    {
        switch (index)
        {
            case 1:
                DualWeapon();
                break;
        }
    }
    void DualWeapon()
    {
        _dualMode = true;
        _dualWeaponTimer = dualWeaponTimer;
    }
    public float DualTime()
    {
        return _dualWeaponTimer;
    }

}
