using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    [Tooltip("Prefab del cofre abierto que se instanciará cuando se active el objeto.")]
    public GameObject chestOpenPrefab;

    void Start()
    {
  
    }

    public void activarObjeto()
    {
        if (chestOpenPrefab != null)
        {
            Instantiate(chestOpenPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
        }

    }
}
