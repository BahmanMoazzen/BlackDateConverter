using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public static class HistoryManager
{
    const string saveKey = "BlackDateConverterSave";
    const char itemSeparator = '*';
    static List<SavedDate> allDates = new List<SavedDate>();
    public static List<SavedDate> LoadList()
    {
        allDates.Clear();
        string savedString = PlayerPrefs.GetString(saveKey, string.Empty);
        if (savedString != string.Empty)
        {
            string[] items = savedString.Split(itemSeparator);
            if (items.Length > 0)
            {
                foreach (string item in items)
                {
                    allDates.Add(new SavedDate(item));
                }
            }
        }
        return allDates;
    }
    public static void SaveList()
    {
        string res = string.Empty;
        foreach (SavedDate date in allDates)
        {
            res += date.ToString() + itemSeparator;
        }

        PlayerPrefs.SetString(saveKey, res.Substring(0, res.Length - 1));
    }
    public static void AddItem(string iFrom, string iTo, string iFromTitle, string iToTitle)
    {
        LoadList();
        SavedDate itemToAdd = new SavedDate(iFrom, iTo, iFromTitle, iToTitle);
        foreach (SavedDate date in allDates)
        {
            if (date.Equals(itemToAdd)) return;
        }
        allDates.Add(itemToAdd);
        SaveList();
        Debug.Log(itemToAdd.ToString());
    }
    public static void RemoveItem(SavedDate iDateToRemove)
    {
        for (int i = 0; i < allDates.Count; i++)
        {
            if (allDates[i].ToString().Equals(iDateToRemove.ToString()))
            {
                allDates.RemoveAt(i);
                SaveList();
                return;
            }
        }



    }
}

public struct SavedDate
{
    const char fromToSeperator = '$';
    const char dateTitleSeperator = '#';
    public string FromDate, ToDate;
    public string FromTitle, ToTitle;
    public SavedDate(string iFrom, string iTo, string iFromTitle, string iToTitle)
    {
        FromDate = iFrom;
        ToDate = iTo;
        FromTitle = iFromTitle;
        ToTitle = iToTitle;
    }
    public SavedDate(string iInput)
    {
        string[] fromTo = iInput.Split(fromToSeperator);
        string[] fromDate = fromTo[0].Split(dateTitleSeperator);
        string[] toDate = fromTo[1].Split(dateTitleSeperator);
        FromDate = fromDate[0];
        FromTitle = fromDate[1];
        ToDate = toDate[0];
        ToTitle = toDate[1];

    }
    public override string ToString()
    {
        return $"{FromDate}{dateTitleSeperator}{FromTitle}{fromToSeperator}{ToDate}{dateTitleSeperator}{ToTitle}";
    }


}
