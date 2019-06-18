using GameLab.UIFramework;

public class GameUIController : MonoSingleton<GameUIController>
{
    private WindowData HomeUIWindowData = new WindowData()
    {
        Path = "",
        UIType = UIType.Window,
        UIData = new HomeUIData()
        {

        }
    };
    private WindowData PlayUIData = new WindowData()
    {
        Path = "",
        UIType = UIType.Window,
        UIData = new PlayUIData()
        {

        }
    };
    private WindowData QuestionUIData = new WindowData()
    {
        Path = "",
        UIType = UIType.Window,
        UIData = new QuestionUIData()
        {

        }
    };

    public void ShowUI(UIWindowType windowType)
    {
        switch (windowType)
        {
            case UIWindowType.HomeUI:
                HiUIManager.Instance.Show(HomeUIWindowData);
                break;
            case UIWindowType.PlayUI:
                HiUIManager.Instance.Show(PlayUIData);
                break;
            case UIWindowType.QuestionUI:
                HiUIManager.Instance.Show(QuestionUIData);
                break;
            default:
                break;
        }
    }
}
