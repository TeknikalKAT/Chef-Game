using UnityEngine;

public class InputController : MonoBehaviour
{
    public Vector3 mousePosition;
    public bool isFiring;
    public bool forwardSwitch;
    public bool backwardSwitch;
    public bool isReloading;
    // Update is called once per frame
    void Update()
    {
        isFiring = Input.GetButton("Fire1");
        mousePosition = Input.mousePosition;
        forwardSwitch = Input.GetButtonDown("Forward Switch");
        backwardSwitch = Input.GetButtonDown("Reverse Switch");
        isReloading = Input.GetButtonDown("Reload");
    }
}
