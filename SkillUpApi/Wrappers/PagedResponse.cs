﻿using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillUpApi.Wrappers
{
    public class PagedResponse<T>:Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public PagedResponse()
        {
            Mesagge = null;
            Succeeded = true;
            Errors = null;
        }
    }
}
