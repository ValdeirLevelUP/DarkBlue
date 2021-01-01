using System.Collections;
 using UnityEngine;

public delegate void OnScore(int valor);
/// <summary>
/// Script para controle dos componentes da medusa.
/// </summary>
public class Medusa : MonoBehaviour
{
    #region Variaveis privadas

    private Color _corInternaMedusa; 

    private IEnumerator _coroutinaMudarCor;

    private StatusMedusa _meuStatus = StatusMedusa.PASSIVA;

    [SerializeField] private Light _minhaLuz;

    [SerializeField] private MeshRenderer _materialInternoMedusa;

    [SerializeField] private SkinnedMeshRenderer _materialExternoMedusa; 

    [SerializeField] private AudioSource _meuAudio;
    #endregion

    #region Variaveis publicas
    public StatusMedusa Status { get => _meuStatus; }

    public static event OnScore Pontuar;
    #endregion

    #region Métodos UNITY
    private void Awake()
    {  
        _corInternaMedusa = _materialInternoMedusa.material.color;

        _minhaLuz.color = _corInternaMedusa; 
    }
    private void Start()
    {
        MudarCorMedusa();
    }
    #endregion

    #region Métodos próprios

    /// <summary>
    /// Funcao responsavel por acionar efeito de mudanca de cor.
    /// </summary>
    public void MudarStatus()
    {
        if(_meuStatus == StatusMedusa.AGRESSIVA)
        {
            _meuStatus = StatusMedusa.PASSIVA;
        }
        else
        {
            _meuStatus = StatusMedusa.AGRESSIVA;
        }
        _meuAudio.Play();

        MudarCorMedusa();
    }
    public void MudarStatus(StatusMedusa status)
    {
        _meuStatus = status;

        MudarCorMedusa();
    }

    /// <summary>
    /// Funcao executada quando mouse está sobre uma medusa enfurecida.
    /// </summary>
    public void MudarStatusComPonteiro()
    {
        if (_meuStatus == StatusMedusa.AGRESSIVA)
        {
            _meuStatus = StatusMedusa.PASSIVA; 

            MarcarPonto();
        } 
        MudarCorMedusa();
    }

    /// <summary>
    /// Funcao responsavel por acionar evento de pontuacao.
    /// </summary>
    private void MarcarPonto()
    {
        Pontuar?.Invoke(1);
    }

    /// <summary>
    /// Funcao para alterar a cor da medusa de forma assincrona.
    /// </summary>
    public void MudarCorMedusa()
    {
        if(_coroutinaMudarCor != null)
        { 
            StopCoroutine(_coroutinaMudarCor);
        } 

        if(_meuStatus == StatusMedusa.PASSIVA)
        {
            _coroutinaMudarCor = MudarCor(Color.blue);

            StartCoroutine(_coroutinaMudarCor);
        }
        else
        {
            _coroutinaMudarCor = MudarCor(Color.red);

            StartCoroutine(_coroutinaMudarCor);
        }
    }

    /// <summary>
    /// Sobrecarga de funcao para alterar cor da medusa.
    /// </summary>
    /// <param name="cor"></param>
    public void MudarCorMedusa(Color cor)
    {
        _coroutinaMudarCor = MudarCor(cor);

        StartCoroutine(_coroutinaMudarCor);
    }

    /// <summary>
    /// IEnumerator para alterar cores da medusa 
    /// </summary>
    /// <param name="novaCor"> cor final para a medusa</param>
    /// <returns></returns>
    IEnumerator MudarCor(Color novaCor)
    {
        float timeElapsed = 0;

        float startTime = Time.time;

        while (_materialInternoMedusa.material.color != novaCor)
        {
            timeElapsed += Time.deltaTime/3; 

            Color corAtual = _materialInternoMedusa.material.color;

            corAtual = Color.Lerp(corAtual, novaCor, timeElapsed);

            _materialInternoMedusa.material.color = corAtual;

            _materialExternoMedusa.material.SetColor("_EmissionColor", corAtual);

            _minhaLuz.color = corAtual;

            yield return null;
        }
        _coroutinaMudarCor = null;
    }
    #endregion
}

/// <summary>
/// Enum para marcar status da medusa
/// </summary>
public enum StatusMedusa
{
    AGRESSIVA,
    PASSIVA
}
