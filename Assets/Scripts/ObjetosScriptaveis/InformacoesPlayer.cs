using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="NovasInformacoesPlayer",menuName ="ScriptableObjectsCustomizados/InformacoesPlayer")]
public class InformacoesPlayer : ScriptableObject
{
    [SerializeField]
    private float vidaMaxima=100;
    [SerializeField]
    private float vidaAtual=100;

    public float GetPorcentagemDeVida=>Mathf.Clamp(vidaAtual/vidaMaxima,0f,100f);
    public float GetVidaAtual=>Mathf.Clamp(vidaAtual,0,vidaMaxima);
    public void ReceberDano(float quantidadeDeDano)
    {
        vidaAtual-=quantidadeDeDano;
        if(vidaAtual>0)
        {
            EventosLevarDano.Invoke();
        }
        else
        {
            EventosMorte.Invoke();
        }
    }
    public UnityAction EventosLevarDano;
    public UnityAction EventosCura;
    public UnityAction EventosMorte;
}
