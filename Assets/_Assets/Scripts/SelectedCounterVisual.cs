using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter clearCounter;
    [SerializeField] GameObject[] visualGameObjectArray;
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterEventArgs e)
    {
        if(e.selected == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        foreach(GameObject virtualItem in visualGameObjectArray){
            virtualItem.SetActive(true);
        }
    }

    void Hide()
    {
        foreach (GameObject virtualItem in visualGameObjectArray)
        {
            virtualItem.SetActive(false);
        }
        
    }

}
