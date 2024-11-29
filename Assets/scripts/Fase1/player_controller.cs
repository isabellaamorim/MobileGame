using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using TMPro;
using System.Diagnostics;

public class player_controller : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Rigidbody2D rb;
    public Animator animator;

    private bool findCandle = false;
    private bool findKey = false;

    public GameObject door1;

    public TextMeshProUGUI countTime;
    private float countdown = 1f;

    private int time = 200;

    public TextMeshProUGUI getKey;
    public TextMeshProUGUI getCandle;

    public GameObject audio_coletavel;
    public GameObject popup;

    private bool isPopupActive = false;
    private float popupTimer = 4f;

    private void Start()
    {
        findCandle = false;
        findKey = false;
        time = 200;

        ShowPopup();

        SetCountText();
    }

    void Update()
    {
        // Atualiza a contagem de tempo
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            time -= 1;
            SetCountText();
            countdown = 1f;
        }

        if (time <= 0)
        {
            SceneManager.LoadSceneAsync(6); // Carrega a próxima cena
        }

        if (isPopupActive)
        {
            popupTimer -= Time.deltaTime;
            if (popupTimer <= 0f)
            {
                popup.SetActive(false);
                isPopupActive = false;
            }
        }
    }

    void FixedUpdate()
    {
        // O movimento é controlado pelo script Movimento.cs
    }

    public void MovePlayer(Vector2 movement, float speed)
    {
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        // Atualiza as animações
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void SetCountText()
    {
        countTime.text = "Time: " + time.ToString() + " s";
        getKey.text = "Key: " + (findKey ? "1/1" : "0/1");
        getCandle.text = "Candle: " + (findCandle ? "1/1" : "0/1");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("vela"))
        {
            other.gameObject.SetActive(false);
            findCandle = true;

            GameObject prefab = Instantiate(audio_coletavel, transform.position, Quaternion.identity);
            Destroy(prefab.gameObject, 1f);

            UnityEngine.Debug.Log("Vela coletada!");
        }

        if (other.gameObject.CompareTag("chave1"))
        {
            other.gameObject.SetActive(false);
            findKey = true;

            GameObject prefab = Instantiate(audio_coletavel, transform.position, Quaternion.identity);
            Destroy(prefab.gameObject, 1f);

            UnityEngine.Debug.Log("Chave coletada!");
        }

        // Verifica se as duas condições foram atendidas
        if (findKey && findCandle)
        {
            UpdateDoorInteraction(); // Ativa o trigger da porta
        }

        if (other.gameObject.CompareTag("door1") && findKey && findCandle)
        {
            UnityEngine.Debug.Log("Passando para a próxima cena!");
            SceneManager.LoadSceneAsync(3); // Passa para a próxima fase
        }
    }

    private void ShowPopup()
    {
        popup.SetActive(true);
        isPopupActive = true;
        popupTimer = 4f;
    }

    void UpdateDoorInteraction()
    {
        UnityEngine.Debug.Log("Ativando trigger na porta!");
        door1.GetComponent<TilemapCollider2D>().isTrigger = true;
    }
}