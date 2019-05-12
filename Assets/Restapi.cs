using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Boomlagoon.JSON;



#region structures
/// <summary>
/// 
/// </summary>
/// 
[System.Serializable]
public class Child
{
    public string id { get; set; }
}

[System.Serializable]
public class Tag
{
    public string id { get; set; }
    public string type { get; set; }
    public float width { get; set; }
    public float length { get; set; }
    public float height { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float angle { get; set; }
    public string tagType { get; set; }
    public List<Child> children { get; set; }
}

[System.Serializable]
public class SensorData
{
    public string id;
    public float x;
    public float y;
    public float z;
    public float angle;
}

[System.Serializable]
public class RootObject
{

    public List<Tag> tags { get; set; }
}
#endregion

public class Restapi: MonoBehaviour
{
    #region variables
    private string textFromWWW;
    private string url = "http://192.168.0.100/rest/tags"; // URL for the Tag data
    
    public static SensorData[] TagSensorData = new SensorData[24];

    public List<string> id = new List<string>();
    public List<float> x = new List<float>();
    public List<float> y = new List<float>();
    public List<float> z = new List<float>();
    int NumofTag = 0;
    #endregion
    public static float dynx;
    public static float dyny;
    public static float dynz;
    public static string ID;
    /// <summary>
    /// Start fucntion to call sub routine
    /// </summary>
    void Start()
    {
        

    }

    private void Update()
    {
        

        List<string> dataid=new List<string>();
        List<float> datax = new List<float>();
        List<float> datay = new List<float>();
        List<float> dataz = new List<float>();
       

        StartCoroutine(GetTextFromWWW());

        if (id.Count >= 1)
        {
            Get_AllTagId(ref dataid, ref datax, ref datay, ref dataz);
            

            for(int tagnum = 0; tagnum < NumofTag; tagnum++)
            {
                if (string.Compare(Restapi.ID, dataid[tagnum]) == 0)
                {
                    dynx = datax[tagnum];
                    dyny = datay[tagnum];
                    dynz = dataz[tagnum];

                }
                Debug.Log(NumofTag.ToString() + "   " + dataid[tagnum] + datax[tagnum].ToString() + datay[tagnum].ToString() + dataz[tagnum].ToString());
             
            }
            
        }

    }

    /// <summary>
    /// Displays Data in GUI. """" Function only for testing"""
    /// </summary>
    void OnGUI()
    {

    }

    /// <summary>
    /// This function receives data from All the sensors through an API 
    /// </summary>
    /// <returns>All Sesnor data</returns>
    IEnumerator GetTextFromWWW()
    {
        WWW www = new WWW(url);
        Tag TempTag = new Tag();
        yield return www;

        if (www.error != null)
        {
            Debug.Log("Ooops, something went wrong...");
        }
        else
        {
            
            textFromWWW = www.text;

        }

        JSONObject json = JSONObject.Parse(textFromWWW);
        JSONArray TagsArr = json.GetArray("tags");

        id.RemoveRange(0, NumofTag);
        x.RemoveRange(0, NumofTag);
        y.RemoveRange(0, NumofTag);
        z.RemoveRange(0, NumofTag);


        foreach (JSONValue tag in TagsArr)
        {
            JSONObject TagInfo = JSONObject.Parse(tag.ToString());
            id.Add(TagInfo.GetValue("id").ToString());
            x.Add(float.Parse(TagInfo.GetValue("x").ToString()));
            y.Add(float.Parse(TagInfo.GetValue("y").ToString()));
            z.Add(float.Parse(TagInfo.GetValue("z").ToString()));
        }

        NumofTag = id.Count();

    }

    /// <summary>
    /// Returns All the following Sensor Data in a Structure
    /// 1. Id of all tags
    /// 2. X position
    /// 3. Y position
    /// 4. Z Postion
    /// 5. Angle of Tag
    /// </summary>
    /// <returns>TagSensorData - contains all sensor data</returns>
    public SensorData[] GetAllSensData()
    {
        return TagSensorData;
    }

    /// <summary>
    /// Provides all the Sensor data individually to the called functions 
    /// calling method : Get_AllTagId(id, x, y, z, ang)
    /// </summary>
    /// <param name="id">Tag id</param>
    /// <param name="x">all X location</param>
    /// <param name="y">all Y location</param>
    /// <param name="z">all Z location</param>
    /// <param name="ang">all Angle of tag</param>
    public void Get_AllTagId(ref List<string> id_out, ref List<float> x_out, ref List<float> y_out, ref List<float> z_out)
    {
        #region initialization
        //id_out = null;
        //x_out = null;
        //y_out = null;
        //z_out = null;
        #endregion
        if (id.Count >= 1)
        { 
            for (int temp = 0; temp < NumofTag; temp++)
            {
                id_out.Add(id[temp]);
                x_out.Add(x[temp]);
                y_out.Add(y[temp]);
                z_out.Add(z[temp]);
            }
        }
    }
}