using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterCommand.Enums.Attributes
{
    public static class EnumExtensions
    {
        public static string MacAddress(this Device value)
        {
            var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            if (enumMember == null)
            {
                throw new Exception($"Cannot find Device {value}");
            }

            if (!(enumMember.GetCustomAttributes(typeof(MacAddressAttribute),false).FirstOrDefault() is MacAddressAttribute attribute))
            {
                throw new Exception($"Cannot find \"MacAddress\" attribute on {value}");
            }
            return
                attribute.MacAddress;
        }
    }
}
