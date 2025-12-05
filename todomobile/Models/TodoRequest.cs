using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todomobile.Models
{
    public record TodoRequest(int userid, string name, string description);
}
