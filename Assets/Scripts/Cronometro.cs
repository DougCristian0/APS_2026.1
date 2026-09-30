using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    private static float tempo;
    public TMP_Text txtCronometro;
    Pontos Pontos = new Pontos();

    public void reiniciaTempo()
    {
        tempo = 31;
    }

    private void Start()
    {
        tempo = 31;
    }

    void Update()
    {
        if (Pontos.getVida() > 0) { tempo -= Time.deltaTime; }
        txtCronometro.text = "Tempo: " + Mathf.FloorToInt(tempo);
        if (tempo <= 0) { Pontos.zeraVida(); }
    }
}
