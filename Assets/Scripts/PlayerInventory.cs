using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventory : MonoBehaviour
{
    
    [SerializeField] private GameObject tomatoBox;
    [SerializeField] private GameObject cabbageBox;
    
    [SerializeField] private GameObject tomatoSeed;
    [SerializeField] private GameObject cabbageSeed;
    
    public static PlayerInventory Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private List<GameObject> tomatoSeeds = new();
    [SerializeField] private List<GameObject> cabbageSeeds = new();
    [SerializeField] private int rottenVegetables;
    [SerializeField] private int unRipeTomato;
    [SerializeField] private int unRipeCabbage;
    [SerializeField] private int ripeTomato;
    [SerializeField] private int ripeCabbage;
    [SerializeField] private int money;

    public int GetTomatoSeedsCount()
    {
        return tomatoSeeds?.Count ?? 0;
    }
    public int GetCabbageSeedsCount()
    {
        return cabbageSeeds?.Count ?? 0;
    }
    
    public int RipeTomato => ripeTomato;
    public int RipeCabbage => ripeCabbage;
    public int UnRipeTomato => unRipeTomato;
    public int UnRipeCabbage => unRipeCabbage;
    public int RottenVegetables => rottenVegetables;
    public int Money => money;
    
    
    
    public bool CheckForTomatoSeed()
    {
        return tomatoSeeds.Count > 0;
    }

    public int GetInventoryValue()
    {
        return unRipeTomato + unRipeCabbage + ripeTomato + ripeCabbage;
    }


    public GameObject GetTomatoSeed()
    {
        var getTomatoSeed = tomatoSeeds[0];
        tomatoSeeds.RemoveAt(0);
        Events.OnPlayerCountChanged.Invoke();
        return getTomatoSeed;
    }
    public GameObject GetCabbageSeed()
    {
        var getCabbageSeed = cabbageSeeds[0];
        cabbageSeeds.RemoveAt(0);
        Events.OnPlayerCountChanged.Invoke();
        return getCabbageSeed;
    }

    public bool CheckForCabbageSeed()
    {
        return cabbageSeeds.Count > 0;
    }
    
    public void BuyCabbageSeed()
    {
        if (money < 1) return;
        money -= 1;
        cabbageSeeds.Add(cabbageSeed);
        Events.OnPlayerCountChanged.Invoke();
    }
    
    public void BuyTomatoSeed()
    {
        if (money < 1) return;
        money -= 1;
        tomatoSeeds.Add(tomatoSeed);
        Events.OnPlayerCountChanged.Invoke();
    }
    public void SellItems()
    {
        money += GetInventoryValue();
        rottenVegetables = 0;
        unRipeTomato = 0;
        unRipeCabbage = 0;
        ripeTomato = 0;
        ripeCabbage = 0;
        tomatoBox.SetActive(false);
        cabbageBox.SetActive(false);
        Events.OnPlayerCountChanged.Invoke();
    }

    public void AddVegetable(GameObject o)
    {
        if (o.transform.GetChild(0).CompareTag("Tomato"))
        {
            tomatoBox.SetActive(true);
            ripeTomato++;
        }
        else if (o.transform.GetChild(0).CompareTag("Cabbage"))
        {
            cabbageBox.SetActive(true);
            ripeCabbage++;
        }
        else if (o.transform.GetChild(0).CompareTag("Rotten"))
        {
            rottenVegetables++;
        }
        else if (o.transform.GetChild(0).CompareTag("UnripeTomato"))
        {
            unRipeTomato++;
        }
        else if (o.transform.GetChild(0).CompareTag("UnripeCabbage"))
        {
            unRipeCabbage++;
        }
        else if (o.transform.GetChild(0).CompareTag("TomatoSeed"))
        {
            tomatoSeeds.Add(tomatoSeed);
        }
        else if (o.transform.GetChild(0).CompareTag("CabbageSeed"))
        {
            cabbageSeeds.Add(cabbageSeed);
        }
        Events.OnPlayerCountChanged.Invoke();
    }
}
