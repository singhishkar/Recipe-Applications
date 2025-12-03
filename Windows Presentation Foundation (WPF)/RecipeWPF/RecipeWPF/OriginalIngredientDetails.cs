using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeWPF
{
    // Class to store details about the original quantity and calories of an ingredient
    public class OriginalIngredientDetails
    {
        // Property to store the original quantity of the ingredient
        public double OriginalQuantity { get; set; }

        // Property to store the original calories of the ingredient
        public double OriginalCalories { get; set; }

        // Constructor to initialize the OriginalIngredientDetails object with specified quantity and calories
        public OriginalIngredientDetails(double originalQuantity, double originalCalories)
        {
            OriginalQuantity = originalQuantity;
            OriginalCalories = originalCalories;
        }
    }

}
