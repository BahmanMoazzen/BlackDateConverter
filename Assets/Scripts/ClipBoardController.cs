using UnityEngine;

public class ClipBoardController : MonoBehaviour
{
    public string _Value;
    public void _CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = _Value;
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("Copied to clipboard",2);

    }
}
