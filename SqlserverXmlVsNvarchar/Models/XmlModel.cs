using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlserverXmlVsNvarchar
{
    public class XmlEntity
    {
        public int Id {get;set;}

        [Column(TypeName = "xml")]
        public string Data {get;set;}
    }
}
