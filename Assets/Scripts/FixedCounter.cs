using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedCounter : MonoBehaviour
{
    public Transform fixedCounter;
    public static FixedCounter instance { get; private set; }

    private void Awake()
    {
        instance = this;
        GetComponent<Text>().text = RubyController.FixedRobots.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
      

    }

    public void SetFixedRobots(int value)
    {
        GetComponent<Text>().text = RubyController.FixedRobots.ToString();

    }


}
