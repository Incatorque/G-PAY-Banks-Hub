using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("TransactionStore")]
public partial class TransactionStore : Entity<int>
{
    [Key]
    [Column("id")]
    public override int Id { get; protected set; }

    public Guid EntityId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EntityName { get; set; } = null!;

    [StringLength(300)]
    [Unicode(false)]
    public string EntityAddress { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string EmailAddress { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime Created { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Msisdn { get; set; } = null!;

    public int Topup { get; set; }

    [Column(TypeName = "money")]
    public decimal TopupValue { get; set; }

    public int Payment { get; set; }

    [Column(TypeName = "money")]
    public decimal PaymentValue { get; set; }

    public int Withdrawal { get; set; }

    [Column(TypeName = "money")]
    public decimal WithdrawalValue { get; set; }

    public int Transfers { get; set; }

    [Column(TypeName = "money")]
    public decimal TransferValue { get; set; }

    public int SendMoney { get; set; }

    [Column(TypeName = "money")]
    public decimal SendMoneyValue { get; set; }

    public int WalletWithdrawal { get; set; }

    [Column(TypeName = "money")]
    public decimal WalletWithdrawalValue { get; set; }

    public int Refund { get; set; }

    [Column(TypeName = "money")]
    public decimal RefundValue { get; set; }

    public int Beneficiary { get; set; }

    [Column(TypeName = "money")]
    public decimal BeneficiaryValue { get; set; }

    public int BankDefined { get; set; }

    [Column(TypeName = "money")]
    public decimal BankDefinedValue { get; set; }

    [Column(TypeName = "money")]
    public decimal DepositValue { get; set; }

    public int Electricity { get; set; }

    [Column(TypeName = "money")]
    public decimal ElectricityValue { get; set; }

    public int Prepaid { get; set; }

    [Column(TypeName = "money")]
    public decimal PrepaidValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }
}


