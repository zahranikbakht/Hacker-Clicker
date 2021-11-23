using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public TextMeshProUGUI AnnouncementText;
    AudioSource sound;
    public AudioClip clip1;
    // Start is called before the first frame update
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
    }

    public void TaskOnClick()
    {
        //MenuSelector.SetBool("closed", false);
        //MenuSelector.Play("Open");
        sound.PlayOneShot(clip1);
        //GameManagerScript.InWindow = true;
        SaveMaster.SaveInstance.Save();
        AnnouncementText.text = "Game Saved!";
    }
    public void TaskOnHover()
    {
        sound.PlayOneShot(clip1);
        AnnouncementText.text = "Save your progress.";
    }
    public void OnHoverExit()
    {
        AnnouncementText.text = "";
    }
}
