using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class FlashLight : MonoBehaviour
{
    public GameObject SliderHud;
    public int TimeRate;

    private Slider _slider;
    private Light2D _light;

    // Start is called before the first frame update
    void Start()
    {
        _slider = SliderHud.GetComponent<Slider>();
        _light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<GameMode>().IsPause)
        {
            _slider.value -= Time.deltaTime / TimeRate;
            _light.intensity -= Time.deltaTime / TimeRate;
            if (_slider.value <= 0)
                _slider.value = 0;
            if (_light.intensity <= 0)
                _light.intensity = 0;
        }            
    }

    public void AddBattery(int Amount)
    {
        _slider.value += (float)Amount / TimeRate;
        _light.intensity += (float)Amount / TimeRate;
        if (_slider.value >= 1)
            _slider.value = 1;
        if(_light.intensity >= 1)
            _light.intensity = 1;
    }
}
