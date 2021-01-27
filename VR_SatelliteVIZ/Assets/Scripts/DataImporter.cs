using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataImporter : MonoBehaviour
{
    // VARIABLES
    // ----------------------------------------------------------------------------------

    public VizController VizController;


    // Read Path
    string filePath;

    // Coordinates
    public List<string> Coordinates = new List<string>();
    float xPos, yPos, zPos;
    string launchDate;
    int launchYear;
    string numberBuffer;

    // GameObject References
    public GameObject ParticleContainer;
    public GameObject ParticleMesh;



    // ----------------------------------------------------------------------------------   



    // MAIN
    // ----------------------------------------------------------------------------------  

    void Start()
    {
        filePath = Application.dataPath + "/ImportData/ImportData_03.csv";

        StartCoroutine(LoadSatellites());

    } // End Start

    // ----------------------------------------------------------------------------------   




    // FUNCTIONS
    // ----------------------------------------------------------------------------------

    // FileReader stores .csv Content in Coordinates List
    void readFile(string filePath)
    {
        Coordinates = new List<string>();
        StreamReader streamReader = new StreamReader(filePath);

        // While Loop (while .csv not fully processed)
        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            Coordinates.Add(line);
        }
        streamReader.Close();


        Debug.Log("Number of Objects: " + Coordinates.Count);
    } // End ReadFile Function


    // Coordinate Mapper Function
    void CreateSatellites()
    {
        for (int i = 0; i < Coordinates.Count; i++)
        {

            string[] positionArray = Coordinates[i].Split(',');

            // xyz are ordered correctly
            zPos = (float.Parse(positionArray[1])) / 1000;
            xPos = (float.Parse(positionArray[2])) / 1000;
            yPos = (float.Parse(positionArray[3])) / 1000;

            // Extract the Year it was Launched
            // numberBuffer = Coordinates[i].Substring(1, 4);
            launchYear = (int.Parse(Coordinates[i].Substring(1, 4)));
            launchDate = positionArray[0];


            // Instantiated Object 
            // ----------------------------------------------------------------------------------
            GameObject Sattelite = Instantiate(ParticleMesh, new Vector3(xPos, yPos, zPos), Quaternion.identity, ParticleContainer.transform);
            
            SatteliteID SatIDcomp = Sattelite.GetComponent<SatteliteID>();
            SatIDcomp.launchYear = launchYear;
            // DAY AND MONTH IS FAKE (Problem im getting it from SatCoord...
            SatIDcomp.launchDate = ((int)Random.Range(1, 32)).ToString() + "." + ((int)Random.Range(1, 13)).ToString() + "." + launchDate;
            SatIDcomp.xPos = xPos;
            SatIDcomp.yPos = yPos;
            SatIDcomp.zPos = zPos;


            VizController.allSatellites.Add(SatIDcomp);
            Sattelite.SetActive(false);
            // ----------------------------------------------------------------------------------




        }
    } // End Coordinate Mapper


    // Master Coroutine
    IEnumerator LoadSatellites()
    {
        readFile(filePath);
        yield return new WaitForSeconds(0.25f);
        CreateSatellites();
    } // End Coroutine

    // ----------------------------------------------------------------------------------



} // End Class
