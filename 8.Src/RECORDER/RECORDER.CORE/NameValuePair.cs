using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public struct NameValuePair {
        public NameValuePair(string name, object value, TypeCode valueType)
            : this() {
                this.Name = name;
                this.Value = value;
                this.ValueType = valueType;
            }
        public string Name { get;  set; }
        public object Value { get;  set; }
        public TypeCode ValueType { get;  set; }

        public override int GetHashCode() {
            return
                this.Name.GetHashCode() ^
                this.Value.GetHashCode() ^
                this.ValueType.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }
            if (obj is NameValuePair) {
                var other = (NameValuePair)obj;
                return
                    this.Name.Equals(other.Name) &&
                    this.ValueType.Equals(other.ValueType) &&
                    this.Value.Equals(other.Value);
            } else {
                return false;
            }
        }
    }
}
