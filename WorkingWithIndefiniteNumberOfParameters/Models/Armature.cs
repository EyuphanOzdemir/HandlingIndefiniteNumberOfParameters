using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Models
{
    public class Armature:BaseEntity
    {
        [Required]
        [Display(Name = "Armature Name")]
        public string ArmatureName { get; set; }
        public override void GetParameters()
        {
            base.GetParameters(); //to get the common parameters

            //special parameters of Armature
            //Normally we would need to fetch parameters from DB
            //The code below is just for example.
            //special parameters of Armature
            Parameter paramPhone = new Parameter() { id = 5, label = "Phone Number", dbType = DbType.String, value = "5325698871", required = true, length = 10 };
            Parameter paramIncomeLevel = new Parameter() { id = 6, label = "Income level", dbType = DbType.Int32, value = "3", required = true };
            Parameter paramFromCompany = new Parameter() { id = 7, label = "From company", dbType = DbType.Boolean, boolValue = true };


            Parameters.Add(paramPhone);
            Parameters.Add(paramIncomeLevel);
            Parameters.Add(paramFromCompany);
        }

        protected override string UpdateParameterValuesInDB()
        {
            //loop in parameter collection
            foreach (Parameter parameter in Parameters)
            {
                //update the value of each parameter
            }
            return "";
        }
    }
}
