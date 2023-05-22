using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Slicer.Application.Models
{
    public record CreateUrlCommandRequest(string Shortening, string Url, string Comment)
    {
        
    }
}
