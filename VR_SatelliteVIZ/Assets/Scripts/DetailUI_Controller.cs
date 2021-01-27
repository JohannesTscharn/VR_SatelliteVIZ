using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class DetailUI_Controller : MonoBehaviour
{
    public GameObject Canvas;
    public Text UIText;
    public Text UIxPos;
    public Text UIyPos;
    public Text UIzPos;
    public XRRayInteractor RayInteractor;

    private RaycastHit RayHit;

    public Material HighlightMat;
    public Material NormalMat;

    private List<GameObject> ActiParticles = new List<GameObject>();
    

    public void ShowCanvas()
    {
        RayInteractor.GetCurrentRaycastHit(out RayHit);

        // Add to List of ActivatedParticles (In case of double activation)
        ActiParticles.Add(RayHit.transform.gameObject);
        
            Canvas.transform.position = RayHit.transform.position;
            Canvas.SetActive(true);

            UIText.text = RayHit.transform.gameObject.GetComponent<SatteliteID>().launchDate;
            UIxPos.text = RayHit.transform.gameObject.GetComponent<SatteliteID>().xPos.ToString();
            UIyPos.text = RayHit.transform.gameObject.GetComponent<SatteliteID>().yPos.ToString();
            UIzPos.text = RayHit.transform.gameObject.GetComponent<SatteliteID>().zPos.ToString();

        RayHit.transform.gameObject.GetComponent<Renderer>().material = HighlightMat;
            RayHit.transform.gameObject.transform.localScale = new Vector3(20f, 20f, 20f);
    }


    public void HideCanvas()
    {
        Canvas.SetActive(false);

        foreach (GameObject Particle in ActiParticles)
        {
            RayHit.transform.gameObject.GetComponent<Renderer>().material = NormalMat;
            RayHit.transform.gameObject.transform.localScale = new Vector3(13f, 13f, 13f);
        }
        ActiParticles.Clear();
    }


    // ----------------------------------------------------------------------------------------------
    // HELPER FUNCTIONS
    // ----------------------------------------------------------------------------------------------



    // ----------------------------------------------------------------------------------------------

}
