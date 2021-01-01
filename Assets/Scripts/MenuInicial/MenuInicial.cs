using UnityEngine;

public delegate void OnStartGamePlayer();
/// <summary>
/// Script para gerenciar o menu inicial.
/// </summary>
public class MenuInicial : MonoBehaviour
{
    #region Variaveis privadas

    [SerializeField] private ViewUIWorld _logo;

    [SerializeField] private ViewUIWorld _btnStart;

    [SerializeField] private ViewUIWorld _tempo;

    [SerializeField] private ViewUIWorld _pontuacao;

    [SerializeField] private ViewUIWorld _tentarNovamente;

    [SerializeField] private ViewUIWorld _contagemRegressiva;

    #endregion

    #region Variaveis públicas

    public static event OnStartGamePlayer Iniciar;
    #endregion

    #region Métodos UNITY

    private void OnEnable()
    {
        BntJogarViewUI.executarAoCarregar += ComecarJogo;

        BtnJogarNovamenteViewUI.executarAoCarregar += ComecarJogo;
    }

    private void OnDisable()
    {
        BntJogarViewUI.executarAoCarregar -= ComecarJogo;

        BtnJogarNovamenteViewUI.executarAoCarregar -= ComecarJogo; 
    }

    #endregion

    #region Méotodos próprios
    /// <summary>
    /// Método que inicia o gameplayer. Funcao executada pelo botao jogar.
    /// </summary>
    public void ComecarJogo()
    {
        _logo.SetVisibilidade(false);

        _btnStart.SetVisibilidade(false);

        _tempo.SetVisibilidade(true);

        _pontuacao.SetVisibilidade(true);

        _tentarNovamente.SetVisibilidade(false);

        _contagemRegressiva.SetVisibilidade(true);

        Iniciar?.Invoke();
    }
    #endregion
}
