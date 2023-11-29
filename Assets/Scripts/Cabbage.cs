using UnityEngine;

public class Cabbage : MonoBehaviour
{
    public GameObject[] cabbagePrefabs;
    private GameObject _currentCabbagePrefab;
    private Transform _cabbagePosition;
    private int _prefabIndex;
    private float _timer;
    private bool _growthDone;
    public PlantCondition plantCondition;

    private void Start()
    {
        
        _cabbagePosition = transform;
        _currentCabbagePrefab = InstantiateCabbage(cabbagePrefabs[_prefabIndex]);
    }

    private void Update()
    {
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
        if (_currentCabbagePrefab != null)
        {
            Destroy(_currentCabbagePrefab);
        }
        _prefabIndex = 3;
        _currentCabbagePrefab = InstantiateCabbage(cabbagePrefabs[_prefabIndex]);
    }
    private void UpdateCabbage()
    {
        if(_growthDone == true) return;
        if (_currentCabbagePrefab != null)
        {
            Destroy(_currentCabbagePrefab);
        }
        
        _prefabIndex = (_prefabIndex + 1) % cabbagePrefabs.Length;
        if (_prefabIndex == 3)
        {
            _prefabIndex = 2;
            _growthDone=true;
        }   
        _currentCabbagePrefab = InstantiateCabbage(cabbagePrefabs[_prefabIndex]);
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
            UpdateCabbage();
        }
    }
    private void OnBlueWarning()
    {
        if (_timer >= 20f)
        {
            _timer = 0;
            UpdateCabbage();
        }
    }
    private void OnGreenWarning()
    {
        if (_timer >= 10f)
        {
            _timer = 0;
            UpdateCabbage();
        }
    }
    private void OnRedWarning()
    {
        RotPlant();
        _growthDone = true;
    }
    private GameObject InstantiateCabbage(GameObject cabbagePrefab)
    {
        if (cabbagePrefab != null)
        {
            return Instantiate(cabbagePrefab, _cabbagePosition.position, _cabbagePosition.rotation, _cabbagePosition);
        }
        return null;
    }
}
