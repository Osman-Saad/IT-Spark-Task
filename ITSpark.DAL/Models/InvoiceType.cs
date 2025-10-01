using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.DAL.Models
{
    public enum InvoiceType
    {
        [EnumMember(Value = "Cash")]
        Cash,
        [EnumMember(Value = "Credit")]
        Credit,
        [EnumMember(Value = "Online")]
        Online,
    }
}
