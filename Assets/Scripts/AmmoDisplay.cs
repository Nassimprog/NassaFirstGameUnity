using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{

    public Transform ammoDisplay;
    public static AmmoDisplay instance { get; private set; }


    private void Awake()
    {

        instance = this;
        GetComponent<Text>().text = RubyController.ammo.ToString();
           
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void SetAmmo(int value)
    {
        GetComponent<Text>().text = RubyController.ammo.ToString();
    }



}