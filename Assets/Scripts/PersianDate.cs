using System;
using System.Globalization;

public static class PersianDate
{

    private static string[] dayNames = { "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه", "یک شنبه" };
    private static string[] monthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

    public static string Now
    {
        get
        {
            PersianCalendar pcal = new PersianCalendar();
            DateTime dt = DateTime.Now;
            return pcal.GetYear(dt).ToString() + "/" + pcal.GetMonth(dt).ToString() + "/" + pcal.GetDayOfMonth(dt).ToString();

        }
    }
    public static string ToDayName
    {
        get
        {
            PersianCalendar pcal = new PersianCalendar();
            DateTime dt = DateTime.Now;
            return dayNames[((int)pcal.GetDayOfWeek(dt)) - 1];
        }
    }
    public static byte DayOfMonth
    {
        get
        {
            PersianCalendar pcal = new PersianCalendar();
            DateTime dt = DateTime.Now;
            return (byte)pcal.GetMonth(dt);

        }
    }
    public static string MonthName
    {
        get
        {
            PersianCalendar pcal = new PersianCalendar();
            DateTime dt = DateTime.Now;
            return monthNames[((int)pcal.GetMonth(dt)) - 1];
        }
    }
    public static string ToPersion(DateTime dt)
    {
        PersianCalendar pcal = new PersianCalendar();

        return pcal.GetYear(dt).ToString() + "/" + pcal.GetMonth(dt).ToString() + "/" + pcal.GetDayOfMonth(dt).ToString();
    }
    public static string ToPersion(int iYear, int iMonth, int iDay)
    {
        PersianCalendar pcal = new PersianCalendar();
        DateTime dt = new DateTime(iYear, iMonth, iDay);
        return pcal.GetYear(dt).ToString() + "/" + pcal.GetMonth(dt).ToString() + "/" + pcal.GetDayOfMonth(dt).ToString();
    }
    public static string ToPersion(DateInInteger iDate)
    {
        PersianCalendar pcal = new PersianCalendar();
        DateTime dt = new DateTime(iDate.Year, iDate.Month, iDate.Day);
        return pcal.GetYear(dt).ToString() + "/" + pcal.GetMonth(dt).ToString() + "/" + pcal.GetDayOfMonth(dt).ToString();
    }
    public static void ToPersion(DateTime dt, out string oYear, out string oMonth, out string oDay)
    {
        PersianCalendar pcal = new PersianCalendar();
        oYear = pcal.GetYear(dt).ToString();
        oMonth = pcal.GetMonth(dt).ToString();
        oDay = pcal.GetDayOfMonth(dt).ToString();
    }

    public static string ToGeorgian(int iYear, int iMonth, int iDay)
    {
        PersianCalendar pcal = new PersianCalendar();
        DateTime dt = new DateTime(iYear, iMonth, iDay, pcal);

        return $"{dt.Year}/{dt.Month}/{dt.Day}";
    }
    public static string ToGeorgian(DateInInteger iDate)
    {
        PersianCalendar pcal = new PersianCalendar();
        DateTime dt = new DateTime(iDate.Year, iDate.Month, iDate.Day, pcal);

        return $"{dt.Year}/{dt.Month}/{dt.Day}";
    }
    public static DateInInteger PersianToGeorgianInteger(DateInInteger iDate)
    {
        PersianCalendar pcal = new PersianCalendar();
        DateTime dt = new DateTime(iDate.Year, iDate.Month, iDate.Day, pcal);
        DateInInteger dii;
        dii.Year = dt.Year;
        dii.Month = dt.Month;
        dii.Day = dt.Day;
        return dii;
    }
    public static DateTime TextToCalendar(int iYear, int iMonth, int iDay, CalendarType iType)
    {
        Calendar c = new GregorianCalendar();
        switch (iType)
        {
            case CalendarType.Persian:
                c = new PersianCalendar();
                break;

            case CalendarType.Hijri:
                c = new HijriCalendar();
                break;

        }
        DateTime dt = new DateTime(iYear, iMonth, iDay, c);
        return dt;
    }
   



}
public enum CalendarType
{
    Persian,
    Gregorian,
    Hijri
}

public struct DateInInteger
{
    public int Year;
    public int Month;
    public int Day;
}

