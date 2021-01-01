using TMPro;
/// <summary>
/// Script base para texto UI.
/// </summary>
public abstract class TexViewUI : ViewUIWorld
{
    #region Variaveis protegidas 
    protected TextMeshProUGUI _text;
    #endregion
    #region Método UNITY
    public override void Awake()
    {
        base.Awake();
        _text = GetComponent<TextMeshProUGUI>();
    }
    #endregion
}
