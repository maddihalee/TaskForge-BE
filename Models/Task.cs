﻿namespace TaskForge.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }

        public DateTime Due { get; set; }
   
        public User User { get; set; }
        public ICollection<Priority> Priorities { get; set; }
    }
}
