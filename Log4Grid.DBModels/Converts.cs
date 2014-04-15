using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.DBModels
{
    public class BoolToInt : Peanut.Mappings.PropertyCastAttribute
    {

        public override object ToProperty(object value, Type ptype, object source)
        {
            return (int)value > 0 ? true : false;
        }

        public override object ToColumn(object value, Type ptype, object source)
        {
            return (bool)value ? 1 : 0;
        }
    }

    public class EnumToInt : Peanut.Mappings.PropertyCastAttribute
    {
        public override object ToProperty(object value, Type ptype, object source)
        {
            return Enum.ToObject(ptype, (int)value);
        }

        public override object ToColumn(object value, Type ptype, object source)
        {
            return (int)value;
        }
    }
}
