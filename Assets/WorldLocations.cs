using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WorldLocations : MonoBehaviour
{
    public enum State { INACCESSIBLE, In_Progress, CLEARED_BY_REPUTATION, CLEARED_BY_INFLUENCE };
    public State WorldState = State.INACCESSIBLE;
    public string Name;
    public TextMeshProUGUI AnnouncementText;
    AudioSource sound;
    public AudioClip clip1;

    // Start is called before the first frame update
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
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
        sound.PlayOneShot(clip1);
        AnnouncementText.text = "COUNTRY: " + Name+ " -- " + "STATE: "+ WorldState.ToString().ToUpper();
    }
    public void TaskOnExitHover()
    {
        AnnouncementText.text = "";
    }
}
