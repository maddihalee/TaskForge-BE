﻿namespace TaskForge.Models
{
    public class Priority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
