using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVC5Homework.Models
{
    public class IsValidEmailAttribute : DataTypeAttribute
    {
        public IsValidEmailAttribute() : base(DataType.Text)
        {
            ErrorMessage = "Email格式不對!";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }


            string email = value.ToString();
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}