using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private int waterLevel = 0;
    
    [SerializeField] private Material redWarning;
    [SerializeField] private Material yellowWarning;
    [SerializeField] private Material blueWarning;
    [SerializeField] private Material greenWarning;

    public PlantCondition plantCondition;
    private MeshRenderer _meshRenderer;
    private bool _isThereAnyPlant = false;
    private float _timer;
    private float _timer2;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void CheckChild()
    {
        _isThereAnyPlant = transform.childCount != 0;
    }
    private void Update()
    {
        CheckChild();
        if (_isThereAnyPlant == false)
        {
            waterLevel = 0;
            return;
        }
        _timer += Time.deltaTime;
        if (_timer >= 0.03f)
        {
            _timer = 0;
            waterLevel--;
        }

        switch (waterLevel)
        {
            case <= -500 and > -1000:
                plantCondition = PlantCondition.BadYellow;
                _meshRenderer.material = yellowWarning;
                break;
            case <= 500 and > -500:
                plantCondition = PlantCondition.GoodBlue;
                _meshRenderer.material = blueWarning;
                break;
            case <= 1000 and > 500:
                plantCondition = PlantCondition.AwesomeGreen;
                _meshRenderer.material = greenWarning;
                break;
            case <= 1500 and > 1000:
                plantCondition = PlantCondition.BadYellow;
                _meshRenderer.material = yellowWarning;
                break;
            default:
                plantCondition = PlantCondition.DeadRed;
                _meshRenderer.material = redWarning;
                break;
        }
    }
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            waterLevel++;
        }    
    }
    
}
public enum PlantCondition
{
    GoodBlue,
    AwesomeGreen,
    BadYellow,
    DeadRed
}
