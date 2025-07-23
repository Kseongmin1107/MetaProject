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
        if (scoreText == null) Debug.LogError("���� �ؽ�Ʈ�� �ν����Ϳ� ������� �ʾҾ��! ������Ʈ: " + gameObject.name);
        if (comboText == null) Debug.LogError("�޺� �ؽ�Ʈ�� �ν����Ϳ� ������� �ʾҾ��! ������Ʈ: " + gameObject.name);
        if (maxComboText == null) Debug.LogError("�ִ� �޺� �ؽ�Ʈ�� �ν����Ϳ� ������� �ʾҾ��! ������Ʈ: " + gameObject.name);
    }

    public void SetUI(int score, int combo, int maxCombo)
    {
        if (scoreText != null) scoreText.text = score.ToString();
        if (comboText != null) comboText.text = combo.ToString();
        if (maxComboText != null) maxComboText.text = maxCombo.ToString();
    }
}