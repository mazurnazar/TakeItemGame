using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField] GameObject pool;
    [SerializeField] GameObject conveyor;
    [SerializeField] private ObjectPool[] pools;
    [SerializeField] private Manager manager;

    private Vector3 initPos;
    private bool create = true;
    private float convoyerSpeed = 0.1f;

    private void Start()
    {
        manager.GameOver += Deactivate; // subscribe to GameOver event
        initPos = new Vector3(-15, 20, 0); // initialize initial position
        pools = new ObjectPool[pool.GetComponentsInChildren<ObjectPool>().Length];
        pools = pool.GetComponentsInChildren<ObjectPool>();
        StartCoroutine(ShowProduct());

    }
    // instantiate product
    IEnumerator ShowProduct()
    {
        if (create)
        {
            // take product from random pool
            int productNumber = Random.Range(0, pools.Length); 
            GameObject product = pools[productNumber].GetPooledObject();

            // assign initial position to product position 
            product.transform.position = initPos;
            product.transform.parent = conveyor.transform;

            initPos.z = Random.Range(-0.5f, 0.5f); // z position in range of convoyer
            product.transform.localPosition = new Vector3(product.transform.localPosition.x, product.transform.localPosition.y, initPos.z);
            yield return new WaitForSeconds(1f);
            StartCoroutine(ShowProduct());
        }
    }
    // "moving" texture of convoyer 
    void MoveConvoyer()
    {
        conveyor.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(Time.realtimeSinceStartup * convoyerSpeed, 0);
    }
    private void Update()
    {
        MoveConvoyer();
    }
    // deactivate creating products
    void Deactivate()
    {
        create = false;
        convoyerSpeed = 0;
        StopCoroutine(ShowProduct());
        conveyor.SetActive(false);
    }
}
