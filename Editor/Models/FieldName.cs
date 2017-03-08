using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Editor.Models
{
    class FieldNameAttribute : Attribute
    {
      
        public FieldNameAttribute(string name)
        {
            this.Name = name;
           
        }
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DisplayNameAttribute
        //     class using the display name.
        //
        // Parameters:
        //   displayName:
        //     The display name.
       


        public string Name { get; set; }
    }
}
