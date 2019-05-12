
using System;
using UnityEngine;
using UnityEngine.UI; //required for Input.compass


public class SampleMovement : MonoBehaviour
{
    public float compassSmooth = 0.5f;
    public static float m_lastMagneticHeading = 0f;
    public Text degree;
    // Use this for initialization
    void Start()
    {
        // If you need an accurate heading to true north, 
        // start the location service so Unity can correct for local deviations:
        Input.location.Start();
        // Start the compass.
        Input.compass.enabled = true;
        degree = GameObject.Find("Degree").GetComponent<Text>();
    }
    // Update is called once per frame
    private void Update()
    {
        //do rotation based on compass
        float currentMagneticHeading = (float)Math.Round(Input.compass.magneticHeading, 2);
        if (m_lastMagneticHeading < currentMagneticHeading - compassSmooth || m_lastMagneticHeading > currentMagneticHeading + compassSmooth)
        {
            m_lastMagneticHeading = currentMagneticHeading;
            degree.text = m_lastMagneticHeading.ToString();
        }
        //Debug.Log("Magnetic head:"+ m_lastMagneticHeading.ToString());
    }
}