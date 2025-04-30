using HrApp.DAL.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("REQUEST_LETTER")]
public class RequestLetter
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    [Column("DESTINATION_NAME")]
    public string? DestinationName { get; set; }
    [Column("IS_REQUIRED_CCC")]
    public int IsRequiredCCC { get; set; }
    [Column("DETAILS")]
    public string? Details { get; set; }
    [Column("LETTER_TYPE")]
    public LetterType LetterType { get; set; }
    [Column("APPROVE_STATUS")]
    public ApproveType ApproveStatus { get; set; } = ApproveType.Pinding;
    [Column("APPROVE_DETAILS")]
    public string? ApproveDetails { get; set; }

    [Column("UPLOAD_FILE_ID")]
    public string? UploadFileId { get; set; }
    [Column("EMP_ID")]
    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }


}
