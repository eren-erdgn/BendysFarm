using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [SerializeField] private ParticleSystem wateringParticles;
    [SerializeField] private GameObject water;
    [SerializeField] private int waterLevelMax = 5000;
    [SerializeField] private int waterLevel = 0;
    private void OnEnable()
    {
        Events.OnPlayerWaterPlant.AddListener(WaterPlant);
        Events.OnPlayerGetWater.AddListener(IncreaseWaterLevel);
    }

    private void OnDisable()
    {
        Events.OnPlayerWaterPlant.RemoveListener(WaterPlant);
        Events.OnPlayerGetWater.RemoveListener(IncreaseWaterLevel);
    }

    private void Start()
    {
        water.gameObject.SetActive(false);
    }

    private void WaterPlant()
    {
        if(waterLevel<=0) return;
        wateringParticles.Play();
        DecreaseWaterLevel();
    }
    private void DecreaseWaterLevel()
    {
        waterLevel--;
        water.gameObject.transform.localScale = new Vector3(0.2f, waterLevel*0.05f / (float)waterLevelMax, 0.2f);
        if (waterLevel <= 0)
        {
            water.gameObject.SetActive(false);
        }
    }
    private void IncreaseWaterLevel()
    {
            
            waterLevel = waterLevelMax;
            water.gameObject.transform.localScale = new Vector3(0.2f, waterLevel*0.05f / (float)waterLevelMax, 0.2f);
            water.gameObject.SetActive(true);
    }
}
