using GPay.Banking.BankHub;

namespace GPay.Banking.Helpers;

internal static class EntityIdExtensions
{
    public static void SetIdForInsert(this BankHubAvsBatch batch, Guid id)
    {
        Volo.Abp.Domain.Entities.EntityHelper.TrySetId(batch, () => id);
    }
}
