using HrApp.DAL.Enums;

namespace HrApp.DAL.Dtos;

public class GetPindingLettersOutputDto
{
    public int Count { get; set; } = 0;
    public List<PindingLetterOutputDto> RequestLetters { get; set; } = new();

}
public class PindingLetterOutputDto
{
    public int Id { get; set; }
    public string? DestinationName { get; set; }
    public bool IsRequiredCCC { get; set; }
    public string? Details { get; set; }
    public LetterType LetterType { get; set; }
    public string? UploadFileId { get; set; }
    public string EmployeeId { get; set; }
    public string EmployeeNameAr { get; set; }
    public string EmployeeNameEn { get; set; }

}
