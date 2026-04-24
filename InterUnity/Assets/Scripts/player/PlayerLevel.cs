using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private PlayerStats stats;

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
        LevelUp();
    }

    void LevelUp()
    {
        stats.IncreaseStat("strength");
    }
}