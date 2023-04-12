using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NovoMapeadorDeBotoes",menuName ="ScriptableObjectsCustomizados/MapeadorDeBotoes")]
public class MapeadorDeBotoes : ScriptableObject
{
    [SerializeField]
    private string eixoDeMovimentoHorizontal="Horizontal";
   [SerializeField]
   private KeyCode botaoPulo=KeyCode.Space;

   public string GetEixoDeMovimentoHorizontal=>eixoDeMovimentoHorizontal;
   public KeyCode GetBotaoPulo=>botaoPulo;
}
