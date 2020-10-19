using System;
using System.ComponentModel.DataAnnotations;

namespace Test_PC.Annotations
{
    public class LimitPageAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int intval;
                var intornot = Int32.TryParse(value.ToString(), out intval);
                if (intornot)
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}