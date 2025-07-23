using System.Buffers.Text;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TheStackHomeUI : TheStackBaseUI
{
    Button startButton;
    Button exitButton;

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(TheStackUIManager uiManager)
    {
        base.Init(uiManager);

        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }


}