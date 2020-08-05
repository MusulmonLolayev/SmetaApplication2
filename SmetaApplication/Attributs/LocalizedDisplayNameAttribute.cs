using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Attributs
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string resourceId)
            : base(GetMessageFromResource(resourceId))
        { }

        private static string GetMessageFromResource(string resourceId)
        {
            //return Resources.Resources.labelForName;
            return "";
        }
    }
}
