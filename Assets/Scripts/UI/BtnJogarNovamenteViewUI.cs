/// <summary>
/// Script para o botao jogar Novamente
/// </summary>
public class BtnJogarNovamenteViewUI : AbstractBtn
{
    public static event OnLoad executarAoCarregar;

    #region Método UNITY
    private void OnEnable()
    {
        GameManager.FimDoGame += () => SetVisibilidade(true);   
    }
    private void OnDisable()
    {
        GameManager.FimDoGame -= () => SetVisibilidade(true);
    }
    #endregion

    #region Métodos proprios
    public override void Executar()
    {
        base.Executar();
        if (_meuContadorDeTempo >= TEMPO_ATIVACAO)
        {
            SetVisibilidade(false);

            executarAoCarregar?.Invoke();
        }
    }
    #endregion
}
