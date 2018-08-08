using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour {
    
    public GameObject objectToFocus;       //Public variable to store a reference to the game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private bool isFocusOn = false; //Private variable to store boolean. When true, camera will focus on object
    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = new Vector3(0,1f,1.5f);
    }

    // LateUpdate is called after Update each frame
    void Update()
    {
        if (isFocusOn && objectToFocus != null)
        {
            transform.position = objectToFocus.transform.position + offset;
            transform.LookAt(objectToFocus.transform);
        }
    }


    // Call function when UIManager (raycast) clicks on a planet
    public void SetFocusObject(GameObject POIObject)
    {
        isFocusOn = true;
        objectToFocus = POIObject;
    }

}
