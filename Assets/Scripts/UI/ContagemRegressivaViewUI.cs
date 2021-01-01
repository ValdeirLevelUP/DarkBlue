/// <summary>
/// Script para exibir a contagem regressiva
/// </summary>
public class ContagemRegressivaViewUI : TexViewUI
{
    #region Métodos UNITY
    private void OnEnable()
    {
        GameManager.MarcarContagemRegressiva += MarcarTempo;
    }
    private void OnDisable()
    {
        GameManager.MarcarContagemRegressiva -= MarcarTempo;
    }
    #endregion

    #region Métodos proprios

    /// <summary>
    /// Método que atualizar UI de tempo de inicio.
    /// </summary>
    /// <param name="tempo">Tempo restante para iniciar a partida</param>
    public void MarcarTempo(float tempo)
    {
        _text.text = string.Format("{0:0}", tempo);
        if (tempo <= 0)
        {
            SetVisibilidade(false);
        }
    }
    #endregion
}
