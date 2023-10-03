namespace COPDistrictMS.Application.Features.Dtos;

public record CreateAssemblyDto(
    string Title,
    string Location,
    string? YearEstablished
    );
