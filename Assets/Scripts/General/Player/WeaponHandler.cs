using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject[] guns;
    [SerializeField] float maxAmount = 15f;
    [SerializeField] float dualFillAmount = 30f;

    [SerializeField] bool infinite = false;
    [SerializeField] float _amountLeft; //will be made private later
    [SerializeField] float reloadTime = 3f;

    [Header("Food in Stock")]
    [SerializeField] float stockAmount;
    [SerializeField] float _stockAmount;
    [SerializeField] float pReloadTime = 5f;

    //Getters and setters
    private bool _isActive;
    private bool _dualMode;
    bool _isReloading;
    public bool dualMode
    {
        get { return _isActive; }
        set { _isActive = value; }
    }
    public bool isActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
        }
    }
    public bool isReloading
    {
        get { return _isReloading; }
    }

    float _dualTime;
    bool dualModeFill = false;      //to set fill amount for duality
    float amountBeforeDual = 0;
    [SerializeField] float _pReloadTime;

    WeaponManager weaponManager;
    InputController inputController;

    private void Start()
    {
        _stockAmount = 0;
        _amountLeft = maxAmount;
        weaponManager = GameObject.Find("WeaponsManager").GetComponent<WeaponManager>();
        inputController = GameObject.Find("Game Manager").GetComponent<InputController>();
        _pReloadTime = 0;
    }
    private void Update()
    {
        ActiveOrNot();
    }

    void ActiveOrNot()
    {
        //extra conditions may be added here
        if (_isActive)
        {
            /*Some players may switch between weapons to avoid losing the dual power quickly
             Hence, a timer would also be necessary in this*/
           if (weaponManager.dualMode && weaponManager.DualTime() > 0)
            {
                guns[0].SetActive(false);
                guns[1].SetActive(true);
         
                //to set the fill amount just once
                if (!dualModeFill)
                    DualModeFill();

                if (_amountLeft <= 0)
                    BackToSingle();
            }
            else //single weapon mode
            {
                guns[0].SetActive(true);
                guns[1].SetActive(false);
                if(_amountLeft <= 0 || _amountLeft < maxAmount && inputController.isReloading)
                {
                    ActiveReload();
                }

                //reloading logic and stuff come here
            }
        }
        else
        {
            guns[0].SetActive(false);
            guns[1].SetActive(false);
            if(_amountLeft < maxAmount || _stockAmount < stockAmount)
                PassiveReload();
        }    
    }

    void DualModeFill()
    {
        amountBeforeDual = _amountLeft;
        _amountLeft = dualFillAmount;
        dualModeFill = true;           //to prevent constant setting of the above
    }
    void ActiveReload()
    {
        if(_stockAmount <= 0)
        {
            //we don't want to switch when there are 'bullets' left
            if (_amountLeft > 0)
                return;
            weaponManager.ForwardSwitch();
        }
        else
        {
            StartCoroutine(Reloading());
        }
    }

    void PassiveReload()
    {
        _pReloadTime += Time.deltaTime;
        if(_pReloadTime >= pReloadTime)
        {
            //first refill the immediate stash before filling the stock
            if (_amountLeft < maxAmount)
                _amountLeft += 1;
            else if (_stockAmount < stockAmount)
                _stockAmount += 1;
            _pReloadTime = 0;
        }

    }
    void BackToSingle()
    {
        _amountLeft = amountBeforeDual;
        weaponManager.dualMode = false;
        dualModeFill = false;       //for the next time we get a dual weapon
    }
    public void Fire()
    {
        if (_amountLeft > 0 && !infinite)
            _amountLeft -= 1;
    }

    public float AmountLeft()
    {
        return _amountLeft;
    }
    IEnumerator Reloading()
    {

        _isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        float amountToAdd = maxAmount - _amountLeft;
        if (amountToAdd <= _stockAmount)
        {
            _amountLeft += amountToAdd;
            _stockAmount -= amountToAdd;
        }
        else
        {
            _amountLeft += _stockAmount;
            _stockAmount = 0;
        }
        _isReloading = false;
    }
    

}
