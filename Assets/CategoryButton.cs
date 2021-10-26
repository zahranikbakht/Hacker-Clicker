using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    Animator anim;
    public Animator OtherAnim1;
    public Animator OtherAnim2;
    public Animator OtherAnim3;
    public GameObject SelfMenu;
    public GameObject OtherMenu1;
    public GameObject OtherMenu2;
    public GameObject OtherMenu3;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        if (this.gameObject.name.Equals("Malware"))
        {
            anim.SetBool("CategorySelected", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
        OtherAnim1.SetBool("CategorySelected", false);
        OtherAnim2.SetBool("CategorySelected", false);
        OtherAnim3.SetBool("CategorySelected",false); 
        anim.SetBool("CategorySelected", true);
        SelfMenu.SetActive(true);
        OtherMenu1.SetActive(false);
        OtherMenu2.SetActive(false);
        OtherMenu3.SetActive(false);
    }
}
