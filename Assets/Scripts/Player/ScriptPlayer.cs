using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    [Header("Parametros Design")]
    [SerializeField]
    private float velocidadeDeMovimento=5f;
    [SerializeField]
    private float forcaPulo=10;
    [SerializeField]
    private bool olhandoParaDireita=true;

    [SerializeField]
    [Range(0f,1f)]
    private float perdaDeMovimentoNoAr=0f;

    [Header("Parametros Debug")]
    [SerializeField]
    private float distanciaChecagemPulo=1;
    [SerializeField]
    private GameObject meshPersonagem;
    private List<RaycastHit2D>raycastsPulo;

    [Header("Scriptable objects")]
    [SerializeField]
    private ControladorDeCena controldadorDeCenaPlayer;
    [SerializeField]
    private MapeadorDeBotoes mapeadorDeBotoes;
    private EstadoBasePlayer estadoPlayerAtual;
    
    [Header("Materiais Fisicos")]
    [SerializeField]
    private PhysicsMaterial2D materialFisicoParado;
    [SerializeField]
    private PhysicsMaterial2D materialFisicoAndando;

    [Header("Componentes fisicos")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private CapsuleCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        raycastsPulo=new List<RaycastHit2D>();
        rb=GetComponent<Rigidbody2D>();
        col=GetComponent<CapsuleCollider2D>();
        estadoPlayerAtual=new EstadoIdlePlayer();
        estadoPlayerAtual.IniciarEstadoPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.jogando)
        {
            estadoPlayerAtual.AtualizarEstado();
        }
        //Debug.Log(estadoPlayerAtual);
    }

    void FixedUpdate()
    {   if(controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.jogando)
        {
            estadoPlayerAtual.AtualizarEstadoFixado();
            if(estadoPlayerAtual is EstadoAtivoBasePlayer && !(estadoPlayerAtual is EstadoNoArBasePlayer))
            {
                rb.Cast(Vector2.down,raycastsPulo,distanciaChecagemPulo);
                if(raycastsPulo.Count!=0)
                {
                    foreach(RaycastHit2D r in raycastsPulo)
                    {
                        if(r.collider.tag!="Player")
                        {
                            raycastsPulo.Clear();
                            return;
                        }
                    }
                    TrocaEstadoPlayer(new EstadoPuloPlayer());
                }
                else
                {
                    TrocaEstadoPlayer(new EstadoPuloPlayer());
                }
                raycastsPulo.Clear();
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(col.bounds.center,new Vector3(col.bounds.center.x,col.bounds.min.y-distanciaChecagemPulo,col.bounds.min.z));
    }

    public void TrocaEstadoPlayer(EstadoBasePlayer novoEstado)
    {
        estadoPlayerAtual.FinalizarEstado();
        estadoPlayerAtual=novoEstado;
        estadoPlayerAtual.IniciarEstadoPlayer(this);
    }
    public void RodarPersonagem(bool olhandoParaDireita)
    {
        if(olhandoParaDireita)
        {
            this.olhandoParaDireita=true;
            meshPersonagem.transform.eulerAngles=new Vector3(0,90,0);
        }
        else
        {
            this.olhandoParaDireita=false;
            meshPersonagem.transform.eulerAngles=new Vector3(0,270,0);
        }
    }
    public PhysicsMaterial2D GetMaterialFisicoParado=> materialFisicoParado;
    public PhysicsMaterial2D GetMaterialFisicoAndando=>materialFisicoAndando;
    public MapeadorDeBotoes GetMapeadorDeBotoes=>mapeadorDeBotoes;
    public Rigidbody2D GetRigidbody2D=>rb;
    public CapsuleCollider2D GetCapsuleCollider2D=>col;
    public float GetVelocidadeDeMovimento=>velocidadeDeMovimento;
    public float GetForcaPulo=>forcaPulo;
    public bool GetOlhandoParaDireita=>olhandoParaDireita;
    public float GetPerdaDeMovimentoNoAr=>perdaDeMovimentoNoAr;
    public List<RaycastHit2D> GetRaycastsPulo=>raycastsPulo;
    public float GetDistanciaChecagemPulo=>distanciaChecagemPulo;
}
