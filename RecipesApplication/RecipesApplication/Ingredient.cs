using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesApplication
{
    // Class representing an ingredient used in a recipe
    public class Ingredient
    {
        // Property to store the name of the ingredient
        public string Name { get; set; }

        // Property to store the quantity of the ingredient
        public double Quantity { get; set; }

        // Property to store the unit of measurement for the quantity (e.g., cups, grams)
        public string Unit { get; set; }

        // Property to store the calorie count of the ingredient
        public double Calories { get; set; }

        // Property to store the food group the ingredient belongs to (e.g., fats, starch)
        public string FoodGroup { get; set; }

        // Property to store the original unit of measurement (before any conversions, if applicable)
        public string OriginalUnit { get; set; }

        // Property to store the original quantity (before any conversions, if applicable)
        public double OriginalQuantity { get; set; }

        // Property to store the original calorie count (before any adjustments, if applicable)
        public double OriginalCalories { get; set; }
    }
}

