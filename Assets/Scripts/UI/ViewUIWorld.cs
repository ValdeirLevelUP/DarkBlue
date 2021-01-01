 using UnityEngine; 

/// <summary>
/// Script base para todos UI world do game.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public abstract class ViewUIWorld : MonoBehaviour
{
    #region Variaveis privadas
    private CanvasGroup _minhaCanvasGroup;
    #endregion

    #region Métodos UNITY
    public virtual void Awake()
    {
        _minhaCanvasGroup = GetComponent<CanvasGroup>();
    }
    #endregion
    #region Método proprios
    /// <summary>
    /// Método para alterar visibilidade de UI no game
    /// </summary>
    /// <param name="visibilidade"> visibilidade verdade ou falsa</param>
    public void SetVisibilidade(bool visibilidade)
    {
        if (visibilidade)
        {
            _minhaCanvasGroup.alpha = 1;

            _minhaCanvasGroup.blocksRaycasts = true;

            _minhaCanvasGroup.interactable = true;
        }
        else
        {
            _minhaCanvasGroup.alpha = 0;

            _minhaCanvasGroup.blocksRaycasts = false;

            _minhaCanvasGroup.interactable = false;
        }
    }
    #endregion
}
