using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCon : MonoBehaviour
{
    public float speed = 3.0f;
    public float sliderSpeed = 0.1f;
    public Slider slider;
    
    void Update()
    {
     playerMove();  
    }

    public void playerMove()
    {
        float sliderValue = slider.value;
        float speed = Mathf.Abs(sliderValue * 3); // Get the absolute value of the slider value as the speed

        if (Input.GetKey(KeyCode.W)) // Increase slider value
        {
            sliderValue += sliderSpeed * Time.deltaTime;
            sliderValue = Mathf.Clamp(sliderValue, -1f, 1f); // Keep slider value between -1 and 1// Keep slider value between 0 and 1
            slider.value = sliderValue; // Update slider value
        }

        if (Input.GetKey(KeyCode.S)) // Decrease slider value
        {
            sliderValue -= sliderSpeed * Time.deltaTime;
            sliderValue = Mathf.Clamp(sliderValue, -1f, 1f); // Keep slider value between -1 and 1
            slider.value = sliderValue; // Update slider value
        }

        
        if (sliderValue > 0) // Move up
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else // Move down
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
    }
}
