using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraHealthDisplay : MonoBehaviour
{

    public GameObject extraHealthDisplay;
    public GameObject ExtraHealthCounter;
    public static ExtraHealthDisplay instance { get; private set; }


    private void Awake()
    {

        instance = this;
        GetComponent<Text>().text = RubyController.ExtraHealth.ToString();
        if (RubyController.HasHealthSatchel)
        {
            extraHealthDisplay.gameObject.SetActive(true);
            ExtraHealthCounter.gameObject.SetActive(true);
            
        }
        
       else
        {
            extraHealthDisplay.gameObject.SetActive(false);
            ExtraHealthCounter.gameObject.SetActive(false);
        }
            
            

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
