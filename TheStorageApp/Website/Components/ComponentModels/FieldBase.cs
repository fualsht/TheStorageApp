using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStorageApp.Website.ClassAttrinutes;

namespace TheStorageApp.Website.Components.ComponentModels
{
    public abstract class FieldBase
    {
        public string FieldName { get; set; }
        public FieldDataTypes FieldDataType { get; set; }
        public object Value { get; set; }

        public FieldBase(object value, FieldDataTypes type)
        {
            Value = value;
            FieldDataType = type;
        }

        public string ValueAsString
        {
            get
            {
                return Value.ToString();
            }
            set
            {
                Value = value;
            }
        }

        public string ValueAsColorString
        {
            get
            {
                return Value.ToString();
            }
            set
            {
                Value = value;
            }
        }

        public byte[] ValueAsImage
        {
            get
            {
                return Value as byte[];
            }
            set
            {
                Value = value;
            }
        }

        public decimal ValueAsDecimal
        {
            get
            {
                return Convert.ToDecimal(Value);
            }
            set
            {
                Value = value;
            }
        }

        public string ValueAsMoneyString
        {
            get
            {
                return Convert.ToDecimal(Value).ToString();
            }
            set
            {
                Value = value;
            }
        }

        public int ValueAsInteger
        {
            get
            {
                return Convert.ToInt32(Value);
            }
            set
            {
                Value = value;
            }
        }
        public DateTime ValueAsDateTime
        {
            get
            {
                return Convert.ToDateTime(Value);
            }
            set
            {
                Value = value;
            }
        }
        public bool ValueAsBool
        {
            get
            {
                return Convert.ToBoolean(Value);
            }
            set
            {
                Value = value;
            }
        }
    }
}
