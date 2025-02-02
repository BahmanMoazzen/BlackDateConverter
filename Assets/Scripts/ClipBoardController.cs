using UnityEngine;
/// <summary>
/// controls the copy of data on clipboard
/// </summary>
public class ClipBoardController : MonoBehaviour
{
    /// <summary>
    /// the value to be copied to the clipboard
    /// </summary>
    public string _Value;
    /// <summary>
    /// copies the _Value to the clipboard and shows message
    /// </summary>
    public void _CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = _Value;
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("Copied to clipboard",2);

    }
}
