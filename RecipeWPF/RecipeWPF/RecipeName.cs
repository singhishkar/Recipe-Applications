using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RecipeWPF
{
    // Class to encapsulate the name of a recipe using a TextBox control
    public class RecipeName
    {
        // Constructor to initialize the RecipeName object with a specified TextBox control
        public RecipeName(TextBox textboxRecipeName)
        {
            // Assigning the provided TextBox control to the class property
            this.textboxRecipeName = textboxRecipeName;
        }

        // Property to store the TextBox control for the recipe name
        public TextBox textboxRecipeName { get; set; }
    }

}
