using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandsBehaviors
{
	public class CommandPropertyNotification
	{
		public string PropertyName { get; private set; }
		public string Message { get; private set; }

		public CommandPropertyNotification(string propertyName, string message)
        {
			PropertyName = propertyName;
			Message = message;
		}
	}

    public abstract  class CommandValidatorContext
	{
		private readonly List<CommandPropertyNotification> _notifications;
		public IReadOnlyCollection<CommandPropertyNotification> Notifications => _notifications;
		public bool IsInvalid => _notifications.Any();

		public CommandValidatorContext()
		{
			_notifications = new List<CommandPropertyNotification>();
		}

		protected void AddNotification(string message)
		{
			_notifications.Add(new CommandPropertyNotification("no-property-name", message));
		}

		protected void AddNotification(string propertyName,  string message)
		{
			_notifications.Add(new CommandPropertyNotification(propertyName, message));
		}

		protected void AddNotifications(IReadOnlyCollection<CommandPropertyNotification> pNotifications)
		{
			foreach(var notification in pNotifications)
				_notifications.Add(new CommandPropertyNotification(notification.PropertyName, notification.Message));
		}

		

		protected void NotificationIncrementalIdIncorret(int id, string propertyName, string message)
		{
			if (id <= 0)
				AddNotification(propertyName, message);
		}

		protected void NotificationEmptyString(string str, string propertyName, string message)
        {
			if (string.IsNullOrEmpty(str))
				AddNotification(propertyName, message);
        }
		

		protected void NotificationCollection(IEnumerable<CommandValidatorContext> collection)
        {
			if (collection == null) return;

			if (collection.Any())
			{
				foreach (var currentItem in collection)
				{
					currentItem.Validate();
					if (currentItem.IsInvalid)
					{
						AddNotifications(currentItem.Notifications);
						break;
					}
				};
			}
		}

		protected void NotificationNullOrEmptyCollection(IEnumerable<object> collection, string propertyName, string message)
		{
			if (collection == null)
				AddNotification(propertyName, message);
			else
			if (! collection.Any())
			{
				AddNotification(propertyName, message);
			}
		}

		protected void NotificationNullValue(object value, string propertyName, string message)
        {
			if (value == null)
				AddNotification(propertyName, message);
        }


		public abstract void Validate();
	}
}
