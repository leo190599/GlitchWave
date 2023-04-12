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
    private bool olhandoParaEsquerda=false;

    [Header("Parametros Debug")]
    [SerializeField]
    private float distanciaChecagemPulo=1;
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
        raycastsPulo.Clear();
    }

    void FixedUpdate()
    {   if(controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.jogando)
        {
            estadoPlayerAtual.AtualizarEstadoFixado();
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

    public PhysicsMaterial2D GetMaterialFisicoParado=> materialFisicoParado;
    public PhysicsMaterial2D GetMaterialFisicoAndando=>materialFisicoAndando;
    public MapeadorDeBotoes GetMapeadorDeBotoes=>mapeadorDeBotoes;
    public Rigidbody2D GetRigidbody2D=>rb;
    public CapsuleCollider2D GetCapsuleCollider2D=>col;
    public float GetVelocidadeDeMovimento=>velocidadeDeMovimento;
    public float GetForcaPulo=>forcaPulo;
    public bool GetOlhandoParaEsquerda=>olhandoParaEsquerda;

    public List<RaycastHit2D> GetRaycastsPulo=>raycastsPulo;
    public float GetDistanciaChecagemPulo=>distanciaChecagemPulo;
}
