
/// <summary>
/// Script para exibir tempo restante do gamePlayer
/// </summary>
public class TempoViewUI : TexViewUI
{
    #region Método UNITY
    private void OnEnable()
    {
        GameManager.ExibirTempoRestante += MarcarTempo;
    }
    private void OnDisable()
    {
        GameManager.ExibirTempoRestante -= MarcarTempo;
    }
    #endregion
    #region Método proprios
    /// <summary>
    /// Método para atualizar UI tempo de gameplayer
    /// </summary>
    /// <param name="tempo">tempo restante</param>
    public void MarcarTempo(float tempo)
    {
        _text.text = string.Format("Tempo:{0:00}", tempo); 
    }
    #endregion
}
