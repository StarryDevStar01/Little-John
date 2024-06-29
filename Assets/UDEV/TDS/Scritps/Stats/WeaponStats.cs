using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Stats", menuName = "UDEV/TDS/Create Weapon Stats")]
public class WeaponStats : Stats
{
    [Header("Base Stats:")]
    public int bullets;
    public float firerate;
    public float reloadTime;
    public float damage;

    [Header("Upgrade:")]
    public int level;
    public int maxLevel;
    public int bulletsUp;
    public float firerateUp;
    public float reloadTimeUp;
    public float damageUp;
    public int upgradePrice;
    public int upgradePriceUp;

    [Header("Limit:")]
    public float minFirerate = 0.1f;
    public float minReloadTime = 0.01f;

    public int BulletsUpInfo { get => bulletsUp * (level + 1); }

    public float FirerateUpInfo { get => firerateUp * Helper.GetUpgradeFormula(level + 1); }
    public float ReloadTimeUpInfo { get => reloadTimeUp * Helper.GetUpgradeFormula(level + 1); }
    public float DamageUpInfo { get => damageUp * Helper.GetUpgradeFormula(level + 1); }

    public override bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

    public override void Load()
    {
        if (!string.IsNullOrEmpty(Prefs.playerWeaponData))
        {
            JsonUtility.FromJsonOverwrite(Prefs.playerWeaponData, this);
        }
    }

    public override void Save()
    {
        Prefs.playerWeaponData = JsonUtility.ToJson(this);
    }

    public override void Upgrade(Action OnSuccess = null, Action OnFailed = null)
    {
        if(Prefs.IsEnoughCoins(upgradePrice) && !IsMaxLevel())
        {
            Prefs.coins -= upgradePrice;

            level++;
            bullets += bulletsUp * level;
            firerate -= firerateUp * Helper.GetUpgradeFormula(level);
            firerate = Mathf.Clamp(firerate, minFirerate, firerate);

            reloadTime -= reloadTimeUp * Helper.GetUpgradeFormula(level);
            reloadTime = Mathf.Clamp(reloadTime, minReloadTime, reloadTime);

            damage += damageUp * Helper.GetUpgradeFormula(level);
            upgradePrice += upgradePriceUp * level;

            Save();
            OnSuccess?.Invoke();

            return;
        }

        OnFailed?.Invoke();
    }
}
