﻿namespace GeneralNotes.Domain.Entities;
public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Contact { get; set; }
    public string Password { get; set; }
}
