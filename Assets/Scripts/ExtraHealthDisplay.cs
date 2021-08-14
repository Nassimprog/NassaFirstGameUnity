using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraHealthDisplay : MonoBehaviour
{

    public Transform extraHealthDisplay;
    public static ExtraHealthDisplay instance { get; private set; }


    private void Awake()
    {
        instance = this;
        GetComponent<Text>().text = RubyController.ExtraHealth.ToString();
       

    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {


    }


    public void SetExtraHealth(int value)
    {
        GetComponent<Text>().text = RubyController.ExtraHealth.ToString();
    }

}
