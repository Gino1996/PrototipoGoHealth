using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{
    public TMP_Text status;

    public TMP_Text latitud;

    public TMP_Text longitud;
    //public bool isUpdating;

    private void Start()
    {
        StartCoroutine(GetLocation());
    }


    // private void Update()
    // {
    //     if (!isUpdating)
    //     {
    //         StartCoroutine(GetLocation());
    //         isUpdating = !isUpdating;
    //     }
    // }

    IEnumerator GetLocation()
    {
        // if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        // {
        //     Permission.RequestUserPermission(Permission.FineLocation);
        //     Permission.RequestUserPermission(Permission.CoarseLocation);
        // }

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(3);

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            status.text = "Tiempo de espera concluido";
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            status.text = "No se puede determinar la localización";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            //gpsOut.text = "Location: " + Input.location.lastData.latitude + " Lat " + Input.location.lastData.longitude + " Long " + Input.location.lastData.altitude+100f + " Alt " + Input.location.lastData.horizontalAccuracy + " Timestamp " + Input.location.lastData.timestamp;
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " +
                  Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " +
                  Input.location.lastData.timestamp);
            InvokeRepeating("UpdateGps", 0.5f, 1f);
        }

        // Stop service if there is no need to query location updates continuously
        // isUpdating = !isUpdating;
        // Input.location.Stop();
    }

    public void UpdateGps()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            status.text = "On";
            //gpsOut.text = "Latitud: " + Input.location.lastData.latitude + " Longitud " + Input.location.lastData.longitude + " Altitud " + Input.location.lastData.altitude+100f + "  " + Input.location.lastData.horizontalAccuracy + " Timestamp " + Input.location.lastData.timestamp;
            //gpsOut.text = "Latitud: " + Input.location.lastData.latitude + "\n Longitud " + Input.location.lastData.longitude + "\n Altitud " + Input.location.lastData.altitude+100f;
            latitud.text = "" + Input.location.lastData.latitude;
            longitud.text = "" + Input.location.lastData.longitude;

        }
        else
        {
            status.text = "Off";
        }

    }

}