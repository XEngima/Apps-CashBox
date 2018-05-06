using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DanielNilsson.DateAndTime;

namespace EasyBase.Classes
{
	public partial class MessageTemplate
	{
		public string ParseEventSubject(string eventName)
		{
			return Subject.Replace("{{event}}", eventName.Trim());
		}

		public string ParseReminderPartnerNotPaid(string name, string partnerName, string eventName, DateTime paymentDueDate)
		{
			if (Type != MessageType.PartnerRemindedNotification) {
				throw new InvalidOperationException("Message template is not a partner not payed.");
			}

			return Text.Replace("{{name}}", name).Replace("{{partner}}", partnerName).Replace("{{event}}", eventName).Replace("{{paymentduedate}}", paymentDueDate.ToShortDateString());
		}

		public string ParseExpiredNotification(string name, string eventName, DateTime paymentDueDate)
		{
			if (Type != MessageType.ExpireNotification) {
				throw new InvalidOperationException("Message template is not an expire notification.");
			}

			return Text.Replace("{{name}}", name).Replace("{{event}}", eventName).Replace("{{paymentduedate}}", paymentDueDate.ToShortDateString());
		}

        public string ParseExpiredCoupleNotification(string recipientsName, string partnersName, string eventName, DateTime paymentDueDate)
        {
            if (Type != MessageType.ExpireNotification)
            {
                throw new InvalidOperationException("Message template is not an expire notification.");
            }

            return Text.Replace("{{name}}", recipientsName).Replace("{{partnersname}}", partnersName).Replace("{{event}}", eventName).Replace("{{paymentduedate}}", paymentDueDate.ToShortDateString());
        }

        public string ParseApprovedNotification(string name, string eventName, DateTime eventStartTime)
		{
			if (Type != MessageType.ApprovedNotification) {
				throw new InvalidOperationException("Message template is not an approvement notification.");
			}

			return Text.Replace("{{name}}", name).Replace("{{event}}", eventName).Replace("{{eventstartdate}}", eventStartTime.ToShortDateString()).Replace("{{eventstarttime}}", eventStartTime.ToShortTimeString());
		}

		public string ParseUnregister(string recipientsName, string eventName)
		{
			if (Type != MessageType.UnregisterNotification) {
				throw new InvalidOperationException("Message template is not an unregister.");
			}

			return Text.Replace("{{name}}", recipientsName).Replace("{{event}}", eventName);
		}

		public string ParseUnregisterPartner(string recipientsName, string partnersName, string eventName, string siteUrl)
		{
			if (Type != MessageType.PartnerUnregisterNotification) {
				throw new InvalidOperationException("Message template is not an approvement notification.");
			}

			return Text.Replace("{{name}}", recipientsName).Replace("{{partnersname}}", partnersName).Replace("{{event}}", eventName).Replace("{{siteurl}}", siteUrl);
		}
	}
}
