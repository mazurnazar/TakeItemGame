using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] private string name;
    public string Name { get => name; private set { } }

    [SerializeField] private float speed = 5f;
    private bool selected;

    [SerializeField] private PlayerMovement playerMovement;
    private ObjectPool objectPool;
    public ObjectPool ObjectPool { get => objectPool; set=> objectPool = value; }
    private Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        name = gameObject.name;
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }
    // move product along x axis with speed
    void Move()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        if(!selected)
        Move();
    }
    // when mouse is clicked on object
    private void OnMouseDown()
    {
        if (playerMovement.CanTake)
        {
            selected = true; 
            playerMovement.TakeItem(gameObject); // take item
        }
    }
    // when object enters trigger of "Basket" and it is needed in task 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Basket") && name == manager.NeededProduct)
        {
           StartCoroutine(manager.ReduceProducts());
        }
    }
    // when object leaves trigger of "Conveyor" we return it to pool 
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Conveyor")&&transform.parent.name=="Conveyor") objectPool.ReturnToPool(gameObject);
    }

}
