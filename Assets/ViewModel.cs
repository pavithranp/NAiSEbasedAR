using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ViewModel : MonoBehaviour
{

    public static float rotation;
    public Canvas startcanvas;
    public static int selected;
    
    public Dropdown drop2;
    public int DropdownIndex2;
    public dropdownController dropdown;


    public List<Dropdown.OptionData> menuOptions2 = new List<Dropdown.OptionData>();
    private void Start()
    {
        startcanvas = GameObject.Find("startcanvas").GetComponent<Canvas>();
        drop2 = GameObject.Find("Dropdown2").GetComponent<Dropdown>();
    }

   

    public void Button_Click()
    {
        Debug.Log("Button Clicked");
        //rotation = SampleMovement.m_lastMagneticHeading;
        // Unfortunately the magnetometer was not working properly , due to presence of heavy magnets in demo area
        //startcanvas.gameObject.SetActive(false);


        Debug.Log(dropdownController.m_Message);
        selected = dropdownController.m_DropdownValue;
        //startTracking();
    }
    public Vector3 Transform2Unity(Vector3 position)
    {
        Vector3 newposition = new Vector3();
        newposition.x = position.y;
        newposition.y = position.z;
        newposition.z = position.x;
        return newposition;
    }

    

}
