using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Models
{
    public class Lamp:BaseEntity
    {
        [DisplayName("Lamp Code")]
        [Required]
        public int LampCode { get; set; }

        public override void GetParameters()
        {
            base.GetParameters(); //to get the common parameters
            
            //special parameters of Lamp
            //Normally we would need to fetch parameters from DB
            //The code below is just for example.
            //special parameters of Lamb
            Parameter discountEndDate = new Parameter() { id = 5, label = "Discount End Date", dbType = DbType.DateTime, value = DateTime.Today.ToString(), required = true };
            Parameter inDiscount = new Parameter() { id = 6, label = "is in Discount", dbType = DbType.Boolean, boolValue = false, required = true };
            Parameters.Add(discountEndDate);
            Parameters.Add(inDiscount);
            Parameter guaranteePeriod = new Parameter() { id = 7, label = "Guarantee Period", dbType = DbType.String, required = true, selectListCSV="1=1 Year,2=2 Year"};
            Parameters.Add(guaranteePeriod);
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