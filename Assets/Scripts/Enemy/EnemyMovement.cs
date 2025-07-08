using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 1f, minSpeed = 0.2f;
    [SerializeField] float rotateSpeed = 0.2f;
    [SerializeField] Transform rotatePoint;
    [SerializeField] GameObject[] spawnPoints;
    Color _spriteColor;
    [SerializeField] SatisfactionBarBehaviour satisfactionBar;
    [SerializeField] float _disappearRate = 0.03f;

    [Header("Damage Parameters")]
    [SerializeField] float damageAmount = 2f;
    [SerializeField] float damageTime = 5f;

    float _damageTime;
    [SerializeField] bool inRange;
    Transform targetPos;
    Rigidbody2D _rigidbody2D;
    EnemyHealth health;
    bool foundLeavingPath;
    FoodType _foodType;
    float _speed;


    GameObject gameManager;      //we'll replace this with an instance of the score manager, which will be the game manager

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = Random.Range(maxSpeed, minSpeed);
        _foodType = GetComponentInChildren<FoodType>();
        satisfactionBar = GetComponentInChildren<SatisfactionBarBehaviour>();
        foundLeavingPath = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        targetPos = GameObject.FindWithTag("Player").transform;
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        health = GetComponent<EnemyHealth>();

        gameManager = GameObject.Find("Game Manager");
        if (rotatePoint == null)
            rotatePoint = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        _spriteColor = gameObject.GetComponent<SpriteRenderer>().color;

        if (health.HealthValue() <= 0)
        {
            if (!foundLeavingPath)
                StartCoroutine(LeaveRestaurant());
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(_spriteColor.r, _spriteColor.g, _spriteColor.b, _spriteColor.a - _disappearRate);
            }

            if (Vector2.Distance(transform.position, targetPos.position) <= 0.5f || _spriteColor.a <= 0)
                Destroy(gameObject);

        }
        else
        {
            if (inRange)
            {
                _damageTime -= Time.deltaTime;
            }
            else
            {
                _damageTime = damageTime;
            }
             AttackPlayer();
        }

        Vector3 direction = targetPos.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion q = Quaternion.Euler(0, 0, angle);
        rotatePoint.rotation = Quaternion.Slerp(rotatePoint.rotation, q, rotateSpeed);

    }
    private void FixedUpdate()
    {
        if (inRange)
            _rigidbody2D.linearVelocity = Vector2.zero;
        else
            _rigidbody2D.linearVelocity = transform.up * _speed;
    }

    IEnumerator LeaveRestaurant()
    {

        _speed = 0;
        yield return new WaitForSeconds(0.5f);
        float leaveSpeed = 2f;
        _speed = leaveSpeed;
        satisfactionBar.gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        Transform leavePath = spawnPoints[0].transform;

        float shortestPath = Vector2.Distance(transform.position, leavePath.position);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, spawnPoints[i].transform.position);
            if (shortestPath > distance)
            {
                shortestPath = distance;
                leavePath = spawnPoints[i].transform;
            }

        }
        targetPos = leavePath;

        foundLeavingPath = true;

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    void AttackPlayer()
    {
        if (_damageTime <= 0)
        {
            gameManager.GetComponent<HealthManager>().HurtPlayer(damageAmount);
            _damageTime = damageTime;
        }

    }
}
