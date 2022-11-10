using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinimapStreet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider Zm;
    [SerializeField] private TMP_Text ZoomValue;
    //[SerializeField] private TMP_Text PRUEBA;
    public Mapbox.Unity.Map.AbstractMap StreetMap;
    private string zoom;
    void Start()
    {
        Zm = GetComponent<Slider>();
        Zm.onValueChanged.AddListener((v =>
                {
                  
                    ZoomValue.text = v.ToString("0.00");
                    
                   // v = StreetMap.Zoom;
                   // StreetMap.SetZoom(v);
                   // StreetMap.UpdateMap();
                }
                ));
        // Zm.onValueChanged.AddListener((v =>
        //         {
        //             float change=0;
        //             zoom = StreetMap.Zoom;
        //             ZoomValue.text = change.ToString();
        //             change = zoom;
        //         }
        //     ));

        
    }

    // Update is called once per frame
    void Update()
    {
       // zoom = StreetMap.Zoom;
       // Zm.value = zoom;
    }
    public void ZoomMap()
    {

        zoom = ZoomValue.text;
        float z= float.Parse(zoom, CultureInfo.InvariantCulture.NumberFormat);

        //PRUEBA.text="Slider : "+Zm.value.ToString() + "-- ZoomValue.text : "+zoom;
        if (z!=StreetMap.Zoom)
        {
            StreetMap.SetZoom(z);
            StreetMap.UpdateMap();
        }
    }

    

 
}
