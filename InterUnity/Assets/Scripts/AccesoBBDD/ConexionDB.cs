using System;
using MySql.Data.MySqlClient;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using UnityEngine;

public class ConexionDB : MonoBehaviour
{
    public PlayerStats playerStats;
    public int playerId = 1;
    string conexionDB;

    void Start()
    {
        conexionDB =
            "Server=db-stats.cuujsyktg2jv.us-east-1.rds.amazonaws.com;" +
            "Database=UnityDB;" +
            "User=admin;" +
            "Password=Temporal26;" +
            "Port=3306;";

       
        TestConexion();

        if (playerStats == null)
        {
            playerStats = GetComponent<PlayerStats>();
        }

        if (playerStats != null)
        {
            if (LoadPlayerStatsFromDB(playerId))
            {
                Debug.Log("PlayerStats cargado desde la base de datos");
            }
            else
            {
                Debug.LogWarning("No se pudo cargar PlayerStats para id=" + playerId);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró PlayerStats en este GameObject.");
        }

        Debug.Log("conexion: " + conexionDB);
    }

    public bool LoadPlayerStatsFromDB(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(conexionDB))
        {
            try
            {
                conn.Open();
                string query = @"SELECT level, maxLevel, strength, intelligence, vitality, mana, resistance, attackSpeed, manaRegen, maxHealth, maxMana
                                 FROM player_stats
                                 WHERE id = @id
                                 LIMIT 1;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader leerBD = cmd.ExecuteReader())
                    {
                        if (leerBD.Read())
                        {
                            playerStats.LoadFromDatabase(leerBD);
                            return true;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("NO FUNCA ERROR: " + ex.Message);
            }
        }

        return false;
    }

    void TestConexion()
    {
      using (MySqlConnection conn = new MySqlConnection(conexionDB))
        {
            try
            {
                conn.Open();
                Debug.Log("FUNCA - conexión abierta");
                String query = "SELECT * FROM player_stats;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                
                using (MySqlDataReader leerdb = cmd.ExecuteReader())
                {
                    if (leerdb.Read())
                    {
                        Debug.Log("Consulta válida. Resultado: " + leerdb[0].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("NO FUNCA ERROR: " + ex.Message);
            }
        }
    }
}
