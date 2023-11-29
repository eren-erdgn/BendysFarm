using System;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public GameObject[] tomatoPrefabs;
    private GameObject _currentTomatoPrefab;
    private Transform _tomatoPosition;
    private int _prefabIndex;
    private float _timer;
    private bool _growthDone;
    public PlantCondition plantCondition;

    private void Start()
    {
        
        _tomatoPosition = transform;
        _currentTomatoPrefab = InstantiateTomato(tomatoPrefabs[_prefabIndex]);
    }

    private void Update()
    {
        
        if (_prefabIndex == 2)
        {
            
        }
        GetParentPlantCondition();
        _timer += Time.deltaTime;
        switch (plantCondition)
        {
            case PlantCondition.BadYellow:
                OnYellowWarning();
                break;
            case PlantCondition.GoodBlue:
                OnBlueWarning();
                break;
            case PlantCondition.AwesomeGreen:
                OnGreenWarning();
                break;
            case PlantCondition.DeadRed:
                OnRedWarning();
                break;

            default:
                OnRedWarning();
                break;
        }
        
    }
    private void RotPlant()
    {
        if (_currentTomatoPrefab != null)
        {
            Destroy(_currentTomatoPrefab);
        }
        
        _prefabIndex = 3;
        _currentTomatoPrefab = InstantiateTomato(tomatoPrefabs[_prefabIndex]);
    }
    private void UpdateTomato()
    {
        if(_growthDone == true) return;
        if (_currentTomatoPrefab != null)
        {
            Destroy(_currentTomatoPrefab);
        }
        
        _prefabIndex = (_prefabIndex + 1) % tomatoPrefabs.Length;
        if (_prefabIndex == 3)
        {
            _prefabIndex = 2;
            _growthDone=true;
        }   
        _currentTomatoPrefab = InstantiateTomato(tomatoPrefabs[_prefabIndex]);
    }
    
    private void GetParentPlantCondition()
    {
        plantCondition = transform.parent.GetComponent<Plant>().plantCondition;
    }
    
    private void OnYellowWarning()
    {
        if (_timer >= 30f)
        {
            _timer = 0;
            UpdateTomato();
        }
    }
    private void OnBlueWarning()
    {
        if (_timer >= 20f)
        {
            _timer = 0;
            UpdateTomato();
        }
    }
    private void OnGreenWarning()
    {
        if (_timer >= 10f)
        {
            _timer = 0;
            UpdateTomato();
        }
    }
    private void OnRedWarning()
    {
        RotPlant();
        _growthDone = true;
    }
    private GameObject InstantiateTomato(GameObject tomatoPrefab)
    {
        if (tomatoPrefab != null)
        {
            return Instantiate(tomatoPrefab, _tomatoPosition.position, _tomatoPosition.rotation, _tomatoPosition);
        }
        return null;
    }
}
