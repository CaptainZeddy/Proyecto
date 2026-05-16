using System;
using MySql.Data.MySqlClient;
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

    public void LoadFromDatabase(MySqlDataReader leerBD)
    {
        level = leerBD["level"] != DBNull.Value ? Convert.ToInt32(leerBD["level"]) : level;
        maxLevel = leerBD["maxLevel"] != DBNull.Value ? Convert.ToInt32(leerBD["maxLevel"]) : maxLevel;
        strength = leerBD["strength"] != DBNull.Value ? Convert.ToInt32(leerBD["strength"]) : strength;
        intelligence = leerBD["intelligence"] != DBNull.Value ? Convert.ToInt32(leerBD["intelligence"]) : intelligence;
        vitality = leerBD["vitality"] != DBNull.Value ? Convert.ToInt32(leerBD["vitality"]) : vitality;
        mana = leerBD["mana"] != DBNull.Value ? Convert.ToInt32(leerBD["mana"]) : mana;
        resistance = leerBD["resistance"] != DBNull.Value ? Convert.ToInt32(leerBD["resistance"]) : resistance;
        attackSpeed = leerBD["attackSpeed"] != DBNull.Value ? Convert.ToSingle(leerBD["attackSpeed"]) : attackSpeed;
        manaRegen = leerBD["manaRegen"] != DBNull.Value ? Convert.ToSingle(leerBD["manaRegen"]) : manaRegen;
        maxHealth = leerBD["maxHealth"] != DBNull.Value ? Convert.ToInt32(leerBD["maxHealth"]) : maxHealth;
        maxMana = leerBD["maxMana"] != DBNull.Value ? Convert.ToInt32(leerBD["maxMana"]) : maxMana;
    }

    public void RecalculateStats()
    {
        maxHealth = 10 + (vitality * 1);
        maxMana = 10 + (mana * 1);
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