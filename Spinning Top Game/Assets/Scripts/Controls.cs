using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour {

    [SerializeField] private Spinner spinner;

    [SerializeField] private Slider powerSlider;
    [SerializeField] private Image sliderFillImage;

    [SerializeField] private float maxPower = 1;
    [SerializeField] private float reccomendedPower = 1;
    
    [SerializeField] private float fillSpeed = 1;

    void Start () {
		
	}

    float startPressedTime;
    bool released = true;

    public float colorSlider;

    Vector3 touchStartPosition;
    Vector3 touchEndPosition;

	void Update () {
        
        if (!released)
        {
            colorSlider = ((Time.timeSinceLevelLoad - startPressedTime * fillSpeed) - reccomendedPower);

            powerSlider.value = (Time.timeSinceLevelLoad - startPressedTime * fillSpeed) / maxPower;

            Color oldColor = sliderFillImage.color;

            float redColor = colorSlider;
            if (redColor < 0) redColor = 0;
            else if (redColor > 1) redColor = 1;
            oldColor.r = redColor;

            float greenColor = 1 - colorSlider;
            if (greenColor < 0) greenColor = 0;
            else if (greenColor > 1) greenColor = 1;
            oldColor.g = greenColor;
            
            sliderFillImage.color = oldColor;
        }
        else
        {
            float velocity = 0;

            powerSlider.value = Mathf.SmoothDamp(powerSlider.value, 0, ref velocity, 0.05f);
            Color oldColor = sliderFillImage.color;
            oldColor.g = Mathf.SmoothDamp(oldColor.g, 1, ref velocity, 0.05f);
            oldColor.r = Mathf.SmoothDamp(oldColor.r, 0, ref velocity, 0.05f);
            sliderFillImage.color = oldColor;
        }
    }

    private void OnValidate()
    {
        if (maxPower < 1) maxPower = 1;
        if (reccomendedPower < 1) reccomendedPower = 1;
        if (fillSpeed < 1) fillSpeed = 1;
    }

    public void PointerUp()
    {
        if (!released && !spinner.IsLaunched)
        {
            released = true;

            Vector2 mousePos;
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Camera.main.pixelHeight - Input.mousePosition.y;

            touchEndPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

            Vector3 launchDirection = touchEndPosition - touchStartPosition;
            launchDirection.z = -launchDirection.z;
            spinner.Launch(Time.timeSinceLevelLoad - startPressedTime * fillSpeed, launchDirection);
        }
    }

    public void PointerClick()
    {
        if (!spinner.IsLaunched)
        {
            startPressedTime = Time.timeSinceLevelLoad;
            touchStartPosition = Input.mousePosition;

            released = false;

            Vector2 mousePos;
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Camera.main.pixelHeight - Input.mousePosition.y;

            touchStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        }
    }
}
