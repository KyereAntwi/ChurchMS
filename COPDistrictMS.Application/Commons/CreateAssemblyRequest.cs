namespace COPDistrictMS.Application.Commons;

public record CreateAssemblyRequest(
    string Title,
    string Location,
    string YearEstablished
    );