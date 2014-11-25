namespace Shafam.Common.DataModel
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public int PatientId { get; set; }
    }
}