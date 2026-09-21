using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class TransactionStore
{
    public int Id { get; set; }

    public Guid EntityId { get; set; }

    public string EntityName { get; set; } = null!;

    public string EntityAddress { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public DateTime Created { get; set; }

    public string Msisdn { get; set; } = null!;

    public int Topup { get; set; }

    public decimal TopupValue { get; set; }

    public int Payment { get; set; }

    public decimal PaymentValue { get; set; }

    public int Withdrawal { get; set; }

    public decimal WithdrawalValue { get; set; }

    public int Transfers { get; set; }

    public decimal TransferValue { get; set; }

    public int SendMoney { get; set; }

    public decimal SendMoneyValue { get; set; }

    public int WalletWithdrawal { get; set; }

    public decimal WalletWithdrawalValue { get; set; }

    public int Refund { get; set; }

    public decimal RefundValue { get; set; }

    public int Beneficiary { get; set; }

    public decimal BeneficiaryValue { get; set; }

    public int BankDefined { get; set; }

    public decimal BankDefinedValue { get; set; }

    public decimal DepositValue { get; set; }

    public int Electricity { get; set; }

    public decimal ElectricityValue { get; set; }

    public int Prepaid { get; set; }

    public decimal PrepaidValue { get; set; }

    public DateTime OrderDate { get; set; }
}
