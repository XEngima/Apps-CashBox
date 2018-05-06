using System;
using System.Collections.Generic;
using System.Text;

namespace DanielEiserman.DateAndTime
{
    public class Date
    {
        private DateTime _dateTime;

		public static DateTime FloorDateTime(DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
		}

        public Date(DateTime dateTime)
        {
            _dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public Date(int year, int month, int day)
        {
            _dateTime = new DateTime(year, month, day);
        }

        public int Year
        {
            get { return _dateTime.Year; }
        }

        public int Month
        {
            get { return _dateTime.Month; }
        }

        public int Day
        {
            get { return _dateTime.Day; }
        }

        public static bool operator ==(Date date1, Date date2)
        {
            return (date1.ToString() == date2.ToString());
        }

        public static bool operator !=(Date date1, Date date2)
        {
            return !(date1 == date2);
        }

        public override bool Equals(object obj)
        {
            return ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return _dateTime.GetHashCode();
        }

        public static bool operator >(Date date1, Date date2)
        {
            return date1.Year > date2.Year || (date1.Year == date2.Year && date1.Month > date2.Month) || (date1.Year == date2.Year && date1.Month == date2.Month && date1.Day > date2.Day);
        }

        public static bool operator <(Date date1, Date date2)
        {
            return date1 != date2 && !(date1 > date2);
        }

        public static bool operator >=(Date date1, Date date2)
        {
            return date1 > date2 || date1 == date2;
        }

        public static bool operator <=(Date date1, Date date2)
        {
            return date1 < date2 || date1 == date2;
        }

        public static implicit operator Date(DateTime dateTime)
        {
            return new Date(dateTime);
        }

		public static implicit operator DateTime(Date date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public override string ToString()
        {
            return _dateTime.ToShortDateString();
        }

        public string ToString(string format)
        {
            return _dateTime.ToString(format);
        }

        public Date AddDays(int days)
        {
            return _dateTime.AddDays(days);
        }

        public Date AddMonths(int months)
        {
            return _dateTime.AddMonths(months);
        }

        public Date AddYears(int years)
        {
            return _dateTime.AddYears(years);
        }
    }
}
