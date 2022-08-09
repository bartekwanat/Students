﻿namespace StudentsAPI.Models
{
    public class PaginationModel
    {
       
            private const int MaxItemsPerPage = 50;

            public int pageSize { get; set; } = 20;
            public int page { get; set; } = 0;

            public PaginationModel(int page, int pageSize)
            {
                this.page = page;
                this.pageSize = pageSize;
            }

            public PaginationModel()
            {

            }

            [System.Text.Json.Serialization.JsonIgnore]
            public int ItemsPerPage => pageSize > MaxItemsPerPage ? MaxItemsPerPage : pageSize;
        }
    }

