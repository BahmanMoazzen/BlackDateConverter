using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this class is the general manager for the app UI
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// the input fields for the desired date
    /// </summary>
    [SerializeField] InputField _year, _month, _day;
    /// <summary>
    /// the from and to date type
    /// </summary>
    [SerializeField] Dropdown _fromDate, _toDate;
    [SerializeField] Dropdown _fromDateType, _toDateType;
    /// <summary>
    /// the list item prefab to populate into the list
    /// </summary>
    [SerializeField] GameObject _listItemPrefab;
    /// <summary>
    /// the base root to instantiate the list item prefab
    /// </summary>
    [SerializeField] Transform _listTransform;
    /// <summary>
    /// the form validators of the date fields
    /// </summary>
    [SerializeField] Validator[] _validators;

    private IEnumerator Start()
    {
        yield return null;
        StartCoroutine(_populateList());
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("Helen Game Factory Presents");
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("Black Date Converter");
    }
    public void _ResetValidators()
    {
        foreach (var validator in _validators)
        {
            validator._Reset();
        }
    }
    /// <summary>
    /// converts the dates
    /// </summary>
    public void _ConvertDate()
    {
        if (isFormValidate)
        {

            DateInInteger dii = new DateInInteger();
            dii.Year = _validators[(int)DateElementOrder.Year].FieldValue;
            dii.Month = _validators[(int)DateElementOrder.Month].FieldValue;
            dii.Day = _validators[(int)DateElementOrder.Day].FieldValue;

            BAHMANDateConverter dc = new BAHMANDateConverter(dii, (CalendarTypes)_fromDate.value, (CalendarTypes)_toDate.value);
            dc._CurrentFromFormat = _fromDateType.value;
            dc._CurrentToDateFormat = _toDateType.value;
            HistoryManager.AddItem(dc._Convert());
            StartCoroutine(_populateList());

            BAHMANMessageBoxManager._INSTANCE._ShowMessage("Date Converted", 2);
        }
        else
        {
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("Date is not valid", 2);
        }

    }
    public void _CloseButtonClicked()
    {
        BAHMANBackButtonManager._Instance._ShowMenu();
    }
    /// <summary>
    /// Checks if all the form validators are validated
    /// </summary>
    bool isFormValidate
    {
        get
        {
            bool isValid = true;
            foreach (var validator in _validators)
            {
                if (validator._Validate())
                {
                    isValid &= true;
                }
                else
                {
                    isValid &= false;
                }
            }

            return isValid;
        }
    }
    /// <summary>
    /// uses HistoryManager to populate the converted dates
    /// </summary>
    /// <returns></returns>
    IEnumerator _populateList()
    {
        yield return null;
        for (int i = 0; i < _listTransform.childCount; i++)
        {
            Destroy(_listTransform.GetChild(i).gameObject);

        }
        List<SavedDate> list = HistoryManager.LoadList();
        foreach (var itm in list)
        {
            Instantiate(_listItemPrefab, _listTransform).GetComponent<ListItemController>()._LoadData(itm);
        }
    }
    /// <summary>
    /// share the app
    /// </summary>
    public void _Share()
    {
        BAHMANPublicRelation._Instance._ShareClicked();
    }

    /// <summary>
    /// rate us the app
    /// </summary>
    public void _RateUs()
    {
        BAHMANPublicRelation._Instance._RateClicked();
    }
}

