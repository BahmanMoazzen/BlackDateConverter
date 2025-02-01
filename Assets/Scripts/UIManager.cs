using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] InputField _year, _month, _day;
    [SerializeField] Dropdown _fromDate, _toDate;
    [SerializeField] GameObject _listItemPrefab;
    [SerializeField] Transform _listTransform;
    [SerializeField] Validator[] _validators;
    private void Start()
    {
        _populateList();
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("Helen Game Factory Presents.");
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("Black Date Converter.");
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
            DateTime dt = new DateTime();
            string convertedDate = string.Empty;
            switch ((CalendarTypes)_fromDate.value)
            {
                case CalendarTypes.Perian:
                    PersianCalendar pCal = new PersianCalendar();
                    dt = new DateTime(_validators[(int)DateElementOrder.Year].FieldValue
                        , _validators[(int)DateElementOrder.Month].FieldValue
                        , _validators[(int)DateElementOrder.Day].FieldValue, pCal);

                    break;
                case CalendarTypes.Gregorian:
                    dt = new DateTime(_validators[(int)DateElementOrder.Year].FieldValue
                        , _validators[(int)DateElementOrder.Month].FieldValue
                        , _validators[(int)DateElementOrder.Day].FieldValue);
                    break;
                case CalendarTypes.Hijri:
                    HijriCalendar hiCal = new HijriCalendar();
                    dt = new DateTime(_validators[(int)DateElementOrder.Year].FieldValue
                        , _validators[(int)DateElementOrder.Month].FieldValue
                        , _validators[(int)DateElementOrder.Day].FieldValue, hiCal);
                    break;
            }

            switch ((CalendarTypes)_toDate.value)
            {
                case CalendarTypes.Perian:
                    PersianCalendar pCal = new PersianCalendar();
                    convertedDate = $"{pCal.GetYear(dt)}/{pCal.GetMonth(dt)}/{pCal.GetDayOfMonth(dt)}";
                    break;
                case CalendarTypes.Gregorian:
                    convertedDate = $"{dt.Year}/{dt.Month}/{dt.Day}";
                    break;
                case CalendarTypes.Hijri:
                    HijriCalendar hiCal = new HijriCalendar();
                    convertedDate = $"{hiCal.GetYear(dt)}/{hiCal.GetMonth(dt)}/{hiCal.GetDayOfMonth(dt)}";
                    break;
            }

            HistoryManager.AddItem($"{_validators[2].FieldValue}/{_validators[1].FieldValue}/{_validators[0].FieldValue}", convertedDate, _fromDate.captionText.text, _toDate.captionText.text);

            _populateList();

            BAHMANMessageBoxManager._INSTANCE._ShowMessage("Date Converted.",2);
        }
        else
        {
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("Date is not valid!");
        }

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


    void _populateList()
    {
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
}

public enum DateElementOrder { Day, Month, Year }
public enum CalendarTypes { Perian, Gregorian, Hijri }
