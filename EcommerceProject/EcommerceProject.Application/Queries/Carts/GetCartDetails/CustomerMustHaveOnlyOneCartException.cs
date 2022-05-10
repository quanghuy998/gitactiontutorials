using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.Application.Queries.Carts.GetCartDetails
{
    public class CustomerMustHaveOnlyOneCartException : Exception
    {
        public override string Message
        {
            get
            {
                return "Each customer must has an cart. Something is broken.";
            }
        }
    }
}
