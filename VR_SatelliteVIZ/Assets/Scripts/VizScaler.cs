using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VizScaler : MonoBehaviour
{
    [SerializeField]
    public float Scale = 0.1f;


    void Start()
    {
        StartCoroutine(ScaleDelayed());
    }




    IEnumerator ScaleDelayed()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.localScale = new Vector3(Scale, Scale, Scale);

    }

}
