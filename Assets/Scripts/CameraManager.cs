using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject FondoUI;


    
    public void EnableBackground()
    {
        if (FondoUI.activeInHierarchy==true)
        {
            FondoUI.SetActive(false);
        }
        else
        {
            FondoUI.SetActive(true);
        }
    }

   
    
}
