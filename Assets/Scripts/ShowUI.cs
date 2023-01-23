using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private Text taskText;
    [SerializeField] private Manager manager;
    [SerializeField] private Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        ShowTask();
        // subscribing to ChangeNumber and GameOver events
        manager.ChangeNumber += ShowTask; 
        manager.GameOver += GameOver;
    }
    // show in text current task
    void ShowTask()
    {
            taskText.text = "Collect " + manager.QuantityProducts + " " + manager.NeededProduct;
            taskText.gameObject.SetActive(true);
    }
    public void GameOver()
    {
        // change text to 
        taskText.text = "Level passed"; 
    }
    public void Restart()
    {
        restartButton.gameObject.SetActive(true); // activate restart button
    }
    
   
}
