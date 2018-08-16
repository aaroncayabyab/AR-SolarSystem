using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTrackerManager : DefaultTrackableEventHandler {

    #region UI_VARIABLES
    public GameObject planetNameDisplay;
    public GameObject exitZoomButton;
    public GameObject imageSearcher;
    #endregion

	// Use this for initialization
    protected override void Start () {
        base.Start();
        //Set up UI
        planetNameDisplay.SetActive(false);
        exitZoomButton.SetActive(false);
        imageSearcher.SetActive(true);
	}
	
    protected override void OnTrackingFound() {
        base.OnTrackingFound();
        //Setup UI
        imageSearcher.SetActive(false);
    }
}
