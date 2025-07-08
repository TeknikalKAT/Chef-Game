using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] GameObject[] foods;
    Weapon_1[] weapons;
    [SerializeField] int currentFoodNumber;

    InputController inputController;
    void Start()
    {
        //we may change this to get the last food used before moving to the next level
        currentFoodNumber = 0;
        inputController = GameObject.Find("Game Manager").GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        weapons = GameObject.FindObjectsByType<Weapon_1>(FindObjectsSortMode.None);

        foreach(Weapon_1 weapon in weapons)
        {
            weapon.GetFood(foods[currentFoodNumber]);
        }
        SwitchFoods();
    }

    void SwitchFoods()
    {
        //forward switching
        if(inputController.forwardSwitch)
        {
            if ((currentFoodNumber + 1) != foods.Length)
                currentFoodNumber += 1;
            else
                currentFoodNumber = 0;
        }

        //reverse switching
        if(inputController.backwardSwitch)
        {
            if ((currentFoodNumber - 1) != -1)
                currentFoodNumber -= 1;
            else
                currentFoodNumber = (foods.Length) - 1;
        }
    }

}
