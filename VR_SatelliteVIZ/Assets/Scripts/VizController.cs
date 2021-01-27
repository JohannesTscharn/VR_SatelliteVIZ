using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VizController : MonoBehaviour
{
    // VARIABLES
    // ----------------------------------------------------------------------------------
    public List<SatteliteID> allSatellites = new List<SatteliteID>();
    public List<SatteliteID> currentYearSatellites = new List<SatteliteID>();

    public int currentYear = 1957;

    public float SliderValue = 0.0f;


    public Text UIText;
    public Button NextBtn;
    public Button PrevBtn;
    public Button YearBtn;
    public Slider YearSlider;

    private bool playing;
    private bool reversing;
    private bool YearHighligted;

    public Material MaterialPopUp;
    public Material MaterialNormal;
    

    // ----------------------------------------------------------------------------------
    void Start()
    {
        YearSlider.value = (float)currentYear;
    }

    void Update()
    {

        SolveUI();
        
    }

    // ----------------------------------------------------------------------------------   

    // ----------------------------------------------------------------------------------  
    // ----------------------------------------------------------------------------------   
    public void TogglePlay()
    {
        if (!playing && currentYear != 2020)
        {
            Stop();
            Play();
            playing = true;
        }
        else
        {
            Stop();
            playing = false;
        }
    }
    public void ToggleReverse()
    {
        if (!reversing && currentYear != 1957)
        {
            Stop();
            Reverse();
            reversing = true;
        }
        else
        {
            Stop();
            reversing = false;
        }
    }

    public void ToggleYearColor()
    {
        if (!YearHighligted)
        {
            YearHighligted = true;
            foreach (SatteliteID Sat in currentYearSatellites)
                Sat.gameObject.GetComponent<Renderer>().material = MaterialPopUp;
        }
        else
        {
            YearHighligted = false;
            foreach (SatteliteID Sat in currentYearSatellites)
                Sat.gameObject.GetComponent<Renderer>().material = MaterialNormal;
        }


        
    }
    // ----------------------------------------------------------------------------------   

    private void Play()
    {
        Stop();
        StartCoroutine(IncrementYears());
        
    }

    private void Reverse()
    {
        Stop();
        StartCoroutine(DecrementYears());
    }

    private void Stop()
    {
        StopAllCoroutines();
        reversing = false;
        playing = false;
        foreach (SatteliteID Sat in allSatellites)
        {
            Sat.gameObject.transform.localScale = new Vector3(13f, 13, 13f);
            Sat.gameObject.GetComponent<Renderer>().material = MaterialNormal;
        }
        
    }

    // ----------------------------------------------------------------------------------  
    // ----------------------------------------------------------------------------------  

    IEnumerator IncrementYears()
    {
        while (currentYear < 2020)
        {
            currentYear++;
            UIText.text = currentYear.ToString();
            YearSlider.value = (float)currentYear;
            yield return new WaitForSeconds(0.75f);
        }
        if (currentYear == 2020)
        {
            Stop();
        }
    }

    IEnumerator DecrementYears()
    {
        while (currentYear > 1957)
        {
            currentYear--;
            UIText.text = currentYear.ToString();
            YearSlider.value = (float)currentYear;
            yield return new WaitForSeconds(0.75f);
        }
        if (currentYear == 1957)
        {
            Stop();
        }
    }

    // ----------------------------------------------------------------------------------  
    // ----------------------------------------------------------------------------------  




    // SLIDER
    // ----------------------------------------------------------------------------------

    public void NewSliderValue(float SliderVal)
    {
        currentYear = (int)SliderVal;

        SelectYear(currentYear);
        UIText.text = currentYear.ToString();
    }


    // ----------------------------------------------------------------------------------  
    public void SelectYear(int year)
    {
        currentYearSatellites.Clear();
        foreach (SatteliteID Sat in allSatellites)
        {

            Sat.gameObject.transform.localScale = new Vector3(13f, 13, 13f);
            Sat.gameObject.GetComponent<Renderer>().material = MaterialNormal;

            if (Sat.launchYear <= year)
            {
                Sat.gameObject.SetActive(true);
                if (Sat.launchYear == year)
                {
                    currentYearSatellites.Add(Sat);
                    if (YearHighligted)
                        Sat.gameObject.GetComponent<Renderer>().material = MaterialPopUp;
                }
            }
            else
                Sat.gameObject.SetActive(false);
        }


        if (playing || reversing)
            StartCoroutine(ScaleSatellites(0.6f));

    }
    // ----------------------------------------------------------------------------------  




    IEnumerator ScaleSatellites(float delay)
    {
        foreach (SatteliteID Sat in currentYearSatellites)
        {
            Sat.gameObject.transform.localScale = new Vector3(25f, 25, 25f);
            Sat.gameObject.GetComponent<Renderer>().material = MaterialPopUp;
        }
        yield return new WaitForSeconds(delay);
        
        foreach (SatteliteID Sat in currentYearSatellites)
        {
            Sat.gameObject.transform.localScale = new Vector3(13f, 13, 13f);
            Sat.gameObject.GetComponent<Renderer>().material = MaterialNormal;
        }
    }

    // ----------------------------------------------------------------------------------

        
    private void SolveUI()
    {
        Color Grey = new Color(255f, 255f, 255f, 0.1f);
        Color Grey2 = new Color(255f, 255f, 255f, 0.0f);
        Color White = new Color(255.0f, 255.0f, 255.0f, 1f);
        Color Red = new Color(255.0f, 0.0f, 0.0f, 1f);
        Color Red2 = new Color(255.0f, 0.0f, 0.0f, 0.2f);

        if (!YearHighligted)
        {
            UIText.GetComponent<Text>().color = White;
            YearBtn.transform.localPosition = new Vector3(YearBtn.transform.localPosition.x, YearBtn.transform.localPosition.y, -2.5f);
        }
        else if (YearHighligted)
        {
            UIText.GetComponent<Text>().color = Red;
            YearBtn.transform.localPosition = new Vector3(YearBtn.transform.localPosition.x, YearBtn.transform.localPosition.y, -1.5f);
        }


        if (playing)
        {
            NextBtn.transform.localPosition = new Vector3(NextBtn.transform.localPosition.x, NextBtn.transform.localPosition.y, -1.5f);
            NextBtn.GetComponent<Image>().color = Red;
        }

        if (currentYear == 2020)
        {
            NextBtn.transform.localPosition = new Vector3(NextBtn.transform.localPosition.x, NextBtn.transform.localPosition.y, -1.5f);
            NextBtn.GetComponent<Image>().color = Grey;
        }
        else if (!playing)
        {
            NextBtn.transform.localPosition = new Vector3(NextBtn.transform.localPosition.x, NextBtn.transform.localPosition.y, -3.25f);
            NextBtn.GetComponent<Image>().color = White;
        }

        if (reversing)
        {
            PrevBtn.transform.localPosition = new Vector3(PrevBtn.transform.localPosition.x, PrevBtn.transform.localPosition.y, -1.5f);
            PrevBtn.GetComponent<Image>().color = Red;
        }

        if (currentYear == 1957)
        {
            PrevBtn.transform.localPosition = new Vector3(PrevBtn.transform.localPosition.x, PrevBtn.transform.localPosition.y, -1.5f);
            PrevBtn.GetComponent<Image>().color = Grey;
        }
        else if (!reversing)
        {
            PrevBtn.transform.localPosition = new Vector3(PrevBtn.transform.localPosition.x, PrevBtn.transform.localPosition.y, -3.25f);
            PrevBtn.GetComponent<Image>().color = White;
        }
    }
    
}
