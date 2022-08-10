namespace StudentsAPI.Models
{
    public class PaginationModel
    {
       
            private const int MaxItemsPerPage = 50;

            public int PageSize { get; set; } = 20;
            public int Page { get; set; } = 0;

            public PaginationModel(int page, int pageSize)
            {
                this.Page = page;
                this.PageSize = pageSize;
            }

            public PaginationModel()
            {

            }

            [System.Text.Json.Serialization.JsonIgnore]
            public int ItemsPerPage => PageSize > MaxItemsPerPage ? MaxItemsPerPage : PageSize;
        }
    }

