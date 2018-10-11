using PlayTogether.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayTogether.BusinessLogic
{
    public interface IMasterValueService
    {
        Task<ICollection<T>> GetMasterValuesByIds<T>(ICollection<Guid> ids)
            where T : class, ISimpleEntity;
    }
}