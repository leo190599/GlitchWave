using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPuloPlayer : EstadoAtivoBasePlayer
{
    public override void AtualizarEstadoFixado()
    {
        base.AtualizarEstadoFixado();
        player.GetRigidbody2D.Cast(Vector2.down,player.GetRaycastsPulo,player.GetDistanciaChecagemPulo);
        
        //Essa checagem é feita para garantir que a personagem não volte para o estaddo Idle antes de sair do chão
        if(player.GetRaycastsPulo.Count!=0 && player.GetRigidbody2D.velocity.y<=0)
        {
            foreach(RaycastHit2D r in player.GetRaycastsPulo)
            {
                if(r.collider.tag!="Player")
                {
                    player.TrocaEstadoPlayer(new EstadoIdlePlayer());
                    return;
                }
            }
        }
        player.GetRaycastsPulo.Clear();
    }
}
