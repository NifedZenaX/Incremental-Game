﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public Button ResourceButton;
    public Image ResourceImage;
    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;

    private ResourceConfig _config;
    private int _level = 1;

    private void Start()
    {
        ResourceButton.onClick.AddListener(UpgradeLevel);
    }

    public void SetConfig(ResourceConfig config)
    {
        _config = config;
        ResourceDescription.text = $"{_config.Name} Lv.{_level}\n+{GetOutput().ToString("0")}";
        ResourceUnlockCost.text = $"Unlock Cost\n{GetUnlockCost()}";
        ResourceUpgradeCost.text = $"Upgrade Cost\n{GetUpgradeCost()}";
    }

    public double GetOutput()
    {
        return _config.Output * _level;
    }

    public double GetUpgradeCost()
    {
        return _config.UpgradeCost * _level;
    }

    public void UpgradeLevel()
    {
        double upgradeCost = GetUpgradeCost();
        if(GameManager.Instance.TotalGold < upgradeCost)
        {
            return;
        }
        GameManager.Instance.AddGold(-upgradeCost);
        _level++;
        ResourceUpgradeCost.text = $"Upgrade Cost\n{ GetUpgradeCost() }";
        ResourceDescription.text = $"{_config.Name} Lv.{_level}\n+{GetOutput().ToString("0")}";
    }

    public double GetUnlockCost()
    {
        return _config.UnlockCost;
    }
}
