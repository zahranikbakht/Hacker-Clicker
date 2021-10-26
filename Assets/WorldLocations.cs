using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WorldLocations : MonoBehaviour
{
    public enum State { INACCESSIBLE, InProgress, CLEARED };
    public State WorldState = State.INACCESSIBLE;
    public string Name;
    public TextMeshProUGUI AnnouncementText;


    // Start is called before the first frame update
    void Start()
    {
        Name = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
    
    }

    public void TaskOnHover()
    {
        AnnouncementText.text = "COUNTRY: " + Name+ " -- " + "STATE: "+ WorldState.ToString().ToUpper();
    }
    public void TaskOnExitHover()
    {
        AnnouncementText.text = "";
    }
}
