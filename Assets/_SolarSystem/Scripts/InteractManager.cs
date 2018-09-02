using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour {

    public GameObject solarSystem;
    public Camera arCamera;
    public Camera focusCamera;

    //Planets array to enable/disable when zooming into planets
    public GameObject[] planets;
    //OrigScale and ZoomScale variables when zooming into planets
    Vector3 originalScale;
    Vector3 zoomScale = new Vector3(1f, 1f, 1f);
    GameObject hitObject;
    //UI variables
    public GameObject planetNameDisplay;
    public GameObject planetNameText;
    public GameObject exitZoomButton;

	// Use this for initialization
	void Start () {
        focusCamera.enabled = false;

        exitZoomButton.SetActive(false);
        planetNameDisplay.SetActive(false);

        planets = GameObject.FindGameObjectsWithTag("HeavenlyBody");

	}

    // Update is called once per frame
    // Will only detect gameobjects in HeavenlyBody layer, as assigned for Solar System.
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int solarSystemLayerMask = LayerMask.GetMask("HeavenlyBody");
            RaycastHit hit;

            //If planet clicked/touched, zoom into planet and display information
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, solarSystemLayerMask))
            {
                Debug.Log(hit.collider.gameObject.name);

                //Save original scale value
                hitObject = hit.collider.gameObject;
                originalScale = hitObject.transform.localScale;

                ZoomToObject(hitObject);
                planetNameText.GetComponent<Text>().text = hitObject.name;;
            }

        }
    }

    //When planet is clicked/taooed, will disable other planets in scene
    //When clicked, will set scale of planet to x=1,y=1,z=1, to offset properly
    //When clicked, camera will switch from Vuforia/AR Camera to FocusCamera that sets an offset and LookAt() function
    void ZoomToObject(GameObject go)
    {
        foreach(var planet in planets)
        {
            if (planet.name != go.name)
                planet.SetActive(false);
        }

        go.transform.localScale = zoomScale;

        focusCamera.enabled = true;
        arCamera.enabled = false;

        exitZoomButton.SetActive(true);
        planetNameDisplay.SetActive(true);

        focusCamera.GetComponent<FocusCamera>().SetFocusObject(go);
    }

    //When 'back' button is clicked/tapped, planets will become visible again
    //Planet that is being focused will scale back to original size from zoomScale.
    //Camera will switch back from FocusCamera to ARCamera
    public void ExitZoom()
    {
        foreach (var planet in planets)
        {
            planet.SetActive(true);
        }

        hitObject.transform.localScale = originalScale;

        arCamera.enabled = true; 
        focusCamera.enabled = false;

        exitZoomButton.SetActive(false);
        planetNameDisplay.SetActive(false);
               
    }



}
