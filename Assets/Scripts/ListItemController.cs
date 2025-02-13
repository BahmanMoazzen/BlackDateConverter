using UnityEngine;
using UnityEngine.UI;
using UPersian.Utils;
/// <summary>
/// this class controlls the visibility of the list items and shows different part in different elements
/// </summary>
public class ListItemController : MonoBehaviour
{
    /// <summary>
    /// the text elements of the list item
    /// </summary>
    [SerializeField] Text _fromTitle, _fromText, _toTitle, _toText;
    /// <summary>
    /// clip board controller classes of the list item
    /// </summary>
    [SerializeField] ClipBoardController _fromClipBoard, _toClipBoard;
    /// <summary>
    /// a saved date to show in the item menu
    /// </summary>
    SavedDate _date;

    /// <summary>
    /// shows different part of the item on the UI
    /// </summary>
    /// <param name="iFrom">from date </param>
    /// <param name="iTo">to date</param>
    /// <param name="iFromTitle">from date title</param>
    /// <param name="iToTitle">to date title</param>
    public void _LoadData(string iFrom, string iTo, string iFromTitle, string iToTitle)
    {
        _fromClipBoard._Value = _fromText.text = iFrom;
        _toClipBoard._Value = _toText.text = iTo;
        _fromTitle.text = iFromTitle;
        _toTitle.text = iToTitle;


        _date = new SavedDate(iFrom, iTo, iFromTitle, iToTitle);
    }
    /// <summary>
    /// loads list item based on an input saved date
    /// </summary>
    /// <param name="iDate">the date in SavedDate format</param>
    public void _LoadData(SavedDate iDate)
    {
        _fromClipBoard._Value = _fromText.text = iDate.FromDate;
        _toClipBoard._Value = _toText.text = iDate.ToDate;
        _fromTitle.text = iDate.FromTitle.RtlFix();
        _toTitle.text = iDate.ToTitle.RtlFix();
        _fromText.font = _fromTitle.font = _toText.font = _toTitle.font = LanguageManagerInfo._Instance.CurrentFont;

        _date = iDate;
    }
    /// <summary>
    /// deletes the item list from UI and PlayerPrefs
    /// </summary>
    public void DeleteRecord()
    {
        HistoryManager.RemoveItem(_date);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("Item Removed", 2);
        Destroy(gameObject);
    }
}
