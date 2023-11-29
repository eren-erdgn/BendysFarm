
using System;
using EventSystem;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isAtWaterWell;
    private bool _isAtPlant;
    private bool _isPlantHasVegetable;
    private bool _isAtClipboard;
    
    
    private Transform _plantSelection;

    private Transform _waterWellSelection;
    
    private Transform _clipboardSelection;

    private void Start()
    {
        _mainCamera = Camera.main;
        Events.OnPlayerCountChanged.Invoke();
    }
    
    private void Update()
    {
        ShowInfo();
        if (Input.GetMouseButton(0))
        {
            Events.OnPlayerWaterPlant.Invoke();
        }

        if (_isAtWaterWell && Input.GetKeyDown(KeyCode.E))
        {
            Events.OnPlayerGetWater.Invoke();
        }
        if(_isAtPlant && Input.GetKeyDown(KeyCode.T))
        {
            SeedTomato();
        }
        if(_isAtPlant && Input.GetKeyDown(KeyCode.C))
        {
            SeedCabbage();
        }
        if(_isAtPlant && Input.GetKeyDown(KeyCode.E))
        {
            Harvest();
        }
        if(_isAtClipboard && Input.GetKeyDown(KeyCode.O))
        {
            PlayerInventory.Instance.SellItems();
        }
        if(_isAtClipboard && Input.GetKeyDown(KeyCode.T))
        {
            PlayerInventory.Instance.BuyTomatoSeed();
        }
        if(_isAtClipboard && Input.GetKeyDown(KeyCode.C))
        {
            PlayerInventory.Instance.BuyCabbageSeed();
        }
            
    }

    private void Harvest()
    {
        if (_plantSelection.childCount <= 0) return;
        var vegetable = _plantSelection.GetChild(0);
        var o = vegetable.gameObject;
        PlayerInventory.Instance.AddVegetable(o);
        Destroy(o);
    }


    private void SeedCabbage()
    {
        if ( _plantSelection.childCount > 0 || !PlayerInventory.Instance.CheckForCabbageSeed()) return;
        var cabbageSeed = PlayerInventory.Instance.GetCabbageSeed();
        var seededCabbage = Instantiate(cabbageSeed, _plantSelection.position, _plantSelection.rotation);
        seededCabbage.transform.SetParent(_plantSelection);
    }

    private void SeedTomato()
    {
        if ( _plantSelection.childCount > 0 || !PlayerInventory.Instance.CheckForTomatoSeed()) return;
        var tomatoSeed = PlayerInventory.Instance.GetTomatoSeed();
        var seededTomato = Instantiate(tomatoSeed, _plantSelection.position, _plantSelection.rotation);
        seededTomato.transform.SetParent(_plantSelection);
    }
    
    
    private void ShowInfo()
    {
        if (_clipboardSelection != null)
        {
            Events.OnPlayerAtClipboard.Invoke(false);
            _isAtClipboard = false;
            _clipboardSelection = null;
        }
        if (_plantSelection != null)
        {
            _isAtPlant = false;
            _isPlantHasVegetable = false;
            ShowPlantWaterNeed();           
            ShowPlantUI(_isPlantHasVegetable);
            _isAtWaterWell = false;
            _plantSelection = null;
        }
        if(_waterWellSelection != null)
        {
            Events.OnPlayerAtWaterWell.Invoke(false);
            _isAtWaterWell = false;
            _waterWellSelection = null;
        }
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit,2f))
        {
            var selection = hit.collider;
            if (selection.CompareTag("WaterWell"))
            {
                _waterWellSelection = selection.transform;
                Events.OnPlayerAtWaterWell.Invoke(true);
                _isAtWaterWell = true;
            }
            if(selection.CompareTag("Plants"))
            {
                _plantSelection = selection.transform;
                _isAtPlant = true;
                _isPlantHasVegetable = _plantSelection.childCount > 0;
                ShowPlantUI(_isPlantHasVegetable);
                ShowPlantWaterNeed();
            }
            if(selection.CompareTag("Clipboard"))
            {
                _clipboardSelection = selection.transform;
                Events.OnPlayerAtClipboard.Invoke(true);
                _isAtClipboard = true;
            }
        }
    }
    
    private void ShowPlantWaterNeed()
    {
        if (_isAtPlant)
        {
            if (_isPlantHasVegetable)
            {
                
                _plantSelection.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                _plantSelection.GetComponent<MeshRenderer>().enabled = false;
            }
            
        }
        else
        {
            _plantSelection.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }
    
    private void ShowPlantUI(bool isPlantHasChild)
    {
        if (_isAtPlant)
        {
            if (isPlantHasChild)
            {
                Events.OnPlayerCanHarvest.Invoke(true);
                Events.OnPlayerCanSeed.Invoke(false);
            }
            else
            {
                Events.OnPlayerCanHarvest.Invoke(false);
                Events.OnPlayerCanSeed.Invoke(true);
            }
            
        }
        else
        {
            Events.OnPlayerCanHarvest.Invoke(false);
            Events.OnPlayerCanSeed.Invoke(false);
        }
        
    }
}
