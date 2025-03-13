﻿using Api.People;

namespace Application.People.Queries;

public class PersonDto : BaseDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FatherName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender GenderId { get; set; }
    public string FinNumber { get; set; }
}