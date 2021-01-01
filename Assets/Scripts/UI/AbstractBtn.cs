using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script base para botoes vr.
/// </summary>
public abstract class AbstractBtn : ViewUIWorld
{
    #region Variaveis protegidas

    protected const float TEMPO_ATIVACAO = 0.6f;

    protected float _meuContadorDeTempo;

    protected bool _pontoDentro;

    [SerializeField] protected Image _btnUILoad;

    #endregion

    #region Métodos UNITY
    public virtual void Update()
    {
        if (_pontoDentro)
        {
            if (_meuContadorDeTempo > TEMPO_ATIVACAO) return;

            Executar();
        }
        if (_btnUILoad != null)
        {
            _btnUILoad.fillAmount = _meuContadorDeTempo / TEMPO_ATIVACAO;
        }
    }
    #endregion

    #region Métodos proprios

    /// <summary>
    /// Método executado quando tempo ativo é alcansado.
    /// </summary>
    public virtual void Executar()
    {
        _meuContadorDeTempo += Time.deltaTime;
    }

    /// <summary>
    /// Método que ativa a contagem. Será executado quando o ponteiro estiver sobre o botao.
    /// </summary>
    public virtual void OnPointEnter()
    {
        _pontoDentro = true;
    }

    /// <summary>
    /// Método que desativa a contagem. Será executado quando o ponteiro sair do botao.
    /// </summary>
    public virtual void OnPointExit()
    {
        _pontoDentro = false;

        _meuContadorDeTempo = 0;

        _btnUILoad.fillAmount = 0;
    }
    #endregion

}
