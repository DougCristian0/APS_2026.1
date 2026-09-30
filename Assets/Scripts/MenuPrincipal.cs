using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject pnlMenu;
    public GameObject pnlControles;

    public void Jogar()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SairDoJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }

    public void AbrirControles()
    {
        pnlMenu.SetActive(false);
        pnlControles.SetActive(true);
    }

    public void FecharControles()
    {
        pnlMenu.SetActive(true);
        pnlControles.SetActive(false);
    }
}