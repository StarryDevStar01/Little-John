using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "UDEV/TDS/Create Enemy Stats")]
public class EnemyStats : ActorStats
{
    [Header("Xp Bonus:")]
    public float minXpBonus;
    public float maxXpBonus;

    [Header("Level Up:")]
    public float hpUp;
    public float damageUp;

    public override void Load()
    {
        if (!string.IsNullOrEmpty(Prefs.enemyData))
        {
            Debug.Log("enemyData" + Prefs.enemyData);
            JsonUtility.FromJsonOverwrite(Prefs.enemyData, this);
        }
    }

    public override void Save()
    {
        Prefs.enemyData = JsonUtility.ToJson(this);
    }
}
