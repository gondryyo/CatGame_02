using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlayer : MonoBehaviour
{
    [SerializeField] private float speed;

    public static CatPlayer Instance;
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;

    public List<Transform> catList = new List<Transform>();

    //el jugador solo se moverá de izquierda a derecha, la traslación en Y estará bloqueada
    //por lo tanto el BG será el que se moverá, especificamente el tile será un prefab que irá generandose con el tiempo
    //necestio resolver lo de los gatitos, ¿cómo aparecerán?, ¿en línea o en bolita?

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerInputActions.Enable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(playerInputActions.Standard.Movement.ReadValue<Vector2>().x, playerInputActions.Standard.Movement.ReadValue<Vector2>().y, 0).normalized;
        rb.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Cat"))
        {
            if (collision.transform.GetComponent<CatScript>().catCollected == false)
            {
                Transform currentCat = collision.transform;

                if (catList.Count == 0)
                {
                    collision.transform.GetComponent<CatScript>().currentTarget = transform;
                }
                else
                {
                    collision.transform.GetComponent<CatScript>().currentTarget = catList[catList.Count - 1];
                }

                collision.transform.GetComponent<CatScript>().catCollected = true;
                catList.Add(currentCat);
            }
            
        }
        
    }
}
