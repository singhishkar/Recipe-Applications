using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeWPF
{
    // Class to store details about a recipe including its name, ingredients, and steps
    public class Recipe
    {
        // Property to store the name of the recipe
        public string RecipeName { get; set; }

        // Property to store the list of ingredients in the recipe
        public List<IngredientDetails> Ingredients { get; set; }

        // Property to store the list of steps for preparing the recipe
        public List<StepDetails> Steps { get; set; }

        // Constructor to initialize the Recipe object with empty lists for ingredients and steps
        public Recipe()
        {
            Ingredients = new List<IngredientDetails>();
            Steps = new List<StepDetails>();
        }
    }

}
