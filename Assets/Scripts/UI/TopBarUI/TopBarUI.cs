using UnityEngine;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{
    [SerializeField] private Button BtnSetting;
    [SerializeField] private Button BtnCoin;
    [SerializeField] private Button BtnGem;
    [SerializeField] private Text TxtCoin;
    [SerializeField] private Text TxtGem;

    public void InitUI(int coin, int gem)
    {
        TxtCoin.text = coin.ToString();
        TxtGem.text = gem.ToString();
        BtnSetting.onClick.AddListener(() => { BtnSettingFunction(); });
        BtnCoin.onClick.AddListener(() => { BtnCoinFunction(); });
        BtnGem.onClick.AddListener(() => { BtnGemFunction(); });
    }

    private void BtnSettingFunction()
    {
    }

    private void BtnCoinFunction()
    {

    }

    private void BtnGemFunction()
    {

    }
}
