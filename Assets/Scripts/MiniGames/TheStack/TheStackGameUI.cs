using System.Buffers.Text;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TheStackGameUI :TheStackBaseUI
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI maxComboText;

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(TheStackUIManager uiManager)
    {
        base.Init(uiManager);

        //scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        //comboText = transform.Find("ComboText").GetComponent<TextMeshProUGUI>();
        //maxComboText = transform.Find("MaxComboText").GetComponent<TextMeshProUGUI>();
        if (scoreText == null) Debug.LogError("점수 텍스트가 인스펙터에 연결되지 않았어요! 오브젝트: " + gameObject.name);
        if (comboText == null) Debug.LogError("콤보 텍스트가 인스펙터에 연결되지 않았어요! 오브젝트: " + gameObject.name);
        if (maxComboText == null) Debug.LogError("최대 콤보 텍스트가 인스펙터에 연결되지 않았어요! 오브젝트: " + gameObject.name);
    }

    public void SetUI(int score, int combo, int maxCombo)
    {
        if (scoreText != null) scoreText.text = score.ToString();
        if (comboText != null) comboText.text = combo.ToString();
        if (maxComboText != null) maxComboText.text = maxCombo.ToString();
    }
}