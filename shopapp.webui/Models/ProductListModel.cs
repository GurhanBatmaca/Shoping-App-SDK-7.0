using shopapp.entity;

namespace shopapp.webui.Models;

public class ProductListModel
{
    public List<Product>? Products { get; set; }
    public PageInfo? PageInfo { get; set; }
}

public class PageInfo
{
    public int TotalItems { get; set; }
    public int ItemPerPage { get; set; }
    public int CurrentPage { get; set; }
    public string? CurrentCategory { get; set; }
    public string? SearchString { get; set; }
    public EnumOrderState? OrderState { get; set; }

    public int TotalPages()
    {
        return (int)Math.Ceiling((decimal)TotalItems/ItemPerPage);
    }
}
