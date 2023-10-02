namespace COPDistrictMS.Application.Commons;

public class BaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? ValidationErrors { get; set; }
    public object? Data { get; set; }         
}
