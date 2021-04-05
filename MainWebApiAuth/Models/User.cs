namespace TokenApp.Models
{
    public class User
    {
        public object Username { get; internal set; }
        public object Password { get; internal set; }
        public long Id { get; internal set; }
        public object Firstname { get; internal set; }
        public object Middlename { get; internal set; }
        public object Lastname { get; internal set; }
        public object Age { get; internal set; }
    }
}