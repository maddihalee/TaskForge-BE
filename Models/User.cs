﻿namespace TaskForge.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirebaseUid { get; set; }
        public ICollection<Task> ListofTasks { get; set; }
    }
}
