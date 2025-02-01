using UnityEngine;

public class ClipBoardController : MonoBehaviour
{
    public string _Value;
    public void _CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = _Value;
    }
}
