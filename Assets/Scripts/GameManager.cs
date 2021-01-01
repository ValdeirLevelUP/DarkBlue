using System;
using System.Collections;
using UnityEngine;

public delegate void ContagemRegressiva(float tempoRestante);

public delegate void ExibirPontos(int pontuacaoAtual); 

public delegate void ExibirTempo(float tempoRestante);

public delegate void EndTime();

/// <summary>
/// Script responsavel pelo gameplayer do game.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Variáveis privadas

    private Medusa[] _medusasEmJogo;

    private int _score;

    private float tempoRestante;

    private IEnumerator _enfurece;

    [SerializeField] private GameObject _modeloMedusa;

    [SerializeField] private Transform _jogador;

    [SerializeField] private ConfigGame _config;

    #endregion

    #region Eventos
    public static event ContagemRegressiva MarcarContagemRegressiva;

    public static event ExibirPontos ExibirPontos;

    public static event ExibirTempo ExibirTempoRestante;

    public static event EndTime FimDoGame;
    #endregion

    #region Métodos UNITY
    private void Awake()
    {
        _enfurece = EnfurecerMedusas();
    }
    private void OnEnable()
    {
        MenuInicial.Iniciar += () => StartCoroutine(ContagemRegressiva(_config.ContagemRegressiva, () => { InstanciarMedusas(_config.QuantidadeMedusas);
            StartCoroutine(_enfurece);
        }));
        Medusa.Pontuar += Pontuar;
    }
    private void OnDisable()
    {
        MenuInicial.Iniciar -= () => StartCoroutine(ContagemRegressiva(_config.ContagemRegressiva, () => {
            InstanciarMedusas(_config.QuantidadeMedusas);
            StartCoroutine(_enfurece);
        }));
        Medusa.Pontuar -= Pontuar;
    }
    #endregion

    #region Métodos Próprios
    /// <summary>
    /// Método que adiciona pontuacao.
    /// </summary>
    /// <param name="valor">Valor adicionado</param>
    public void Pontuar(int valor)
    {
        _score += valor;

        ExibirPontos?.Invoke(_score);
    }

    /// <summary>
    /// Método para instanciar as medusas no cenario.
    /// </summary>
    /// <param name="quantidade">Quantidade de mesusas</param>
    public void InstanciarMedusas(int quantidade)
    {
        if(_medusasEmJogo != null && _medusasEmJogo.Length > 0)
        {
            foreach (Medusa medusa in _medusasEmJogo)
            {
                Destroy(medusa.gameObject);
            }
        }
        _medusasEmJogo = new Medusa[quantidade];

        for (int i = 0; i < quantidade; i++)
        {
            Medusa medusa = Instantiate(_modeloMedusa).GetComponent<Medusa>();

            _medusasEmJogo[i] = medusa;
        }

        PosicionarMedusas();
    }
    /// <summary>
    /// Método para posicionar medusas ao retorno do jogador.
    /// </summary>
    private void PosicionarMedusas()
    {
        for (int i = 0, cont = 0; i < _medusasEmJogo.Length; cont++)
        {
            for (int j = 0; j < 7; j++, i++)
            {

                Quaternion lookRotation = Quaternion.Euler(new Vector2(0, _config.DistanciaHorizontalEntreMedusas * j));

                Vector3 lookDirection = lookRotation * Vector3.left - Vector3.up * _config.DistanciaVerticalEntreMedusas * cont;

                Vector3 lookPosition = (_jogador.transform.position - Vector3.up * _config.DistanciaDoSolo) - lookDirection * _config.DistanciaDoJogador;

                _medusasEmJogo[i].transform.SetPositionAndRotation(lookPosition, lookRotation);
            }
        }
    }
    #endregion

    #region Corotinas
    /// <summary>
    /// Coroutina que iniciar contagem regressiva para inicio do game.
    /// </summary>
    /// <param name="tempoTotal">Tempo para iniciar o jogo</param>
    /// <param name="callBack">Funcao executada após a contagem</param>
    /// <returns></returns>
    IEnumerator ContagemRegressiva(float tempoTotal, Action callBack = null)
    {
        float tempoRestante = tempoTotal;

        while(tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime;

            MarcarContagemRegressiva?.Invoke(tempoRestante);

            yield return null;
        } 
        callBack?.Invoke();

        StartCoroutine(ContagemParaFimDoGamePlayer(_config.TempoDePartida));
    }

    /// <summary>
    /// Coroutina que inicia contagem para o final do game.
    /// </summary>
    /// <param name="tempoTotal">Tempo total da partida</param>
    /// <param name="callBack"> funcoes executada ao finalizar o tempo</param>
    /// <returns></returns>
    IEnumerator ContagemParaFimDoGamePlayer(float tempoTotal, Action callBack = null)
    {
        tempoRestante = tempoTotal;

        _score = 0;

        ExibirPontos?.Invoke(_score);

        while (tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime;

            ExibirTempoRestante?.Invoke(tempoRestante);

            yield return null;
        }
        callBack?.Invoke();
        StopCoroutine(_enfurece);
        foreach (Medusa medusa in _medusasEmJogo)
        {
            medusa.MudarCorMedusa(Color.yellow);
        }
        FimDoGame?.Invoke();
    }

    /// <summary>
    /// Coroutine que enferece as medusas.
    /// </summary>
    /// <returns></returns>
    IEnumerator EnfurecerMedusas()
    { 
        do
        {  
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, _config.TempoEntreDoisEnfurecimentos));

            int contador = 0;

            foreach (Medusa medusa in _medusasEmJogo)
            {
                if (medusa.Status == StatusMedusa.AGRESSIVA)
                {
                    contador++;
                }
            }
            if (contador < _config.NMaxDeMedusasSimultanementeFuriosas)
            {
                int random = (int)UnityEngine.Random.Range(0, _medusasEmJogo.Length);

                if (_medusasEmJogo[random].Status == StatusMedusa.PASSIVA)
                {
                    _medusasEmJogo[random].MudarStatus();
                }
                else
                {
                    random++;
                    if (_medusasEmJogo[random].Status == StatusMedusa.PASSIVA)
                    {
                        _medusasEmJogo[random].MudarStatus();
                    }
                }

            }

            yield return null;
        }
        while (tempoRestante > 0);
    }
    #endregion

}
