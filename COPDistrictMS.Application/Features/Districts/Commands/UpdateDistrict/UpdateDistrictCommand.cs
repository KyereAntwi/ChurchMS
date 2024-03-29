﻿using AutoMapper;
using COPDistrictMS.Application.Commons;
using COPDistrictMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace COPDistrictMS.Application.Features.Districts.Commands.UpdateDistrict;

public record UpdateDistrictCommand(
    Guid Id,
    string Title,
    string Area,
    string DistrictPastor
    ) : IRequest<BaseResponse>;

public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand, BaseResponse>
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAsyncRepository<District> _asyncRepository;

    public UpdateDistrictCommandHandler(IAsyncRepository<District> asyncRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _asyncRepository = asyncRepository;
    }

    public async Task<BaseResponse> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validation = new UpdateDistrictCommandValidator(_asyncRepository);
        var validationErrors = await validation.ValidateAsync(request, cancellationToken);

        if (validationErrors.Errors.Any())
        {
            response.Success = false;
            response.Message = "Operation failed to perform update of district";

            response.ValidationErrors = new List<string>();

            foreach (var validationError in validationErrors.Errors)
            {
                response.ValidationErrors.Add(validationError.ErrorMessage);
            }

            return response;
        }

        response.Success = true;
        response.Message = "Operation was successful";

        var updatedDistrict = _mapper.Map<District>(request);
        updatedDistrict.UpdatedAt = DateTime.UtcNow;

        string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        updatedDistrict.CreatedBy = userId; // replace createdBy with updatedBy - same meaning

        await _asyncRepository.UpdateAsync(updatedDistrict);

        response.Data = updatedDistrict.Id;

        return response;
    }
}
