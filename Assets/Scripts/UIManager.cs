using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] InputField _year, _month, _day;
    [SerializeField] Dropdown _fromDate, _toDate;
    [SerializeField] GameObject _listItemPrefab;
    [SerializeField] Transform _listTransform;
    Dictionary<string, string> _convertedList = new Dictionary<string, string>();
    [SerializeField] Validator[] _validators;
    private void Start()
    {
        _populateList();
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
            HistoryManager.AddItem($"{_validators[2].FieldValue}/{_validators[1].FieldValue}/{_validators[0].FieldValue}", "2025/01/01", _fromDate.captionText.text, _toDate.captionText.text);
            //_addItem("From" + Random.Range(1, 100), "Too" + Random.Range(1, 100),"greforian","Persian");
            _populateList();
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

            if (isValid)
            {
                Debug.Log("Validated");
            }
            else
            {
                Debug.Log("invalid");
            }
            return isValid;
        }
    }
    void _addItem(string iFrom, string iTo, string iFromTitle, string iToTitle)
    {
        _convertedList.Add(iFrom, iTo);
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
