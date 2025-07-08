using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float rotateSpeed;
    [SerializeField] Transform rotatePoint;
    [SerializeField] float maxAngle, minAngle;

    InputController inputController;
    Vector3 mousePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputController = GameObject.Find("Game Manager").GetComponent<InputController>();
        if (rotatePoint == null)
            rotatePoint = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MouseControl();
    }

    void MouseControl()
    {
        mousePos = Camera.main.ScreenToWorldPoint(inputController.mousePosition);

        Vector3 direction = mousePos - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;


        Quaternion q = Quaternion.Euler(0, 0, angle);
        rotatePoint.rotation = Quaternion.Slerp(rotatePoint.rotation, q, rotateSpeed);

        ClampRotation();
    }

    void ClampRotation()
    {
        Vector3 eulerAngles = rotatePoint.rotation.eulerAngles;

        eulerAngles.z = (eulerAngles.z > 180) ? eulerAngles.z - 360 : eulerAngles.z;
        eulerAngles.z = Mathf.Clamp(eulerAngles.z, minAngle, maxAngle);
    }
}
