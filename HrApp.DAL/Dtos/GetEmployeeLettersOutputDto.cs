using HrApp.DAL.Enums;

namespace HrApp.DAL.Dtos;

public class GetEmployeeLettersOutputDto
{
    public List<EmpRequestLetterDto> RequestLetters { get; set; } = new();
}
public class EmpRequestLetterDto
{
    public int Id { get; set; }

    public string? DestinationName { get; set; }
    public bool IsRequiredCCC { get; set; }
    public string? Details { get; set; }
    public LetterType LetterType { get; set; }

    public ApproveType ApproveStatus { get; set; }
    public string ApproveDetails { get; set; }
    public string? UploadFileId { get; set; }

}