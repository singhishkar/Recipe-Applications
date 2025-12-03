using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RecipeWPF
{
    // Class to encapsulate the details of a step in a recipe using a TextBox control
    public class StepDetails
    {
        // Constructor to initialize the StepDetails object with a specified TextBox control
        public StepDetails(TextBox textboxStepDetails)
        {
            // Assigning the provided TextBox control to the class property
            this.textboxStepDetails = textboxStepDetails;
        }

        // Property to store the TextBox control for the step details
        public TextBox textboxStepDetails { get; set; }
    }

}
