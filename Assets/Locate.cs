using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locate : MonoBehaviour
{
    public GameObject marker;
    public static int dynamic = 0;


    public static Vector3 myInitialLocation;
    public Vector3 Room1;
    public Vector3 Exit;
    public Vector3 Car;
    public Vector3 FArea;
    public Vector3 ladder;
    public Vector3 ID1022;
    public Vector3 ID102B;
    public static Vector3 trackLocation;
    // Start is called before the first frame update
    void Start()
    {
        marker = GameObject.Find("Arrow");

        //Static points in the indoor area
        //myInitialLocation = new Vector3((float)16.98, (float)22.45, (float)0.0517);

        /* Static Test values from tags */
        myInitialLocation = new Vector3((float)21.77, (float)26.48, (float)0.35);
        Room1 = new Vector3((float)26.7, (float)26.99, (float)3.2);
        Exit = new Vector3((float)28.7, (float)14.54, (float)3.2);
        FArea = new Vector3((float)14.2, (float)30.20, (float)3.0);
        ladder = new Vector3((float)14.01, (float)8.54, (float)3.0);
        Car = new Vector3((float)12.01, (float)16.54, (float)3.0);
        
        ID1022 = new Vector3((float)17.34, (float)22.88, (float)0.42);
    }

    // Update is called once per frame
    void Update()
    {
        startTracking();
        //To track Dynamic objects
        if (dynamic == 1)
        {
            trackLocation.x = Restapi.dynx;
            trackLocation.y = Restapi.dyny;
            trackLocation.z = Restapi.dynz;
            trackLocation = trackLocation - myInitialLocation;
            trackLocation = Transform2Unity(trackLocation);
            Debug.Log(trackLocation.ToString() + "dynamic" + dynamic.ToString());
           
        }
        else
        { 
            Debug.Log(trackLocation.ToString() + "static" + dynamic.ToString());
       }
        marker.transform.localPosition = trackLocation;

    }

    public Vector3 Transform2Unity(Vector3 position)
    {
        Vector3 newposition = new Vector3();
        newposition.x = position.y;
        newposition.y = position.z;
        newposition.z = position.x;
        return newposition;
    }

    public void startTracking()
    {
        Locate.dynamic = 0;
        if (ViewModel.selected == 1)
        {
            trackLocation = Room1 - myInitialLocation;
            trackLocation = Transform2Unity(trackLocation);
            marker.transform.localPosition = trackLocation;
        }
        else if (ViewModel.selected == 2)
        {
            trackLocation = Car - myInitialLocation;
            trackLocation = Transform2Unity(trackLocation);
            marker.transform.localPosition = trackLocation;
        }
        else if (ViewModel.selected == 3)
        {
            trackLocation = Exit - myInitialLocation;
            trackLocation = Transform2Unity(trackLocation);
            marker.transform.localPosition = trackLocation;
        }
        else if (ViewModel.selected == 4)
        {
            trackLocation = FArea - myInitialLocation;
            trackLocation = Transform2Unity(trackLocation);
            marker.transform.localPosition = trackLocation;
        }
        else if (ViewModel.selected == 5)
        {
            trackLocation = ladder - myInitialLocation;
            trackLocation = Transform2Unity(trackLocation);
            marker.transform.localPosition = trackLocation;
        }
        else if (ViewModel.selected == 6) //Sample Dynamic ID tracking,
        {
            Restapi.ID = "012B";
            // setting the Id here fixes hologram to the tag
        }
        else
        {
            Debug.Log("Fail!!!");
        }
      /*  else if (ViewModel.selected == 4)
        {
            dynamic = 1;
        }*/
        
    }
}
