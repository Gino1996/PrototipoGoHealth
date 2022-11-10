using System;
using System.Collections;
using System.Collections.Generic;
using ARLocation;
using ARLocation.MapboxRoutes;
//using ARLocation.MapboxRoutes.SampleProject;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StreetMapManager : MonoBehaviour
{

        public string MapboxToken = "pk.eyJ1IjoiZ2lubzE5OTYiLCJhIjoiY2w3bnlsNm1pMDZlZTNwcXl4Yml5aGczeSJ9.D90gZTZ2ZRD59-6W7WQhOA";
        public GameObject ARSession;
        public GameObject ARSessionOrigin;
    
        public Camera MinimapCamera;

        public Texture RenderTexture;
        public Mapbox.Unity.Map.AbstractMap StreetMap;
        [Range(300, 1000)]
        public int MapSize = 400;
        
        public float MinimapStepSize = 0.5f;
        
        

        void Start()
        {
   
            ARLocationProvider.Instance.OnEnabled.AddListener(onLocationEnabled);
          
        }



        private void onLocationEnabled(Location location)
        {
            StreetMap.SetCenterLatitudeLongitude(new Mapbox.Utils.Vector2d(location.Latitude, location.Longitude));
            // Map.SetZoom(18);
            StreetMap.UpdateMap();
        }

        void OnEnable()
        {
            Debug.Log("Enable!!!!!!!!");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            // ARLocationProvider.Instance.OnEnabled.RemoveListener(onLocationEnabled);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"Scene Loaded: {scene.name}");
        }

        void drawMap()
        {
            var tw = RenderTexture.width;
            var th = RenderTexture.height;

            var scale = MapSize / th;
            var newWidth = scale * tw;
            var x = Screen.width / 2 - newWidth / 2;
            float border;
            if (x < 0)
            {
                border = -x;
            }
            else
            {
                border = 0;
            }


            GUI.DrawTexture(new Rect(x, Screen.height - MapSize, newWidth, MapSize), RenderTexture, ScaleMode.ScaleAndCrop);
            GUI.DrawTexture(new Rect(0, Screen.height - MapSize - 20, Screen.width, 20), separatorTexture, ScaleMode.StretchToFill, false);
            var newZoom = GUI.HorizontalSlider(new Rect(0, Screen.height - 60, Screen.width, 60), StreetMap.Zoom, 10, 22);

            if (newZoom != StreetMap.Zoom)
            {
                StreetMap.SetZoom(newZoom);
                StreetMap.UpdateMap();
            
            }
        }

       
        public void OnGUI()
        {
          
            
            //drawMap();
            
        }

 
       

        private Texture2D _separatorTexture;
        private Texture2D separatorTexture
        {
            get
            {
                if (_separatorTexture == null)
                {
                    _separatorTexture = new Texture2D(1, 1);
                    _separatorTexture.SetPixel(0, 0, new Color(0.15f, 0.15f, 0.15f));
                    _separatorTexture.Apply();
                }

                return _separatorTexture;
            }
        }

      

        Vector3 lastCameraPos;
        void Update()
        {
            var cameraPos = Camera.main.transform.position;

            var arLocationRootAngle = ARLocationManager.Instance.gameObject.transform.localEulerAngles.y;
            var cameraAngle = Camera.main.transform.localEulerAngles.y;
            var mapAngle = cameraAngle - arLocationRootAngle;

            MinimapCamera.transform.eulerAngles = new Vector3(90, mapAngle, 0);

            if ((cameraPos - lastCameraPos).magnitude < MinimapStepSize) {
                return;
            }

            lastCameraPos = cameraPos;

            var location = ARLocationManager.Instance.GetLocationForWorldPosition(Camera.main.transform.position);

            StreetMap.SetCenterLatitudeLongitude(new Mapbox.Utils.Vector2d(location.Latitude, location.Longitude));
            StreetMap.UpdateMap();


        }
}
