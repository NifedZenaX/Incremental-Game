using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [Range(0f, 1f)]
    public float AutoCollectPercentage = 0.1f;
    public ResourceConfig[] ResourcesConfigs;

    public Transform ResourcesParent;
    public ResourceController ResourcePrefab;

    public Text GoldInfo;
    public Text AutoCollectInfo;

    private List<ResourceController> _activeResources = new List<ResourceController>();
    private float _collectSecond;

    private double _totalGold;

    void Start()
    {
        AddAllResources();
    }

    void Update()
    {
        _collectSecond += Time.unscaledDeltaTime;
        if(_collectSecond >= 1f)
        {
            CollectPerSecond();
            _collectSecond = 0f;
        }
    }

    private void AddAllResources()
    {
        foreach (ResourceConfig config in ResourcesConfigs)
        {
            GameObject obj = Instantiate(ResourcePrefab.gameObject, ResourcesParent, false);
            ResourceController resource = obj.GetComponent<ResourceController>();
            resource.SetConfig(config);
            _activeResources.Add(resource);
        }
    }

    private void CollectPerSecond()
    {
        double output = 0f;
        foreach (ResourceController resource in _activeResources)
        {
            output += resource.GetOutput();
        }
        output *= AutoCollectPercentage;
        AutoCollectInfo.text = $"Auto Collect: {output.ToString("F1")}/second";
        AddGold(output);
    }

    private void AddGold(double value)
    {
        _totalGold += value;
        GoldInfo.text = $"Gold:{_totalGold.ToString("0")}";
    }
}



[System.Serializable]
public struct ResourceConfig
{
    public string Name;
    public double UnlockCost;
    public double UpgradeCost;
    public double Output;
}