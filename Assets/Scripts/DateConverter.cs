
using System.Globalization;
using System;

public class DateConverter
{
    CalendarTypes _fromDateType, _toDateType;
    string[] _fromDateFormat = { "{0}/{1}/{2}", "{2}/{1}/{0}", "{0}/{3}/{2}", "{2}/{3}/{0}" };
    string[] _toDateFormat = { "{0}/{1}/{2}", "{2}/{1}/{0}", "{0}/{3}/{2}", "{2}/{3}/{0}" };
    string[] _dateFormatNames = { "Persian", "Gregorian", "Hijri" };
    public int _CurrentFromFormat { get; set; }
    public int _CurrentToDateFormat { get; set; }
    DateInInteger _dateToConvert;


    public DateConverter(DateInInteger iDate, CalendarTypes iFromType, CalendarTypes iToType)
    {
        _fromDateType = iFromType;
        _toDateType = iToType;
        _dateToConvert = iDate;
    }

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
            );


        DateTime dt = new DateTime();
        string convertedDate = string.Empty;



        switch (_fromDateType)
        {
            case CalendarTypes.Perian:

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
            case CalendarTypes.Perian:
                PersianCalendar pCal = new PersianCalendar();
                convertedDate = string.Format(
                    _toDateFormat[_CurrentToDateFormat]
                    , pCal.GetYear(dt)
                    , _zeroInserter(pCal.GetMonth(dt), 2)
                    , _zeroInserter(pCal.GetDayOfMonth(dt), 2)
                    );
                break;
            case CalendarTypes.Gregorian:
                convertedDate = string.Format(
                    _toDateFormat[_CurrentToDateFormat]
                    , dt.Year
                    , _zeroInserter(dt.Month, 2)
                    , _zeroInserter(dt.Day, 2)
                    );

                break;
            case CalendarTypes.Hijri:
                HijriCalendar hiCal = new HijriCalendar();
                convertedDate =
                    convertedDate = string.Format(_toDateFormat[_CurrentToDateFormat]
                    , hiCal.GetYear(dt)
                    , _zeroInserter(hiCal.GetMonth(dt), 2)
                    , _zeroInserter(hiCal.GetDayOfMonth(dt), 2)
                    );

                break;
        }
        converted.ToDate = convertedDate;
        return converted;
    }
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

public enum DateElementOrder { Day, Month, Year }
public enum CalendarTypes { Perian, Gregorian, Hijri }