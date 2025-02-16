
using System.Globalization;
using System;


public class BAHMANDateConverter
{
    CalendarTypes _fromDateType, _toDateType;
    string[] _fromDateFormat = { "{0}/{1}/{2}", "{2}/{1}/{0}", "{2} {3} {0}" };
    string[] _toDateFormat = { "{0}/{1}/{2}", "{2}/{1}/{0}", "{2} {3} {0}" };
    string[] _dateFormatNames = { "Persian", "Gregorian", "Hijri" };
    string[,] _monthNames = {
        {"فروردین","اردیبهشت","خرداد","تیر","مرداد","شهریور","مهر","آبان","آذر","دی","بهمن","اسفند"}
        ,{"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November","December"}
        ,{"محرم", "صفر", "ربیع الاوّل", "ربیع الثّانی", "جمادی الاوّل", "جمادی الثّانی", "رجب", "شعبان", "رمضان", "شوّال","ذوالقعده", "ذوالحجّه"}
        };
    public int _CurrentFromFormat { get; set; }
    public int _CurrentToDateFormat { get; set; }
    DateInInteger _dateToConvert;

    /// <summary>
    /// the constructor for the class
    /// </summary>
    /// <param name="iDate">the date in integer to convert to</param>
    /// <param name="iFromType">the calendar in which the date is presented</param>
    /// <param name="iToType">the calendar intended to convert to</param>
    public BAHMANDateConverter(DateInInteger iDate, CalendarTypes iFromType, CalendarTypes iToType)
    {
        _fromDateType = iFromType;
        _toDateType = iToType;
        _dateToConvert = iDate;
    }
    /// <summary>
    /// converts and returns the date
    /// </summary>
    /// <returns>a converted date in SavedDate format</returns>
    public SavedDate _Convert()
    {
        SavedDate converted = new SavedDate();
        converted.FromTitle = BAHMANLanguageManager._Instance._Translate(_dateFormatNames[(int)_fromDateType]);
        converted.ToTitle = BAHMANLanguageManager._Instance._Translate(_dateFormatNames[(int)_toDateType]);


        converted.FromDate = string.Format(
            _fromDateFormat[_CurrentFromFormat]
            , _dateToConvert.Year.ToString()
            , _zeroInserter(_dateToConvert.Month, 2)
            , _zeroInserter(_dateToConvert.Day, 2)
            , _monthNames[(int)_fromDateType, _dateToConvert.Month - 1]
            );


        DateTime dt = new DateTime();
        string convertedDate = string.Empty;



        switch (_fromDateType)
        {
            case CalendarTypes.Persian:

                PersianCalendar pCal = new PersianCalendar();
                dt = new DateTime(_dateToConvert.Year
                    , _dateToConvert.Month
                    , _dateToConvert.Day
                    , pCal);

                break;
            case CalendarTypes.Gregorian:
                dt = new DateTime(_dateToConvert.Year
                    , _dateToConvert.Month
                    , _dateToConvert.Day);
                break;
            case CalendarTypes.Hijri:
                HijriCalendar hiCal = new HijriCalendar();
                dt = new DateTime(_dateToConvert.Year
                    , _dateToConvert.Month
                    , _dateToConvert.Day
                    , hiCal);
                break;
        }

        switch (_toDateType)
        {
            case CalendarTypes.Persian:
                PersianCalendar pCal = new PersianCalendar();
                convertedDate = string.Format(
                    _toDateFormat[_CurrentToDateFormat]
                    , pCal.GetYear(dt)
                    , _zeroInserter(pCal.GetMonth(dt), 2)
                    , _zeroInserter(pCal.GetDayOfMonth(dt), 2)
                    , _monthNames[(int)CalendarTypes.Hijri, pCal.GetMonth(dt) - 1]
                    );
                break;
            case CalendarTypes.Gregorian:
                convertedDate = string.Format(
                    _toDateFormat[_CurrentToDateFormat]
                    , dt.Year
                    , _zeroInserter(dt.Month, 2)
                    , _zeroInserter(dt.Day, 2)
                    , _monthNames[(int)CalendarTypes.Gregorian, dt.Month - 1]
                    );

                break;
            case CalendarTypes.Hijri:
                HijriCalendar hiCal = new HijriCalendar();
                convertedDate =
                    convertedDate = string.Format(_toDateFormat[_CurrentToDateFormat]
                    , hiCal.GetYear(dt)
                    , _zeroInserter(hiCal.GetMonth(dt), 2)
                    , _zeroInserter(hiCal.GetDayOfMonth(dt), 2)
                    , _monthNames[(int)CalendarTypes.Hijri, hiCal.GetMonth(dt) - 1]);

                break;
        }
        converted.ToDate = convertedDate;
        return converted;
    }
    /// <summary>
    /// inserts zero befor numbers that required to be in certain length
    /// </summary>
    /// <param name="iInput">the integer to convert</param>
    /// <param name="iTotalLength">total length required</param>
    /// <returns>the string with the number in proper length</returns>
    string _zeroInserter(int iInput, int iTotalLength)
    {
        string res = iInput.ToString();
        if (res.Length != iTotalLength)
        {
            for (int i = 0; i < iTotalLength - iInput.ToString().Length; i++)
            {
                res = "0" + res;
            }

        }
        return res;

    }
}
/// <summary>
/// this simple structure enhances the date element transfere between classes
/// </summary>
public struct DateInInteger
{
    public int Year, Month, Day;
}
/// <summary>
/// the order of date elements in default
/// </summary>
public enum DateElementOrder { Day, Month, Year }
/// <summary>
/// all the calendar types supported
/// </summary>
public enum CalendarTypes { Persian, Gregorian, Hijri }

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