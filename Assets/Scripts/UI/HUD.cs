using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI matchesCountTxt;

    public void SetMacthesCountText(int numberOfMatches)
    {
        matchesCountTxt.text = numberOfMatches.ToString();
    }
}
