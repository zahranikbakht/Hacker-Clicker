using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public TextMeshProUGUI AnnouncementText;
    AudioSource sound;
    public AudioClip clip1;
    // Start is called before the first frame update
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TaskOnClick()
    {
        SaveMaster.SaveInstance.Save();
    #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif
        
    }
    public void TaskOnHover()
    {
        sound.PlayOneShot(clip1);
        AnnouncementText.text = "Save and quit Game.";
    }
    public void OnHoverExit()
    {
        AnnouncementText.text = "";
    }
}
