using UnityEngine;

public class SelectedObj : MonoBehaviour
{
    LayerMask mask;
    float distancia = 1.5f;
    public Texture2D cursor;
    public GameObject textE;
    GameObject ultimoObjeto = null;
     void Awake()
    {
        

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mask = LayerMask.GetMask("RayCast Detected");
        textE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            deselected();
            selectedObj(hit.transform);
          if(hit.collider.tag == "Objeto Interactivo")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<ObjetoInteractivo>().activarObjeto();
                }
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }
        else
        {
            deselected();
        }
    }
    void selectedObj(Transform transform)
    {
     transform.GetComponent<Renderer>().material.color = Color.red;
     ultimoObjeto = transform.gameObject;
    }
    void deselected()
    {
        if (ultimoObjeto != null)
        {
             ultimoObjeto.GetComponent<Renderer>().material.color = Color.white;
             ultimoObjeto = null;
        }
    }
     void OnGUI()
    {          
    
      GUI.DrawTexture(new Rect(Screen.width / 2 - cursor.width / 2, Screen.height / 2 - cursor.height / 2, cursor.width, cursor.height), cursor);

        if (ultimoObjeto != null)
        {
            textE.SetActive(true);
        }
        else
        {
            textE.SetActive(false);
        }
    }
}
