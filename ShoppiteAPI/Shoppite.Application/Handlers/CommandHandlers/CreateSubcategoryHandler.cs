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
    public class CreateSubcategoryHandler : IRequestHandler<CreateSubcategoryCommand, Subcatgory_DTO>
    {
        private readonly ISubcategoryRepository _createSubcategoryRepository;

        public CreateSubcategoryHandler(ISubcategoryRepository createSubcategoryRepository)
        {
            _createSubcategoryRepository = createSubcategoryRepository;
        }
        public async Task<Subcatgory_DTO> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            return await _createSubcategoryRepository.PostSubcategory(request.subcatgory_DTO);
        }
    }
}
