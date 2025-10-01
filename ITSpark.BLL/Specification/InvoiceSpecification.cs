using ITSpark.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.BLL.Specification
{
    public class InvoiceSpecification:BaseSpecification<Invoice>
    {
        public InvoiceSpecification():base()
        {
            Includes.Add(I => I.Items);
        }
        public InvoiceSpecification(int id):base(I => I.Id == id)
        {
            Includes.Add(I => I.Items);
        }
    }
}
