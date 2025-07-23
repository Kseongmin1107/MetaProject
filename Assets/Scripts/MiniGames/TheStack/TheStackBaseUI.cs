using UnityEngine;

public abstract class TheStackBaseUI : MonoBehaviour
{
    protected TheStackUIManager uiManager;

    public virtual void Init(TheStackUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}