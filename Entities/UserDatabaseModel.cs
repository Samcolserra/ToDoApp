namespace ToDoApp.Entities
{
    public class UserDatabaseModel
    {
        public Guid Id {get; set;}
        public string _email {get; set;}
        public string _passwordHash {get; set;}
        public string _salt {get; set;}
        public DateTimeOffset _createdTimestamp {get; set;}
        public DateTimeOffset _updatedTimestamp {get; set;}
    }
}