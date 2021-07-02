using System.Collections.Generic;
using System.Linq;

namespace Mobile.Models
{
    public class ApiOperationResponse<TBody>  where TBody : struct
    {
        public bool IsValid { get; set; }
        public TBody Body { get; set; }
        public IEnumerable<PropertyNotification> Notifications { get; private set; }


        public void SetNotification(string propertyName, string message)
        {
            IsValid = false;
            ((List<PropertyNotification>)Notifications).Add(new PropertyNotification() { PropertyName = propertyName, Message = message });
        }

        public void SetNotification(string message)
        {
            SetNotification("No-Property-Name", message);
        }

        public void SetNotification(IEnumerable<PropertyNotification> notifications)
        {
            foreach (var notification in notifications)
                SetNotification(notification.PropertyName, notification.Message);
        }

        public void Reset()
        {
            ((List<PropertyNotification>)Notifications).Clear();
            IsValid = true;
        }

        public string GetSingleMessageNotification()
        {
            return Notifications.FirstOrDefault().Message;
        }
    }
}
