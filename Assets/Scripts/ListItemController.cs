using UnityEngine;
using UnityEngine.UI;

public class ListItemController : MonoBehaviour
{
    [SerializeField] Text _fromTitle, _fromText, _toTitle, _toText;
    [SerializeField] ClipBoardController _fromClipBoard, _toClipBoard;
    SavedDate _date;
    public void _LoadData(string iFrom, string iTo, string iFromTitle, string iToTitle)
    {
        _fromClipBoard._Value = _fromText.text = iFrom;
        _toClipBoard._Value = _toText.text = iTo;
        _fromTitle.text = iFromTitle;
        _toTitle.text = iToTitle;


        _date = new SavedDate(iFrom, iTo, iFromTitle, iToTitle);
    }
    public void _LoadData(SavedDate iDate)
    {
        _fromClipBoard._Value = _fromText.text = iDate.FromDate;
        _toClipBoard._Value = _toText.text = iDate.ToDate;
        _fromTitle.text = iDate.FromTitle;
        _toTitle.text = iDate.ToTitle;


        _date = iDate;
    }
    public void DeleteRecord()
    {
        HistoryManager.RemoveItem(_date);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("Item Removed",2);
        Destroy(gameObject);
    }
}
