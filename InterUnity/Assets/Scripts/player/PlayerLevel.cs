using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private PlayerStats stats;
    private int currentXP = 0;
    public int xpToNextLevel = 100;


    void Awake()
    {
        stats = GetComponent<PlayerStats>();

        if (stats == null)
        {
            Debug.LogError("❌ PlayerStats NO está en este GameObject o no se reconoce el script");
        }
        else
        {
            Debug.Log("✔ PlayerStats OK");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddXP(100);
        }
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
        if (stats.level < stats.maxLevel)
        {
            stats.level++;
            stats.IncreaseStat("strength");
            stats.RecalculateStats();
            Debug.Log("¡Nivel " + stats.level + "!");
        }
    }
}