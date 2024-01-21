using System;

namespace intervals.Models
{
    public class EmployeeTag
    {
        public bool IsActual;
        public string TagId;
        public DateTime TimeStamp;
        public override string ToString()
        {
            return $"{IsActual} {TagId}  {TimeStamp}";
        }
    }
}