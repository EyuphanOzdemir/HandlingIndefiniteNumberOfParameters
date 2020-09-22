using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;


namespace Models
{
    public class Parameter
    {

        public int id { get; set; }
        public string label { get; set; }
        public DbType dbType { get; set; }
        public String value { get; set; }

        public IEnumerable<SelectListItem> selectList { get; set; }

        private string _selectListCSV;

        public string selectListCSV
        {
            get => _selectListCSV;

            set => _selectListCSV = value;
        }
        public bool required { get; set; }

        public bool isLongText { get; set; }
        public int length { get; set; }

        //if the type is boolean, we need to use checkbox in our partial view (_ParametersView), and 
        //in the following code we cant use "value" which is an object.
        //@Html.CheckBoxFor(modelItem => Model.parameters[i].boolValue, new { @class = "form-control" })
        //This is because value of the parameter in this class is an object and objects cannot be implicitly converted to bool.
        public bool boolValue
        {
            get; set;
        }

        //we need this field to get all the information we need to show to the user

        public string explanation
        {
            get
            {
                string info = label;
                if (dbType!=DbType.Boolean)
                {
                    String type = dbType.ToString();
                    if (dbType==DbType.Int16 || dbType == DbType.Int32 || dbType == DbType.Int64)
                        type = "numeric";

                    info += "Please enter a(n) " + type + " value.";
                    info += required ? " This field is required. " : "";
                    info += length > 0 ? " Max. " + length + " characters." : "";
                }

                return info;
            }
            set { }
        }
    }
}