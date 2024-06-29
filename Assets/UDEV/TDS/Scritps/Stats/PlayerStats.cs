using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "UDEV/TDS/Create Player Stats")]
public class PlayerStats : ActorStats
{
    [Header("Level Up Base:")]
    public int level;
    public int maxLevel;
    public float xp;
    public float levelUpXpRequired;

    [Header("Level Up:")]
    public float levelUpXpRequiredUp;
    public float hpUp;

    public override bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

    public override void Load()
    {
        if (!string.IsNullOrEmpty(Prefs.playerData))
        {
            Debug.Log("playerData" + Prefs.playerData);
            JsonUtility.FromJsonOverwrite(Prefs.playerData, this);
        }
    }

    public override void Save()
    {
        Prefs.playerData = JsonUtility.ToJson(this);
    }

    public override void Upgrade(Action OnSuccess = null, Action OnFailed = null)
    {
        while (xp >= levelUpXpRequired && !IsMaxLevel())
        {
            level++;
            xp -= levelUpXpRequired;

            hp += hpUp * Helper.GetUpgradeFormula(level);
            levelUpXpRequired += levelUpXpRequiredUp * Helper.GetUpgradeFormula(level);

            Save();

            OnSuccess?.Invoke();
        }

        if(xp < levelUpXpRequired || IsMaxLevel()) {
            OnFailed?.Invoke();
        }
    }
}
