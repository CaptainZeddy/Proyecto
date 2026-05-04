using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Weapon weapon;
    public Animator animator;
    public float cooldownTime = 0.3f; // Tiempo de enfriamiento DESPUÉS del combo
    private static int attackCount = 0; // Contador de ataques
    float maxComboDelay = 0.8f; // Tiempo máximo entre clicks para mantener el combo
    float lastClickTime = 0f; // Tiempo del último clic
    float noOfClick = 0f; // Contador de clics
    private float comboEndTime = 0f; // Tiempo cuando debe terminar el combo

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Resetear combo si se excede el tiempo máximo entre ataques
        if(Time.time - lastClickTime > maxComboDelay && noOfClick > 0)
        {
            noOfClick = 0f; // Reiniciar el contador de clics si se excede el tiempo máximo entre ataques
            comboEndTime = Time.time + cooldownTime; // Establecer cooldown después de resetear combo
        }

        // Resetear animaciones cuando terminan
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && animator.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
           animator.SetBool("Hit1", false);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && animator.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
           animator.SetBool("Hit2", false);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && animator.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))
        {
           animator.SetBool("Hit3", false);
           noOfClick = 0f; // Reiniciar el contador de clics después del tercer ataque
           weapon.endAttack(); // Finalizar ataque después del tercero
           comboEndTime = Time.time + cooldownTime; // Establecer cooldown después de completar combo
        }

        // Manejar input de ataque (sin cooldown entre clicks del combo)
        if(Time.time >= comboEndTime && Input.GetMouseButtonDown(0))
        {
            onClick();
        }
    }
    void onClick()
    {
        lastClickTime = Time.time;
        attackCount++;
        noOfClick++; // Incrementar el contador de clics
        noOfClick = Mathf.Clamp(noOfClick, 0, 3); // Limitar el número de clics a 3

        // Primer ataque
        if(noOfClick == 1)
        {
            weapon.Attack(); // Llamar ataque en el weapon
            animator.SetBool("Hit1", true);
        }
        // Segundo ataque (después de que el primero haya avanzado)
        else if(noOfClick == 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && animator.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            animator.SetBool("Hit1", false); // Reiniciar el estado del primer ataque
            animator.SetBool("Hit2", true);
            weapon.Attack(); // Llamar ataque en el weapon
        }
        // Tercer ataque (después de que el segundo haya avanzado)
        else if(noOfClick == 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && animator.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
            animator.SetBool("Hit2", false); // Reiniciar el estado del segundo ataque
            animator.SetBool("Hit3", true);
            weapon.Attack(); // Llamar ataque en el weapon
        }
    }
}
