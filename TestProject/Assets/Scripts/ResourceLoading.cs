using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceLoading : MonoBehaviour
{

    private Slider slider;
    public Text resourceCounter;
    public Text overallResourceCounter;
    public int resourceNumber;
    public int totalNumber;
    public bool characterCollecting;

    public Transform collectingPoint;

    public float collectingSpeed;
    void Start()
    {
        collectingSpeed = 10f;
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        UpdateValue();
      
    }

    private void UpdateValue()
    {
        if (!CompletedBar())
        {
            if (characterCollecting)
                slider.value += collectingSpeed * Time.deltaTime;
            else
            {
                resourceCounter.text = "x0"; 
            }
        }
        
    }

    private bool CompletedBar()
    {
        if (slider.value == slider.maxValue)
        {
            slider.value = slider.minValue;
            ResourceAdd();
            return true;
        }
        else
            return false;
    }

    private void ResourceAdd()
    {
        resourceNumber++;
        resourceCounter.text = "x" + resourceNumber.ToString();
      
    }

    public void SumResourceNumber()
    {
        totalNumber += resourceNumber;
        overallResourceCounter.text = totalNumber.ToString();
    }

    public void ResourceReset()
    {
        resourceNumber = 0;
    }


   
}
