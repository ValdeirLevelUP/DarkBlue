public delegate void OnLoad();

/// <summary>
/// Script do botao jogar.
/// </summary>
public class BntJogarViewUI : AbstractBtn
{
    public static event OnLoad executarAoCarregar;
    public override void Executar()
    {
        base.Executar();
        if(_meuContadorDeTempo >= TEMPO_ATIVACAO)
        { 
            executarAoCarregar?.Invoke();
        }
    }

}
