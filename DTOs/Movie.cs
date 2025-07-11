﻿namespace DTOs
{
    public class Movie : BaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
