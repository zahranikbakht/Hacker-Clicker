using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClose : MonoBehaviour
{
    public Animator MissionSelector;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void TaskOnClick()
    {
        MissionSelector.SetBool("closed", true);
        MissionSelector.Play("Close");
        GameManagerScript.InWindow = false;
    }
}