using Questrade.FinCrime.Email.Intelligence.Domain.Models.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository.Models;

namespace Questrade.FinCrime.Email.Intelligence.Domain.Repository;

public interface ILexisNexisRepository
{
    public Task<AttributeQuery?> GetLexisNexisAttributeQueryAsync(GetLexisNexisAttributeQueryRequest request, CancellationToken cancellationToken);
}