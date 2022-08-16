namespace StudentsAPI.Models
{
    public class SearchModel
    {

        public string? searchPhrase { get; set; }

        public SearchModel(string? searchPhrase)
        {
            this.searchPhrase = searchPhrase;
        }

        public SearchModel()
        {
            
        }
    }
}
