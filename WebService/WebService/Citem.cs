using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService
{
    public class Citem
    {
        public Guid idItem{get; set;}
        public System.Data.Linq.Binary imageItem { get; set; }
        public Guid idLocation { get; set; }
        public Citem()
        {
        }
        public Citem(Guid iditem, System.Data.Linq.Binary img, Guid idlocation)
        {
            this.idItem = iditem;
            this.imageItem = img;
            this.idLocation = idlocation;
        }
    }
}