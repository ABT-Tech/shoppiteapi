using MediatR;
using Shoppite.Application.Commands;
using Shoppite.Core.DTOs;
using Shoppite.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shoppite.Application.Handlers.CommandHandlers
{
    public class UpdateSubcategoryHandler : IRequestHandler<UpdateSubcategoryCommand, Subcatgory_DTO>
    {
        private readonly ISubcategoryRepository _createSubcategoryRepository;

        public UpdateSubcategoryHandler(ISubcategoryRepository createSubcategoryRepository)
        {
            _createSubcategoryRepository = createSubcategoryRepository;
        }
        public async Task<Subcatgory_DTO> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            return await _createSubcategoryRepository.UpdateSubCategory(request.subcatgory_DTO);
        }
    }
}
