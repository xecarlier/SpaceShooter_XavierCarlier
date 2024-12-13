using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float ratioDisparo;
    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject spawnPoint1;
    [SerializeField] private GameObject spawnPoint2;
    [SerializeField] private TextMeshProUGUI textoVida;
    private AudioSource audioSource;
    private float temporizador = 0.5f;
    private float vida = 100f;
    private float old_rt;
    private float old_spd;
    // Start is called before the first frame update
    void Start()
    {
        textoVida.text = "Vida: " + vida;
        old_rt = ratioDisparo;
        old_spd = velocidad;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        DelimitarMovimiento();
        Disparar();
    }

    void Movimiento()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * velocidad * Time.deltaTime);
    }

    void DelimitarMovimiento()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -8.4f, 8.4f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    void Disparar()
    {
        temporizador += 1 * Time.deltaTime;
        if(Input.GetKey(KeyCode.Space) && temporizador > ratioDisparo)
        {
            Instantiate(disparoPrefab, spawnPoint1.transform.position, Quaternion.identity);
            Instantiate(disparoPrefab, spawnPoint2.transform.position, Quaternion.identity);
            temporizador = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if(elOtro.gameObject.CompareTag("DisparoEnemigo") || elOtro.gameObject.CompareTag("Enemigo"))
        {
            vida -= 5;
            textoVida.text = "Vida: " + vida;
            Destroy(elOtro.gameObject);
            if(vida<=0)
            {
                StartCoroutine(Death());
            }
        }
        else if (elOtro.gameObject.CompareTag("PowerUp")){
            PowerUp pwr_scrpt = elOtro.gameObject.GetComponent<PowerUp>();
            if (pwr_scrpt.typePower == "atk"){
                StartCoroutine(RaiseAtkRate());
            }
            else if (pwr_scrpt.typePower == "spd"){
                StartCoroutine(RaiseSpd());
            }
            else {
                vida += 20;
            }
            Destroy(elOtro.gameObject);
        }
    }

    IEnumerator Death()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    IEnumerator RaiseAtkRate()
    {
        old_rt = ratioDisparo;
        ratioDisparo = 0.25f;
        yield return new WaitForSeconds(4f);
        ratioDisparo = old_rt;
    }

    IEnumerator RaiseSpd()
    {
        old_spd = velocidad;
        velocidad = 8f;
        yield return new WaitForSeconds(4f);
        velocidad = old_spd;
    }
}
