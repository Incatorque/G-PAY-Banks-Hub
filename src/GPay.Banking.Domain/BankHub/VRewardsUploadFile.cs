using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VRewardsUploadFile
{
    [Column("PKRewardsUploadFileId")]
    public int PkrewardsUploadFileId { get; set; }

    [Column("filename")]
    [StringLength(500)]
    [Unicode(false)]
    public string Filename { get; set; } = null!;

    [Column("filepath")]
    [Unicode(false)]
    public string Filepath { get; set; } = null!;

    [Column("FKEntityId")]
    public Guid FkentityId { get; set; }

    [Column("FKUploadedUserId")]
    public Guid FkuploadedUserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UploadedUser { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UploadedEntity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UploadDate { get; set; }

    public bool? Active { get; set; }

    public byte[] UploadedData { get; set; } = null!;
}


