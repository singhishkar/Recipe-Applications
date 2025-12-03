using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RecipeWPF
{
    // Class to store details about an ingredient including original and scaled quantities, units, and food group
    public class IngredientDetails
    {
        // Property to store the original quantity of the ingredient as a string
        public string OriginalQuantity { get; set; }

        // Property to store the original calories of the ingredient as a string
        public string OriginalCalories { get; set; }

        // Property to store the original unit of the ingredient
        public string OrginalUnit { get; set; }

        // Property to store the original food group of the ingredient
        public string OriginalFoodGroup { get; set; }

        // Property to store the scaled quantity of the ingredient as a double
        public double scaledQnty { get; set; }

        // Property to store the scaled unit of the ingredient
        public string scaledUnit { get; set; }

        // Constructor to initialize the IngredientDetails object using various text boxes and a combo box
        public IngredientDetails(TextBox textboxIngredName, TextBox textboxQuantity, TextBox textboxUnit, TextBox textboxCalories, ComboBox comboFoodGroup)
        {
            // Assigning the provided TextBox and ComboBox controls to the class properties
            this.textboxIngredName = textboxIngredName;
            this.textboxQuantity = textboxQuantity;
            this.textboxUnit = textboxUnit;
            this.textboxCalories = textboxCalories;
            this.comboFoodGroup = comboFoodGroup;

            // Initializing OriginalQuantity, OriginalCalories, and OriginalFoodGroup using the text values from the provided TextBox and ComboBox controls
            OriginalQuantity = textboxQuantity.Text;
            OriginalCalories = textboxCalories.Text;
            OriginalFoodGroup = comboFoodGroup.Text;
        }

        // Property to store the TextBox control for the ingredient name
        public TextBox textboxIngredName { get; set; }

        // Property to store the TextBox control for the ingredient quantity
        public TextBox textboxQuantity { get; set; }

        // Property to store the TextBox control for the ingredient unit
        public TextBox textboxUnit { get; set; }

        // Property to store the TextBox control for the ingredient calories
        public TextBox textboxCalories { get; set; }

        // Property to store the ComboBox control for the ingredient food group
        public ComboBox comboFoodGroup { get; set; }
    }

}
