using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int level = 1;
    public int maxLevel = 25;

    public int strength = 1;
    public int intelligence = 1;
    public int vitality = 1;
    public int mana = 1;
    public int resistance = 0;

    public float attackSpeed = 1f; // ataques por segundo
    public float manaRegen = 0.5f;

    public int maxHealth;
    public int maxMana;

    void Start()
    {
        RecalculateStats();
    }

    public void RecalculateStats()
    {
        maxHealth = 100 + (vitality * 10);
        maxMana = 50 + (mana * 5);
        manaRegen = 0.5f + (mana * 0.5f);
        attackSpeed = 1f + (level * 0.2f);
    }

    public int CalculatePhysicalDamage(int baseDamage)
    {
        return baseDamage + strength;
    }

    public int CalculateMagicDamage(int baseDamage)
    {
        return baseDamage + intelligence;
    }

    public int ReduceDamage(int incomingDamage)
    {
        return Mathf.Max(incomingDamage - resistance, 0);
    }

    public void IncreaseStat(string stat)
    {
        switch (stat)
        {
            case "strength":
                Debug.Log("Subes fuerza de " + (strength));
                strength++;
                Debug.Log("Subes fuerza a " + (strength));
                break;

            case "vitality":
                vitality++;
                break;

            case "intelligence":
                intelligence++;
                break;

            case "mana":
                mana++;
                break;

            case "resistance":
                resistance++;
                break;
        }

        RecalculateStats();
    }

}