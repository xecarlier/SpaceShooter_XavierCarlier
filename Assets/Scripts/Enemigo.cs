using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private GameObject spawnPoint;
    private AudioSource audioSource;
    private float vida = 30f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnearDisparos());
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);
    }

    IEnumerator SpawnearDisparos()
    {
        while(true)
        {
            Instantiate(disparoPrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if(elOtro.gameObject.CompareTag("DisparoPlayer") || elOtro.gameObject.CompareTag("Player"))
        {
            vida -= 20;
            if (elOtro.gameObject.CompareTag("DisparoPlayer")){
                Destroy(elOtro.gameObject);
            }
            if(vida<=0)
            {
                if(Random.value < 0.15f)
                {
                    Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                }
                StartCoroutine(Death());
            }
        }
    }

    IEnumerator Death()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
