using JetBrains.Annotations;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;

    public bool temCronometro;

    public float spd;
    public float jumpForce;
    public LayerMask ground;
    private bool grounded;

    public TMP_Text vidaTxt;
    public TMP_Text pntTxt;

    public string level;

    public int tipoDoJogo;

    private int auxDirecaoHor;
    private int auxDirecaoVer;


    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    Pontos Pontos = new Pontos();

    private void Start()
    {
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
    }

    void Update()
    {
        vidaTxt.text = "Vidas: " + Pontos.getVida();
        pntTxt.text = "Pontos: " + Pontos.getPonto() + "/" + Pontos.getObjetivo();


        switch (tipoDoJogo)
        {
            case 1:
                if (auxDirecaoVer != 0) { player.transform.Translate(0, spd * Time.deltaTime * auxDirecaoVer, 0); }
                
                if (auxDirecaoHor != 0 || auxDirecaoVer != 0) { animator.SetBool("Swim", true); } else { animator.SetBool("Swim", false); }

                if (Pontos.getVida() > 0) { Swim(); }
                else { Derrota(); }
                break;

            case 2:
                grounded = Physics2D.Raycast(player.transform.position, Vector3.down, 1f, ground);

                if (auxDirecaoHor != 0) { animator.SetBool("Walk", true); } else { animator.SetBool("Walk", false); }
                
                if (grounded) { animator.SetBool("Jump", false); } else { animator.SetBool("Jump", true); }

                if (Pontos.getVida() > 0) { Move(); Jump(); }
                else { Derrota(); }
                break;

        }

        if (auxDirecaoHor != 0) {
            player.transform.Translate(spd * Time.deltaTime * auxDirecaoHor, 0, 0);

            if (auxDirecaoHor == -1) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
        }


    }

    public void Derrota()
    {
        spd = 0;

        switch (tipoDoJogo)
        {
            case 1:
                animator.SetBool("Swim", false);
                break;

            case 2:
                animator.SetBool("Walk", false);
                animator.SetBool("Jump", false);
                break;

        }

        GameObject[] huds = GameObject.FindGameObjectsWithTag("HUD");

        foreach (GameObject hud in huds)
        {
            hud.SetActive(false);
        }

        GameObject[] telaDerrota = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.CompareTag("Loser")).ToArray();

        foreach (GameObject tela in telaDerrota)
        {
            tela.SetActive(true);
        }
    }

    public void TocHorizontal(int direcao)
    {
        if (Pontos.getVida() > 0)
        {
            auxDirecaoHor = direcao;
        }
    }

    public void TocVertical(int direcao)
    {
        if (Pontos.getVida() > 0)
        {
            auxDirecaoVer = direcao;
        }
    }

    public void BtJump()
    {
        if (Pontos.getVida() > 0)
        {
            if (grounded)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void Swim()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        player.transform.position += movement * Time.deltaTime * spd;
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        player.transform.position += movement * Time.deltaTime * spd;
    }

    public void ReiniciarNivel()
    {
        if (temCronometro) { Cronometro cronometro = new Cronometro(); cronometro.reiniciaTempo(); }
        Pontos.reiniciaPontoVida();
        SceneManager.LoadScene(level);
    }
    public void VaiProMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
