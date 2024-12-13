using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion;
    public string typePower;
    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(1,4);
        
        if (num == 1) {
            typePower = "atk";
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        } else if (num == 2){
            typePower = "spd";
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        } else {
            typePower = "life";
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
}
