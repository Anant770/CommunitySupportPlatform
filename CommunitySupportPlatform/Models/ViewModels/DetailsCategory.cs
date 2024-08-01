using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunitySupportPlatform.Models.ViewModels
{
    public class DetailsCategory
    {
        public CategoryDto SelectedCategory { get; set; }
        public IEnumerable<JobDto> RelatedJobs { get; set; }
    }
}