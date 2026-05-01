using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FuseMenu : MonoBehaviour
{
    public TextMeshProUGUI baseTowerDisplayName;
    public TextMeshProUGUI formulaPreviewText;
    public Button fuseExecuteButton;

    private string baseTowerName = "";
    private string selectedTowerName = "";

    public void SetupMenu(string name)
    {
        baseTowerName = name;
        baseTowerDisplayName.text = "Selected: " + name;
        selectedTowerName = ""; // Reset choice
        formulaPreviewText.text = "Pick a component...";
        fuseExecuteButton.interactable = false;
    }


    public void SelectComponent(string component)
    {
        selectedTowerName = component;
        formulaPreviewText.text = baseTowerName + " + " + component;
        fuseExecuteButton.interactable = true;
    }

    public void OnClickFuse()
    {
        GameMaster.instance.CombineTower(baseTowerName, selectedTowerName);
    }
}
