using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private TextMeshProUGUI textoNiveles;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnearEnemigos());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnearEnemigos()
    {
        for(int i = 0; i < 5; i++) 
        {
            for(int j = 0; j < 3; j++) 
            {
                textoNiveles.text = "Nivel " + (i + 1) + "-" + " Oleada " + (j + 1);
                yield return new WaitForSeconds(2f);
                textoNiveles.text = "";
                for(int k = 0; k < 10; k++) 
                {
                    Vector3 puntoAleatorio = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), 0);
                    Instantiate(enemigoPrefab, puntoAleatorio, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(3f);
        }
        
    }
}
