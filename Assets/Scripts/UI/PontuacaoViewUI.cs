/// <summary>
/// Script para exibir a pontuacao
/// </summary>
public class PontuacaoViewUI : TexViewUI
{
    #region Método UNITY
    private void OnEnable()
    {
        GameManager.ExibirPontos += MarcarPontos;
    }
    private void OnDisable()
    {
        GameManager.ExibirPontos -= MarcarPontos;
    }
    #endregion

    #region Métodos proprios
    /// <summary>
    /// Método para atualizar UI de pontuacao.
    /// </summary>
    /// <param name="pontuacao">pontuacao atual</param>
    public void MarcarPontos(int pontuacao)
    {
        _text.text = string.Format("Pontos:{0:00}", pontuacao);
    }
    #endregion
}
