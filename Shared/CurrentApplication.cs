using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DanielEiserman.DateAndTime;

namespace EasyBase.Classes
{
	public static class CurrentApplication
	{
        private static int _skippedDays = 0;

        public static string Name
        {
            get
            {
                return "CashBox";
            }
        }

        public static bool IsProduktion
        {
            get { return true; }
        }

		public static DateTime DateTimeNow
		{
			get { return DateTime.Now.AddDays(_skippedDays); }
		}

		public static Date DateNow
		{
			get { return (Date)DateTimeNow; }
		}

		public static int SkippedDays
		{
			get { return _skippedDays; }
		}

		public static void SkipDays(int days)
		{
			_skippedDays += days;
		}

        public static int UserNo
        {
            get;
            set;
        }

        public static string MoneyDisplayFormat
        {
            get { return "#,###,##0.00"; }
        }

        public static string MoneyEditFormat
        {
            get { return "0.00"; }
        }

        public static int GridColumnAmountWidth
        {
            get { return 100; }
        }

        private static Verification _currentVerification = null;
        public static Verification CurrentVerification
        {
            get { return _currentVerification; }
            set
            {
                if (_currentVerification != value) {
                    _currentVerification = value;
                    ApplicationEvents.OnVerificationChanged(_currentVerification);
                }
            }
        }
    }
}
