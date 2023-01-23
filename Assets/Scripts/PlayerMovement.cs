using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMovement : MonoBehaviour
{
    private bool cantake = true; // if player can take item
    public bool CanTake { get => cantake; private set { } }

    [SerializeField] private Transform handIKTarget; // target to take
    [SerializeField] private Transform rightHand, leftHand; 
    [SerializeField] private Transform basket;
    [SerializeField] private Manager manager;

    private Vector3 offset = new Vector3(0, 2, 0);
    private Animator animator;
    private GameObject item;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        manager.GameOver += Dance; // subscribe to GameOver event
    }
    //take item from convoyer and start animation of grabbing it
    public void TakeItem(GameObject item)
    {
        handIKTarget.position = item.transform.position + offset;
        animator.SetTrigger("GrabItem");
        this.item = item;
        cantake = false;
    }
    // set parent and final position in hand
    public void TakeIntoHand()
    {
        item.transform.position = rightHand.transform.position-offset;
        item.transform.parent = rightHand.transform;        
    }
    // release from hand to basket
    public void Release()
    {
        item.transform.parent = basket;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        cantake = true;
    }
    // play Dance animation
    public void Dance()
    {
        if (manager.QuantityProducts < 1)
        {
            basket.gameObject.SetActive(false);
            leftHand.GetComponent<TwoBoneIKConstraint>().weight = 0;
            animator.SetTrigger("Dance");
        }
    }
}

