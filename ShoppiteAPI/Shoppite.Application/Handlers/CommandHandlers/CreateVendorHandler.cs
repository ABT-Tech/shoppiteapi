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
    public class CreateVendorHandler : IRequestHandler<CreateVendorCommand, Vendor_DTO>
    {
        private readonly IVendorRepository _createVendorRepository;

        public CreateVendorHandler(IVendorRepository createVendorRepository)
        {
            _createVendorRepository = createVendorRepository;
        }
        public async Task<Vendor_DTO> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            return await _createVendorRepository.PostVendor(request.vendor);
        }
    }
}
