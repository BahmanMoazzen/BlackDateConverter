using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class manages the IO of the converted data using PlayerPrefs
/// </summary>
public static class HistoryManager
{
    /// <summary>
    /// the key to save data on PlayerPrefs
    /// </summary>
    const string saveKey = "BlackDateConverterSave";
    /// <summary>
    /// the separator between each converted items
    /// </summary>
    const char itemSeparator = '*';
    /// <summary>
    /// all the items in a list
    /// </summary>
    static List<SavedDate> allDates = new List<SavedDate>();
    /// <summary>
    /// loads list from PlayerPrefs
    /// </summary>
    /// <returns>a list of SavedData</returns>
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
    /// <summary>
    /// saves a list into PlayerPrefs
    /// </summary>
    public static void SaveList()
    {
        string res = string.Empty;
        if (allDates.Count > 0)
        {
            foreach (SavedDate date in allDates)
            {
                res += date.ToString() + itemSeparator;
            }

            PlayerPrefs.SetString(saveKey, res.Substring(0, res.Length - 1));
        }
        else
        {
            PlayerPrefs.SetString(saveKey, res);
        }
    }
    /// <summary>
    /// Adds a converted date to the list
    /// </summary>
    /// <param name="iFrom">from date</param>
    /// <param name="iTo"> to date</param>
    /// <param name="iFromTitle">title of the from date</param>
    /// <param name="iToTitle">title of the to date</param>
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
    public static void AddItem(SavedDate iDateToSave)
    {
        LoadList();

        allDates.Add(iDateToSave);
        SaveList();
    }
    /// <summary>
    /// removes a converted date from list
    /// </summary>
    /// <param name="iDateToRemove">the date intended to be removed</param>
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
/// <summary>
/// the structure to save converted date
/// </summary>
public struct SavedDate
{
    /// <summary>
    /// the separator of from and to date
    /// </summary>
    const char fromToSeperator = '$';
    /// <summary>
    /// the separator from date and its title
    /// </summary>
    const char dateTitleSeperator = '#';
    /// <summary>
    /// from and to date
    /// </summary>
    public string FromDate, ToDate;
    /// <summary>
    /// from and to title
    /// </summary>
    public string FromTitle, ToTitle;
    /// <summary>
    /// constructor for generating the structure
    /// </summary>
    /// <param name="iFrom">from date</param>
    /// <param name="iTo">to date</param>
    /// <param name="iFromTitle">from title</param>
    /// <param name="iToTitle">to title</param>
    public SavedDate(string iFrom, string iTo, string iFromTitle, string iToTitle)
    {
        FromDate = iFrom;
        ToDate = iTo;
        FromTitle = iFromTitle;
        ToTitle = iToTitle;
    }
    /// <summary>
    /// the constructor for generating the structure from a string input
    /// </summary>
    /// <param name="iInput">the string in which all the from and to dates comparts together in a certain way</param>
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
    /// <summary>
    /// converts the separated from and to date in a single string ready to store in PlayerPrefs
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{FromDate}{dateTitleSeperator}{FromTitle}{fromToSeperator}{ToDate}{dateTitleSeperator}{ToTitle}";
    }


}
