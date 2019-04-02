using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SqlserverXmlVsNvarchar
{
    public class NvarcharEntity
    {
        public int Id {get;set;}
        public string Data {get;set;}
    }
}
