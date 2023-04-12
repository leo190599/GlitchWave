using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoIdlePlayer : EstadoAtivoBasePlayer
{
    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        Debug.Log("a");
    }
}
