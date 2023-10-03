namespace COPDistrictMS.Application.Features.Districts.Dtos;

public record CreateAssemblyDto(
    string Title,
    string Location,
    string? YearEstablished
    );
