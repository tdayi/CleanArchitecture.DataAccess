namespace Core.Repository;

public class PagingResponse<TPagingModel>
{
    public int TotalCount { get; set; }
    public IEnumerable<TPagingModel> Result { get; set; }
}