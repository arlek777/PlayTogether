using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayTogether.Domain;

namespace PlayTogether.BusinessLogic
{
    public class MasterValueService: IMasterValueService
    {
        private readonly ISimpleCRUDService _simpleCRUDService;
        MasterValueService(ISimpleCRUDService simpleCRUDService)
        {
            _simpleCRUDService = simpleCRUDService;
        }

        public async Task<ICollection<T>> GetMasterValuesByIds<T>(ICollection<Guid> ids) 
            where T: class, ISimpleEntity 
        {
            return await _simpleCRUDService.Where<T>(m => ids.Contains(m.Id));
        }
    }
}
