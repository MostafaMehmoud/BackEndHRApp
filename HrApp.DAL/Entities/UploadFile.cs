using HrApp.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("UPLOAD_FILE")]
public class UploadFile
{
    [Key]
    [Column("ID")]
    public string? Id  { get; set; }
    [Column("FILE_NAME")]
    public string? FileName  { get; set; }
    [Column("FILE_DATA")]
    public byte[] FileData { get; set; }
    [Column("FILE_TYPE")]
    public string? FileType  { get; set; }
    [Column("UPLOADED_DATE")]
    public DateTime UploadedDate  { get; set; } = DateTime.Now;
}
