using UnityEngine;

public class FoodType : MonoBehaviour
{
    GameObject _spriteEntry;    //food indicator (from a scriptable object)
    public EnemyStats enemyStats; //foodData
    [SerializeField] Vector3 offset;

    void Start()
    {
        _spriteEntry = enemyStats.GetRandomEntry(); //getting the right food sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer foodObject = _spriteEntry.GetComponent<SpriteRenderer>();

        //setting food sprite and its color to this object's sprite and color
        if (_spriteEntry != null)
        {
            spriteRenderer.sprite = foodObject.sprite;
            spriteRenderer.color = foodObject.color;
        }
    }

    void Update()
    {
        transform.position = transform.parent.position + offset;
    }

    //return the tag of the food sprite 
    public string getName()
    {
        return _spriteEntry.tag;
    }
}