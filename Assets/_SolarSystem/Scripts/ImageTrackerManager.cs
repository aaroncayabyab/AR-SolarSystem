using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTrackerManager : DefaultTrackableEventHandler {

    #region UI_VARIABLES
    public GameObject planetNameDisplay;
    public GameObject exitZoomButton;
    public GameObject imageSearcher;
    public GameObject interactInstructions;
    #endregion

    public GameObject interactManager;
    public Camera focusCamera;

    private bool isInstructionDone = false; //Boolean variable that will allow instructions to only appear once

	// Use this for initialization
    protected override void Start () {
        base.Start();
        //Set up UI
        planetNameDisplay.SetActive(false);
        exitZoomButton.SetActive(false);
        interactInstructions.SetActive(false);
        imageSearcher.SetActive(true);
	}
	
    protected override void OnTrackingFound() {
        base.OnTrackingFound();
        //Setup UI
        imageSearcher.SetActive(false);

        //ShowInstruction coroutine will only run once, because isInstructionDone will toggle.
        if(isInstructionDone == false)
        {
            isInstructionDone = true;
            StartCoroutine(ShowInstructions());
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        //Setup UI
        imageSearcher.SetActive(true);
        planetNameDisplay.SetActive(false);
        exitZoomButton.SetActive(false);
        interactInstructions.SetActive(false);

        //If image lost and in inspect planet mode, run same function as ExitZoom from interact manager
        if (focusCamera.enabled == true)
        {
            interactManager.gameObject.GetComponent<InteractManager>().ExitZoom();
        }
    }

    //Show instructions when image is detected
    //Coroutine used to setup functions that run with timers/delays
    //Will disable instructions after 4 seconds
    IEnumerator ShowInstructions()
    {
        
        yield return new WaitForSeconds(0.5f);
        interactInstructions.SetActive(true);
        yield return new WaitForSeconds(4f);
        interactInstructions.SetActive(false);

    }
}
