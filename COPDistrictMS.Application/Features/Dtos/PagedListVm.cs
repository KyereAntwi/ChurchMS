namespace COPDistrictMS.Application.Features.Dtos;

public class PagedListVm<T> where T : class
{
    public bool PrevPage { get; set; }
    public bool NextPage { get; set; }
    public int Count { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public ICollection<T> ListItems { get; set; } = new List<T>();
}
