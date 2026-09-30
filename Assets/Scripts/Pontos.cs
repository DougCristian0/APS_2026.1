using UnityEngine;
using UnityEngine.SceneManagement;

public class Pontos : MonoBehaviour
{
    private static int vida = 3;
    private static int ponto = 0;
    private static int objetivo;
    public string nextLevel;

    public int objetivoNivel;

    void Start()
    {
        objetivo = objetivoNivel;
    }

    public int getVida()
    {
        return vida;
    }

    public int getPonto()
    {
        return ponto;
    }

    public int getObjetivo()
    {
        return objetivo;
    }

    public void reiniciaPontoVida()
    {
        vida = 3;
        ponto = 0;
    }

    public void zeraVida()
    {
        vida = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ponto >= objetivo) { SceneManager.LoadScene(nextLevel); reiniciaPontoVida(); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            vida--;
            Destroy(collision.gameObject);
        }
        else if (collision.collider.CompareTag("Point"))
        {
            ponto++;
            Destroy(collision.gameObject);
        }
    }
}
