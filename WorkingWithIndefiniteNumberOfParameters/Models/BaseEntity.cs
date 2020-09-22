using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

//All parameter work is in the responsibility of BaseClass
namespace Models
{
    public abstract class BaseEntity
    {
        //data members of the class
        public int id { get; set; }

        [Display(Name = "Label")]
        [Required]
        public string Label { get; set; }
        public List<Parameter> Parameters { get; set; }

        //method members of the class

        public virtual void GetParameters()
        {
            //Create or clear the parameters 
            if (Parameters == null)
                Parameters = new List<Parameter>();
            else
                Parameters.Clear();

            //common parameters
            Parameter paramBrand = new Parameter() { id = 1, label = "Brand", dbType = DbType.String, value = "ASUS", required = false, length = 50 };
            Parameter paramProductDate = new Parameter() { id = 2, label = "Register Date", dbType = DbType.DateTime, required = true, length = 10 };
            Parameter paramModelNumber = new Parameter() { id = 3, label = "Model Number", dbType = DbType.Int32, value = "125", required = true };
            Parameter paramExplanation = new Parameter() { id = 4, label = "Explanation", dbType = DbType.String, required = false, isLongText = true };

            Parameters.Add(paramBrand);
            Parameters.Add(paramProductDate);
            Parameters.Add(paramModelNumber);
            Parameters.Add(paramExplanation);
        }

        private string CheckValue(Parameter parameter)
        {
            String value = parameter.value ?? "";

            //if the parameter is required 
            if (parameter.required && String.IsNullOrEmpty(value))
                return parameter.label + " is a required field";

            //length control
            if (parameter.length > 0 && value.Length > parameter.length)
                return "The value of " + parameter.label + " can be maximum " + parameter.length.ToString() + " characters";

            //type control
            bool ret = false;
            if (parameter.dbType == DbType.Int32)
            {
                int x;
                ret = int.TryParse(value, out x);
            }
            else if (parameter.dbType == DbType.DateTime)
            {
                DateTime x;
                ret = DateTime.TryParse(value, out x);
            }
            else if (parameter.dbType == DbType.Decimal)
            {
                Decimal x;
                ret = Decimal.TryParse(value, out x);
            }
            else if (parameter.dbType == DbType.Boolean)
            {
                Boolean x;
                ret = Boolean.TryParse(value, out x);
            } //and so on
            else
                ret = parameter.dbType == DbType.String;

            if (!ret)
                return "The value of " + parameter.label + " should be " + parameter.dbType.ToString();
            else
                return "";
        }

        public string CheckAndUpdateParameters(ModelStateDictionary modelState)
        {
            //I am a pessimist :)
            string ret = "Failed Update";

            //check parameters
            string checkValue = CheckParameters();

            //if there is a problem, add the error to the modal state
            if (!String.IsNullOrEmpty(checkValue))
            {
                modelState.AddModelError(string.Empty, checkValue);
                return ret;
            }
            else
            {
                //if there is no problen do something for real, DB update
                checkValue = UpdateParameterValuesInDB();
                if (!String.IsNullOrEmpty(checkValue))
                {
                    modelState.AddModelError(string.Empty, checkValue);
                    return ret;
                }
            }

            //everything is fine
            ret = "Successful Update";
            return ret;
        }

        private string CheckParameters()
        {
            foreach (Parameter parameter in Parameters)
            {
                //if the parameter is boolean, set the value
                if (parameter.dbType == DbType.Boolean)
                    parameter.value = parameter.boolValue.ToString();
                //check the value
                string ret = CheckValue(parameter);
                if (!String.IsNullOrEmpty(ret))
                    return ret;
            }
            return ""; //everything is fine
        }

        //since this will update different tables and columns depending on the domain class, I defined this method abstract.
        protected abstract string UpdateParameterValuesInDB();

        public void FillTheSelectLists()
        {
            foreach (Parameter parameter in Parameters)
            {
                if (!string.IsNullOrEmpty(parameter.selectListCSV))
                {
                    List<SelectListItem> _list = new List<SelectListItem>();
                    string[] items = parameter.selectListCSV.Split(',');
                    foreach (string item in items)
                    {
                        SelectListItem listItem = new SelectListItem();
                        string[] innerItems = item.Split('=');
                        listItem.Value = innerItems[0];
                        listItem.Text = innerItems[1];
                        _list.Add(listItem);
                    }

                    SelectList _selectList = new SelectList(_list, "Value", "Text");
                    parameter.selectList = _selectList;
                }
            }
        }
    }
}