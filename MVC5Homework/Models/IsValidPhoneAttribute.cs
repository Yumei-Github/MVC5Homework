using System;
using System.CodeDom;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVC5Homework.Models
{
    public class IsValidPhoneAttribute : DataTypeAttribute
    {
        public IsValidPhoneAttribute() : base(DataType.Text)
        {
            ErrorMessage = "手機格式不對!請輸入XXXX-XXXXXX";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string phone = value.ToString();
            Regex regex = new Regex(@"\d{4}-\d{6}");
            Match match = regex.Match(phone);
            if (match.Success)
                return true;
            else
                return false;

        }
    }
}