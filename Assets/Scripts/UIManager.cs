
using System;
using EventSystem;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waterWellKeyText;
    [SerializeField] private TextMeshProUGUI harvestAndWaterKeyText;
    [SerializeField] private TextMeshProUGUI seedKeyText;
    [SerializeField] private TextMeshProUGUI playerUIText;
    [SerializeField] private TextMeshProUGUI tradeSellText;
    [SerializeField] private TextMeshProUGUI tradeBuyCabbageText;
    [SerializeField] private TextMeshProUGUI tradeBuyTomatoText;

    private void OnEnable()
    {
        Events.OnPlayerAtWaterWell.AddListener(WaterWellInfo);
        Events.OnPlayerCanSeed.AddListener(SeedInfo);
        Events.OnPlayerCanHarvest.AddListener(HarvestInfo);
        Events.OnPlayerCountChanged.AddListener(PlayerInfo);
        Events.OnPlayerAtClipboard.AddListener(ShowTrade);
    }

    

    private void OnDisable()
    {
        Events.OnPlayerAtWaterWell.RemoveListener(WaterWellInfo);
        Events.OnPlayerCanSeed.RemoveListener(SeedInfo);
        Events.OnPlayerCanHarvest.RemoveListener(HarvestInfo);
        Events.OnPlayerCountChanged.AddListener(PlayerInfo);
        Events.OnPlayerAtClipboard.RemoveListener(ShowTrade);
    }
    private void ShowTrade(bool atClipboard)
    {
        
        tradeSellText.text = $"[O]\n" +
                             $"Sell your good for this price?\n" +
                             $"Money: {PlayerInventory.Instance.GetInventoryValue()}" + "$";  
        tradeSellText.enabled = atClipboard;                    
        tradeBuyCabbageText.enabled = atClipboard;
        tradeBuyTomatoText.enabled = atClipboard;
        
    }
    private void PlayerInfo()
    {
        playerUIText.text = $"Tomato Seeds: {PlayerInventory.Instance.GetTomatoSeedsCount()}\n" +
                            $"Cabbage Seeds: {PlayerInventory.Instance.GetCabbageSeedsCount()}\n" +
                            $"Unripe Cabbages: {PlayerInventory.Instance.UnRipeCabbage}\n" +
                            $"Unripe Tomatoes: {PlayerInventory.Instance.UnRipeTomato}\n" +
                            $"Ripe Tomatoes: {PlayerInventory.Instance.RipeTomato}\n" +
                            $"Ripe Cabbages: {PlayerInventory.Instance.RipeCabbage}\n" +
                            $"Rotten: {PlayerInventory.Instance.RottenVegetables}\n" +
                            $"Money: {PlayerInventory.Instance.Money}" + "$";   
    }
    private void HarvestInfo(bool atPlant)
    {
        harvestAndWaterKeyText.enabled = atPlant;
    }
    private void SeedInfo(bool atPlant)
    {
        seedKeyText.enabled = atPlant;
    }

    private void WaterWellInfo(bool atWaterWell)
    {
        waterWellKeyText.enabled = atWaterWell;
    }
}
