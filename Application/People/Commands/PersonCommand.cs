
namespace Application.People.Commands;

public class PersonCommand : BaseCommand
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FatherName { get; set; }
    public DateTime BirthDate { get; set; }
    public int GenderId { get; set; }
    public string FinNumber { get; set; }
}