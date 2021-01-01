using UnityEngine;
/// <summary>
/// Script que armazena valores de gameplayer.
/// </summary>
[CreateAssetMenu(menuName = "Prototipo/config")]
public class ConfigGame : ScriptableObject
{

    #region Variaveis públicas
    public float ContagemRegressiva { get => _contagemRegressiva; }
    public int QuantidadeMedusas { get => _QuantidadeMedusas; }
    public float DistanciaHorizontalEntreMedusas { get => _distanciaHorizontalEntreMedusas; }
    public float DistanciaVerticalEntreMedusas { get => _distanciaVerticalEntreMedusas; }
    public float DistanciaDoSolo { get => _distanciaDoSolo; }
    public float DistanciaDoJogador { get => _distanciaDoJogador; }
    public float TempoDePartida { get => _tempoDePartida; }
    public int NMaxDeMedusasSimultanementeFuriosas { get => _nMaxDeMedusasSimultanementeFuriosas; }
    public float TempoEntreDoisEnfurecimentos { get => _tempoEntreDoisEnfurecimentos; }
    #endregion

    #region Variaveis privadas
    [SerializeField] private float _contagemRegressiva;

    [SerializeField] private int _QuantidadeMedusas;

    [SerializeField] private float _distanciaHorizontalEntreMedusas;

    [SerializeField] private float _distanciaVerticalEntreMedusas;

    [SerializeField] private float _distanciaDoSolo;

    [SerializeField] private float _distanciaDoJogador;

    [SerializeField] private float _tempoDePartida;

    [SerializeField] private int _nMaxDeMedusasSimultanementeFuriosas;

    [SerializeField] private float _tempoEntreDoisEnfurecimentos;
    #endregion
}
