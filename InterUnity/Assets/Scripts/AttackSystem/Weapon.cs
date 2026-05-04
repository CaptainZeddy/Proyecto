using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool isAttacking = false;
    private float attackDuration = 0.5f; // Duración del ataque en segundos
    private float attackStartTime = 0f; // Tiempo en el que comenzo el ataque
    private HashSet<Collider> hitEnemies = new HashSet<Collider>(); // Rastrear enemigos ya golpeados

    public void Attack()
    {
        isAttacking = true;
        attackStartTime = Time.time;
        hitEnemies.Clear(); // Limpiar enemigos golpeados anteriormente
        Debug.Log("Weapon attack!");
    }
    private void OnTriggerStay(Collider other)
    {
        if(!isAttacking)return; // Solo hacer daño cuando está atacando
        if(hitEnemies.Contains(other))return; // No golpear al mismo enemigo dos veces
        if(other.CompareTag("Enemy"))
        {
            hitEnemies.Add(other); // Marcar como golpeado
            Debug.Log("¡tocado y hundido!");
            // TODO: Agregar lógica de daño aquí (ej: other.GetComponent<Enemy>().TakeDamage(damage);)
        }
    }
    public void endAttack()
    {
        isAttacking = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Finalizar ataque automáticamente si ha pasado la duración
        if(isAttacking && Time.time - attackStartTime >= attackDuration)
        {
            endAttack();
        }
    }
}
