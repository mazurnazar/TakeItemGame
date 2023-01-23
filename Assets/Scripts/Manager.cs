using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    [SerializeField] private List<GameObject> products; // all available products
    public List<string> productNames; // their names
    [SerializeField] private GameObject player;
    [SerializeField] private float distanceToPlayer = 10f; // camera distance to player when game over
    [SerializeField] Camera cam;


    private int quantityProducts = 0; //number of needed products
    public int QuantityProducts { get => quantityProducts; set => quantityProducts = value; }

    private string neededProduct; // name of needed product
    public string NeededProduct { get => neededProduct; private set { } }

    [SerializeField] private ParticleSystem particles;

    public delegate void ChangeState(); // delegate for changed state in game such as changed number of needed products or game over
    public event ChangeState ChangeNumber; // event when changed number of needed product
    public event ChangeState GameOver; // even when game over
   
    void Awake()
    {
        SetProductNames();
        SetTask();
        
    }
    void SetProductNames()
    {
        foreach (var item in products)
        {
            productNames.Add(item.name); // add product name to list of names
        }
    }
    // set task for the level
    void SetTask()
    {
        quantityProducts = Random.Range(1, 6); // generate random number between 1 and 5 inclusive
        int numberOfProduct = Random.Range(0, productNames.Count); // generate random product of existed 
        neededProduct = productNames[numberOfProduct];

    }
    // reduce number of needed products in task of level
    public IEnumerator ReduceProducts()
    {
        quantityProducts--;
        particles.Play(); // play particles "+1"
        if (quantityProducts >= 1)
            ChangeNumber.Invoke();
        else
        {
            yield return new WaitForSeconds(2f);
            GameOver.Invoke();
            StartCoroutine( MoveCamera());
        }
    }
    //move camera closer to player after win of game
    IEnumerator MoveCamera()
    {
        float i = 0;
        Vector3 camNewPos = Vector3.MoveTowards(cam.transform.position, player.transform.position, distanceToPlayer); // calculate new camera position
        while (i < 1)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camNewPos, i); // lerp to new  position
            i += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    //reload scene
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
