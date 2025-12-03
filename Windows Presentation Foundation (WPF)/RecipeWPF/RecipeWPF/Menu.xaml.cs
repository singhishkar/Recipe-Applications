using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipeWPF
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
   public partial class Menu : Window
{
    // Dictionaries and lists to store recipe and ingredient details
    Dictionary<IngredientDetails, double> originalQuantities = new Dictionary<IngredientDetails, double>();
    Dictionary<IngredientDetails, double> originalCalories = new Dictionary<IngredientDetails, double>();
    List<Recipe> recipes = new List<Recipe>();
    List<RecipeName> listRecipeName = new List<RecipeName>();
    List<IngredientDetails> ingredients = new List<IngredientDetails>();
    List<StepDetails> listDescription = new List<StepDetails>();
    double scalingFactor = 1.0; // Default scaling factor

    public Menu()
    {
        InitializeComponent();
    }

    // Method to validate quantity input
    private bool ValidateQuantity(string text)
    {
        return double.TryParse(text, out double quantity) && quantity > 0;
    }

    // Method to validate unit input
    private bool ValidateUnit(string text)
    {
        var validUnits = new List<string> { "g", "kg", "mg", "L", "mL", "tablespoon", "teaspoon" };
        return validUnits.Contains(text.ToLower());
    }

    // Method to validate calorie input
    private bool ValidateCalories(string text)
    {
        return double.TryParse(text, out double calories) && calories >= 0;
    }

        // Method to validate all ingredient inputs
        private bool ValidateAllInputs()
        {
            bool isValid = true;

            foreach (var ingredient in ingredients)
            {
                // Validate quantity
                if (!ValidateQuantity(ingredient.textboxQuantity.Text))
                {
                    isValid = false;
                    MessageBox.Show("Please enter a valid quantity for " + ingredient.textboxIngredName.Text, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Validate unit
                if (!ValidateUnit(ingredient.textboxUnit.Text))
                {
                    isValid = false;
                    MessageBox.Show("Please enter a valid unit for " + ingredient.textboxIngredName.Text, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Validate calories
                if (!ValidateCalories(ingredient.textboxCalories.Text))
                {
                    isValid = false;
                    MessageBox.Show("Please enter a valid calorie count for " + ingredient.textboxIngredName.Text, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return isValid;
        }


        private void AddIngreds()
        {
            // Define the font family using a URI to locate the resource
            var amperzandFont = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Amperzand");

            // Create a horizontal stack panel to contain input fields for ingredients
            StackPanel ingredPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 10, 10, 10)
            };

            // TextBox for ingredient name
            TextBox textboxIngredName = new TextBox()
            {
                Width = 120,
                FontFamily = amperzandFont,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap,
                BorderBrush = new SolidColorBrush(Colors.DarkRed),
                BorderThickness = new Thickness(2),
                Height = 24,
                Margin = new Thickness(3, 0, 3, 0)
            };

            // TextBox for ingredient quantity
            TextBox textboxQuantity = new TextBox()
            {
                Width = 120,
                FontFamily = amperzandFont,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap,
                BorderBrush = new SolidColorBrush(Colors.DarkRed),
                BorderThickness = new Thickness(2),
                Height = 24,
                Margin = new Thickness(3, 0, 3, 0)
            };

            // TextBox for ingredient unit
            TextBox textboxUnit = new TextBox()
            {
                Width = 120,
                FontFamily = amperzandFont,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap,
                BorderBrush = new SolidColorBrush(Colors.DarkRed),
                BorderThickness = new Thickness(2),
                Height = 24,
                Margin = new Thickness(3, 0, 3, 0)
            };

            // TextBox for ingredient calories
            TextBox textboxCalories = new TextBox()
            {
                Width = 120,
                FontFamily = amperzandFont,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap,
                BorderBrush = new SolidColorBrush(Colors.DarkRed),
                BorderThickness = new Thickness(2),
                Height = 24,
                Margin = new Thickness(3, 0, 3, 0)
            };

            // ComboBox for selecting food group
            ComboBox comboFoodGroup = new ComboBox()
            {
                Width = 120,
                FontFamily = amperzandFont,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.DarkRed),
                Background = new SolidColorBrush(Colors.Black),
                BorderBrush = new SolidColorBrush(Colors.DarkRed),
                BorderThickness = new Thickness(2),
                Height = 24,
                Margin = new Thickness(3, 0, 3, 0)
            };

            // Adding food groups to the ComboBox
            comboFoodGroup.Items.Add("Vegetables");
            comboFoodGroup.Items.Add("Fruits");
            comboFoodGroup.Items.Add("Grains");
            comboFoodGroup.Items.Add("Protein Foods");
            comboFoodGroup.Items.Add("Dairy");
            comboFoodGroup.Items.Add("Oils and Solid Fats");
            comboFoodGroup.Items.Add("Added Sugars");
            comboFoodGroup.Items.Add("Beverages");

            // Add labels and input fields to the ingredient panel
            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Name: ",
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = amperzandFont,
                FontSize = 13
            });
            ingredPanel.Children.Add(textboxIngredName);

            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Quantity: ",
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = amperzandFont,
                FontSize = 13
            });
            ingredPanel.Children.Add(textboxQuantity);

            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Unit: ",
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = amperzandFont,
                FontSize = 13
            });
            ingredPanel.Children.Add(textboxUnit);

            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Calories: ",
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = amperzandFont,
                FontSize = 13
            });
            ingredPanel.Children.Add(textboxCalories);

            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Food Group: ",
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = amperzandFont,
                FontSize = 13
            });
            ingredPanel.Children.Add(comboFoodGroup);

            // Create an IngredientDetails object and add it to the ingredients list
            ingredients.Add(new IngredientDetails(textboxIngredName, textboxQuantity, textboxUnit, textboxCalories, comboFoodGroup));

            // Add the ingredient panel to the main display panel
            pnlDisplay.Children.Add(ingredPanel);
        }


        private void AddSteps()
        {
            // Define the font family using a URI to locate the resource
            var amperzandFont = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Amperzand");

            // Create a horizontal stack panel to contain input fields for steps
            StackPanel stepPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 10, 10, 10)
            };

            // TextBox for entering step details
            TextBox textboxSteps = new TextBox()
            {
                Width = 120,
                FontFamily = amperzandFont,
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap,
                BorderBrush = new SolidColorBrush(Colors.DarkRed),
                BorderThickness = new Thickness(2),
                Height = 48
            };

            // Add a label and the TextBox for step input to the step panel
            stepPanel.Children.Add(new TextBlock()
            {
                Text = "Step: ",
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = amperzandFont,
                FontSize = 16
            });
            stepPanel.Children.Add(textboxSteps);

            // Create a StepDetails object and add it to the list of step descriptions
            listDescription.Add(new StepDetails(textboxSteps));

            // Add the step panel to the main display panel
            pnlDisplay.Children.Add(stepPanel);
        }


        private void btnEnterIngredsandSteps_Click(object sender, RoutedEventArgs e)
        {
            // Enable the save and display button
            btnsaveanddisplay.IsEnabled = true;

            // Validate number of ingredients input
            int numIngreds;
            if (!int.TryParse(textboxNumIngreds.Text, out numIngreds) || numIngreds <= 0)
            {
                MessageBox.Show("Number of ingredients must be a valid integer greater than zero.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Exit method if input is invalid
            }

            // Validate number of steps input
            int numSteps;
            if (!int.TryParse(textBoxnumSteps.Text, out numSteps) || numSteps <= 0)
            {
                MessageBox.Show("Number of steps must be a valid integer greater than zero.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Exit method if input is invalid
            }

            // Clear previous entries before adding new ones
            pnlDisplay.Children.Clear(); // Clear all UI elements in the display panel
            ingredients.Clear(); // Clear the list of ingredients
            listDescription.Clear(); // Clear the list of step descriptions

            // Add ingredients based on the specified number
            for (int i = 0; i < numIngreds; i++)
            {
                AddIngreds(); // Call the method to add ingredients dynamically
            }

            // Add steps based on the specified number
            for (int i = 0; i < numSteps; i++)
            {
                AddSteps(); // Call the method to add steps dynamically
            }

            // Clear the input textboxes after successful addition
            textboxNumIngreds.Text = string.Empty;
            textBoxnumSteps.Text = string.Empty;

            // Disable the enter ingredients and steps button after inputs are processed
            btnEnterIngredsandSteps.IsEnabled = false;
        }


        // Event handler for the 'Display All Names' button click event
        private void btnDisplayAllNames_Click(object sender, RoutedEventArgs e)
        {
            // Set the text of the textBlockDisplay to the result of DisplayRecipeNamesMethod
            textBlockDisplay.Text = DisplayRecipeNamesMethod();
        }

        // Method to display all recipe names
        private string DisplayRecipeNamesMethod()
        {
            // Create a StringBuilder to construct the display string
            StringBuilder recipeBuilder = new StringBuilder();

            // Add a note about the recipe numbers and a header for the recipe names
            recipeBuilder.AppendLine("PLEASE NOTE RECIPE NUMBERS ARE SUBJECT TO CHANGE BASED ON THEIR ALPHABETICAL ARRANGEMENT!!!");
            recipeBuilder.AppendLine("Recipe Names: \n");

            int nameCount = 1; // Counter for recipe names

            // Loop through each recipe and add its name to the StringBuilder
            foreach (var item in recipes)
            {
                recipeBuilder.AppendLine($"{nameCount}{" : "}{item.RecipeName}");
                nameCount++;
            }

            // Return the constructed string
            return recipeBuilder.ToString();
        }

        // Event handler for the 'Display Recipe' button click event
        private void btnDisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Make the grid for recipe number, the textbox for recipe choice, and the continue button visible
            gridRecipeNumber.Visibility = Visibility.Visible;
            textboxDisplayrecChoice.Visibility = Visibility.Visible;
            btnDisplayRecCont.Visibility = Visibility.Visible;
        }

        // Event handler for the 'Scale' button click event
        private void btnScale_Click(object sender, RoutedEventArgs e)
        {
            // Make the scale button, the grid for recipe number, the grid for scale value, and the next button visible
            btnScale.Visibility = Visibility.Visible;
            gridRecipeNumber.Visibility = Visibility.Visible;
            gridScaleValue.Visibility = Visibility.Visible;
            btnScaleNext.Visibility = Visibility.Visible;
        }


        // Method to scale the quantities and calories of ingredients in a recipe
        public void ScaleRecipe(int recipeIndex, double scalingFactor)
        {
            // Check if the recipeIndex is valid
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                Recipe selectedRecipe = recipes[recipeIndex];

                // Store original values if not already stored
                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    // Store original quantity if not already stored
                    if (!originalQuantities.ContainsKey(ingredient))
                    {
                        double originalQuantity;
                        if (double.TryParse(ingredient.textboxQuantity.Text, out originalQuantity))
                        {
                            originalQuantities.Add(ingredient, originalQuantity);
                        }
                        else
                        {
                            // Handle parsing error or default value as needed
                            originalQuantities.Add(ingredient, 0.0);
                        }
                    }

                    // Store original calorie if not already stored
                    if (!originalCalories.ContainsKey(ingredient))
                    {
                        double originalCalorie;
                        if (double.TryParse(ingredient.textboxCalories.Text, out originalCalorie))
                        {
                            originalCalories.Add(ingredient, originalCalorie);
                        }
                        else
                        {
                            // Handle parsing error or default value as needed
                            originalCalories.Add(ingredient, 0.0);
                        }
                    }

                    // Scale the ingredient
                    double currentQuantity = originalQuantities[ingredient] * scalingFactor;
                    double currentCalories = originalCalories[ingredient] * scalingFactor;

                    // Update the ingredient's quantity and calorie textboxes
                    ingredient.textboxQuantity.Text = currentQuantity.ToString();
                    ingredient.textboxCalories.Text = currentCalories.ToString();
                }
            }
        }

        // Event handler for the 'Reset' button click event
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            // Make the 'Reset Next' button visible
            btnResetNext.Visibility = Visibility.Visible;
        }

        // Event handler for the 'Display Recipe Continued' button click event
        private void btnDisplayRecCont_Click(object sender, RoutedEventArgs e)
        {
            // Check if the entered text is a valid integer
            if (int.TryParse(textboxDisplayrecChoice.Text, out int displayRecChoice))
            {
                // Adjust displayRecChoice from 1-based to 0-based index
                int recipeIndex = displayRecChoice - 1;

                // Check if recipeIndex is within the valid range
                if (recipeIndex >= 0 && recipeIndex < recipes.Count)
                {
                    // Display the selected recipe
                    textBlockDisplay.Text = GetFormattedRecipeDisplay(recipeIndex);
                }
                else
                {
                    // Display error message for invalid recipe choice
                    MessageBox.Show("Recipe choice is out of range. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Display error message for invalid input
                MessageBox.Show("Invalid input. Please enter a valid number for recipe choice.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Hide the 'Display Recipe Continued' button after use
            btnDisplayRecCont.Visibility = Visibility.Hidden;
        }

        // Event handler for the 'Exit' button click event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Exit the application
            Environment.Exit(0);
        }

        // Event handler for the 'Search Ingredient' button click event
        private void SearchIngred_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the search term from the textbox
            string searchIngredient = textboxsearchIngred.Text;

            // Clear the display area
            textBlockDisplay.Text = string.Empty;

            // Filter recipes based on the ingredient name
            List<Recipe> filteredRecipes = recipes.Where(recipe =>
                recipe.Ingredients.Any(ingredient =>
                    ingredient.textboxIngredName.Text.Equals(searchIngredient, StringComparison.OrdinalIgnoreCase))).ToList();

            // Display filtered recipes
            if (filteredRecipes.Count > 0)
            {
                StringBuilder recipeListBuilder = new StringBuilder();
                for (int i = 0; i < filteredRecipes.Count; i++)
                {
                    recipeListBuilder.AppendLine($"{i + 1}.{filteredRecipes[i].RecipeName}");
                }
                textBlockDisplay.Text = recipeListBuilder.ToString();
            }
            else
            {
                // Display message for no recipes found
                MessageBox.Show("No recipe found");
            }
        }

        // Event handler for the 'Search Food Group' button click event
        private void SearchFoodGroup_Click(object sender, RoutedEventArgs e)
        {
            // Check if a food group is selected in the combobox
            if (comboboxsearchFoodGroup.SelectedItem != null)
            {
                // Retrieve the selected food group from the combobox
                ComboBoxItem selectedItem = (ComboBoxItem)comboboxsearchFoodGroup.SelectedItem;
                string searchFoodGroup = selectedItem.Content.ToString();

                // Clear the display area
                textBlockDisplay.Text = string.Empty;

                // Filter recipes based on the selected food group
                List<Recipe> filteredRecipes = recipes.Where(recipe =>
                    recipe.Ingredients.Any(ingredient =>
                        ingredient.comboFoodGroup.Text.Equals(searchFoodGroup, StringComparison.OrdinalIgnoreCase))).ToList();

                // Display filtered recipes
                if (filteredRecipes.Count > 0)
                {
                    StringBuilder recipeListBuilder = new StringBuilder();
                    recipeListBuilder.AppendLine($"Recipes containing {searchFoodGroup}: \n");

                    for (int i = 0; i < filteredRecipes.Count; i++)
                    {
                        recipeListBuilder.AppendLine($"{i + 1}. {filteredRecipes[i].RecipeName}");
                    }

                    textBlockDisplay.Text = recipeListBuilder.ToString();
                }
                else
                {
                    // Display message for no recipes found
                    MessageBox.Show($"No recipes found containing {searchFoodGroup}.");
                }
            }
            else
            {
                // Display message for no food group selected
                MessageBox.Show("Please select a food group to search.");
            }
        }

        // Event handler for the 'Search Max Calories' button click event
        private void SearchMaxCal_Click(object sender, RoutedEventArgs e)
        {
            double maxCalories;

            // Check if the user entered a valid number for max calories
            if (double.TryParse(textboxMaxCal.Text, out maxCalories))
            {
                // Clear the display area
                textBlockDisplay.Text = string.Empty;

                // Filter recipes based on max calories
                List<Recipe> filteredRecipes = new List<Recipe>();

                foreach (var recipe in recipes)
                {
                    double totalCalories = 0;

                    // Calculate total calories for the recipe
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        double calories;
                        if (double.TryParse(ingredient.textboxCalories.Text, out calories))
                        {
                            totalCalories += calories;
                        }
                    }

                    // Check if the total calories are within the max limit
                    if (totalCalories <= maxCalories)
                    {
                        filteredRecipes.Add(recipe);
                    }
                }

                // Display filtered recipes
                if (filteredRecipes.Count > 0)
                {
                    StringBuilder recipeListBuilder = new StringBuilder();
                    recipeListBuilder.AppendLine($"Recipes with Total Calories <= {maxCalories}:\n");

                    int count = 1;
                    foreach (var recipe in filteredRecipes)
                    {
                        recipeListBuilder.AppendLine($"{count}) {recipe.RecipeName}");
                        count++;
                    }

                    textBlockDisplay.Text = recipeListBuilder.ToString();
                }
                else
                {
                    // Display message for no recipes found within the max calorie limit
                    textBlockDisplay.Text = "No recipes found with total calories <= specified limit.";
                }
            }
            else
            {
                // Display message for invalid input for max calories
                MessageBox.Show("Please enter a valid number for maximum calories.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Event handler for the 'Scale Next' button click event
        private void btnScaleNext_Click(object sender, RoutedEventArgs e)
        {
            int displayRecChoice;
            if (int.TryParse(textboxDisplayrecChoice.Text, out displayRecChoice))
            {
                // Check if the entered recipe choice is valid
                if (displayRecChoice >= 1 && displayRecChoice <= recipes.Count)
                {
                    double scaleNum;
                    if (double.TryParse(textboxScaleNum.Text, out scaleNum))
                    {
                        // Scale the selected recipe
                        ScaleRecipe(displayRecChoice - 1, scaleNum);
                    }
                    else
                    {
                        MessageBox.Show("INVALID SCALE NUMBER!!!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Recipe choice is out of range. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number for recipe choice.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Adjust visibility of UI elements after processing
            gridRecipeNumber.Visibility = Visibility.Hidden;
            gridScaleValue.Visibility = Visibility.Hidden;
            btnScaleNext.Visibility = Visibility.Hidden;
        }

        // Event handler for the 'Delete Next' button click event
        private void btnDeleteNext_Click(object sender, RoutedEventArgs e)
        {
            int displayRecChoice;
            if (int.TryParse(textboxDisplayrecChoice.Text, out displayRecChoice))
            {
                // Check if a valid recipe choice is entered
                if (displayRecChoice >= 1 && displayRecChoice <= recipes.Count)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this recipe?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Remove the selected recipe from the list
                        recipes.RemoveAt(displayRecChoice - 1);

                        // Clear any stored scaling data or dictionaries related to this recipe
                        ResetRecipeScaling(displayRecChoice - 1);

                        // Update UI or any other necessary operations
                        RefreshRecipeDisplay(); // Example method to refresh displayed recipes

                        MessageBox.Show("Recipe deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Recipe choice is out of range. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number for recipe choice.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Hide the 'Delete Next' button after processing
            btnDeleteNext.Visibility = Visibility.Hidden;
        }

        // Event handler for the 'Reset Next' button click event
        private void btnResetNext_Click(object sender, RoutedEventArgs e)
        {
            int displayRecChoice;
            if (int.TryParse(textboxDisplayrecChoice.Text, out displayRecChoice))
            {
                // Check if a valid recipe choice is entered
                if (displayRecChoice >= 1 && displayRecChoice <= recipes.Count)
                {
                    // Reset scaling for the selected recipe
                    ResetRecipeScaling(displayRecChoice - 1);
                }
                else
                {
                    MessageBox.Show("Recipe choice is out of range. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number for recipe choice.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Hide the 'Reset Next' button after processing
            btnResetNext.Visibility = Visibility.Hidden;
        }

        // Method to format and display recipe details
        private string GetFormattedRecipeDisplay(int displayRecChoice)
        {
            StringBuilder recipeBuilder = new StringBuilder();
            textBlockDisplay.Text = string.Empty;

            // Check if displayRecChoice is within valid range
            if (displayRecChoice >= 0 && displayRecChoice < recipes.Count)
            {
                recipeBuilder.AppendLine("Recipe Name:");
                var selectedRecipe = recipes[displayRecChoice];

                // Display recipe name and ingredients
                recipeBuilder.AppendLine(selectedRecipe.RecipeName);
                recipeBuilder.AppendLine("Ingredients:\n");

                double totalCalories = 0;
                int count = 1;

                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    // Format ingredient details
                    recipeBuilder.AppendLine($"{count}) {ingredient.textboxQuantity.Text} {ingredient.textboxUnit.Text} of {ingredient.textboxIngredName.Text} (Calories: {ingredient.textboxCalories.Text}) (Food Group: {ingredient.comboFoodGroup.Text})");
                    count++;
                    totalCalories += double.Parse(ingredient.textboxCalories.Text); // Use double.Parse for calories
                }

                // Display total calories and recipe steps
                recipeBuilder.AppendLine($"Total Calories: {totalCalories}");
                recipeBuilder.AppendLine();
                recipeBuilder.AppendLine("Steps:");

                count = 1;
                foreach (var step in selectedRecipe.Steps)
                {
                    recipeBuilder.AppendLine($"{count}) {step.textboxStepDetails.Text}");
                    count++;
                }

                // Provide health advice based on calorie content
                if (totalCalories > 300)
                {
                    recipeBuilder.AppendLine("YOU HAVE EXCEEDED YOUR CALORIE INTAKE OF 300!!!");
                    recipeBuilder.AppendLine("Consuming large amounts of calories can be harmful to your health.");
                    recipeBuilder.AppendLine("Please try scaling the recipe by 0.5.");
                }
                else
                {
                    recipeBuilder.AppendLine("This recipe is under 300 calories, which is a healthy calorie limit for a meal.");
                    recipeBuilder.AppendLine("Eating meals under 300 calories can aid in weight loss and can also improve mental health.");
                }
            }
            else
            {
                recipeBuilder.AppendLine("Recipe not found or invalid selection.");
            }

            return recipeBuilder.ToString();
        }

        // Event handler for the 'Delete' button click event
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Make the 'Delete Next' button visible to confirm deletion
            btnDeleteNext.Visibility = Visibility.Visible;
        }

        // Method to refresh displayed recipes after deletion
        private void RefreshRecipeDisplay()
        {
            // Example method to refresh the displayed recipes in the UI after deletion
            textBlockDisplay.Text = string.Empty; // Clear the display area or update as needed
        }

        // Event handler for the 'Save and Display' button click event
        private void btnsaveanddisplay_Click(object sender, RoutedEventArgs e)
        {
            // Clear the display area and enable search functionalities
            textBlockDisplay.Text = string.Empty;
            SearchIngred.IsEnabled = true;
            SearchFoodGroup.IsEnabled = true;
            SearchMaxCal.IsEnabled = true;

            // Create a new Recipe and add it to the recipes list
            Recipe newRecipe = new Recipe();
            newRecipe.RecipeName = textboxRecipeName.Text;
            newRecipe.Ingredients.AddRange(ingredients);
            newRecipe.Steps.AddRange(listDescription);
            recipes.Add(newRecipe);

            // Clear input fields and display
            textboxRecipeName.Text = string.Empty;
            pnlDisplay.Children.Clear();
            ingredients.Clear();
            listDescription.Clear();

            // Clear the display text block
            textBlockDisplay.Text = string.Empty;

            // Display only the newly added recipe
            DisplayNewRecipe(newRecipe);

            // Enable/disable appropriate buttons and functionality
            btnDisplayAllNames.IsEnabled = true;
            btnDisplayRecipe.IsEnabled = true;
            btnScale.IsEnabled = true;
            btnReset.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnsaveanddisplay.IsEnabled = false;

            // Sort recipes alphabetically
            recipes.Sort((r1, r2) => r1.RecipeName.CompareTo(r2.RecipeName));

            // Enable entering ingredients and steps for a new recipe
            btnEnterIngredsandSteps.IsEnabled = true;
        }


        // Method to update UI elements with original ingredient details
        private void UpdateIngredientUI(IngredientDetails ingredient)
        {
            // Update quantity TextBox
            ingredient.textboxQuantity.Text = ingredient.OriginalQuantity.ToString(); // Update with scaled quantity if necessary

            // Update calories TextBox
            ingredient.textboxCalories.Text = ingredient.OriginalCalories.ToString(); // Update with scaled calories if necessary

            // Update unit ComboBox or TextBox
            ingredient.textboxUnit.Text = ingredient.OrginalUnit; // Update with scaled unit if necessary
        }

        // Method to reset scaling for all ingredients in a recipe
        private void ResetRecipeScaling(int recipeIndex)
        {
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                Recipe selectedRecipe = recipes[recipeIndex];

                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    // Reset quantity if it was scaled
                    if (originalQuantities.ContainsKey(ingredient))
                    {
                        double originalQuantity = originalQuantities[ingredient];
                        ingredient.textboxQuantity.Text = originalQuantity.ToString();
                    }

                    // Reset calories if they were scaled
                    if (originalCalories.ContainsKey(ingredient))
                    {
                        double originalCalorie = originalCalories[ingredient];
                        ingredient.textboxCalories.Text = originalCalorie.ToString();
                    }
                }
            }
        }

        // Method to display a newly added recipe in the text block
        private void DisplayNewRecipe(Recipe recipe)
        {
            StringBuilder recipeBuilder = new StringBuilder();

            // Build recipe details string
            recipeBuilder.AppendLine($"Recipe Name: {recipe.RecipeName}");
            recipeBuilder.AppendLine("Ingredients: \n");

            double totalCalories = 0;
            int count = 1;

            foreach (var ingredient in recipe.Ingredients)
            {
                // Append ingredient details
                recipeBuilder.AppendLine($"{count}) {ingredient.textboxQuantity.Text} {ingredient.textboxUnit.Text} of {ingredient.textboxIngredName.Text} (Calories: {ingredient.textboxCalories.Text}) (Food Group: {ingredient.comboFoodGroup.Text})");
                totalCalories += double.Parse(ingredient.textboxCalories.Text);
                count++;
            }

            // Append total calories and recipe steps
            recipeBuilder.AppendLine($"Total Calories: {totalCalories}");
            recipeBuilder.AppendLine();
            recipeBuilder.AppendLine("Steps:");

            count = 1;
            foreach (var step in recipe.Steps)
            {
                recipeBuilder.AppendLine($"{count}) {step.textboxStepDetails.Text}");
                count++;
            }

            // Add health advice based on calorie content
            if (totalCalories > 300)
            {
                recipeBuilder.AppendLine("YOU HAVE EXCEEDED YOUR CALORIE INTAKE OF 300!!!");
                recipeBuilder.AppendLine("Consuming large amounts of calories can be harmful to your health.");
                recipeBuilder.AppendLine("Please try scaling the recipe by 0.5.");
            }
            else
            {
                recipeBuilder.AppendLine("This recipe is under 300 calories, which is a healthy calorie limit for a meal.");
                recipeBuilder.AppendLine("Eating meals under 300 calories can aid in weight loss and can also improve mental health.");
            }

            // Display the built recipe details in the text block
            textBlockDisplay.Text = recipeBuilder.ToString();
        }

        // Method to update the text block with a list of all recipes
        private void UpdateRecipeListTextBlock()
        {
            StringBuilder recipeListBuilder = new StringBuilder();

            foreach (var recipe in recipes)
            {
                // Append recipe name and ingredient details
                recipeListBuilder.AppendLine($"Recipe Name: {recipe.RecipeName}");
                recipeListBuilder.AppendLine("Ingredients: \n");

                double totalCalories = 0;
                int count = 1;

                foreach (var ingredient in recipe.Ingredients)
                {
                    // Append ingredient details
                    recipeListBuilder.AppendLine($"{count}) {ingredient.textboxQuantity.Text} {ingredient.textboxUnit.Text} of {ingredient.textboxIngredName.Text} (Calories: {ingredient.textboxCalories.Text}) (Food Group: {ingredient.comboFoodGroup.Text})");
                    totalCalories += double.Parse(ingredient.textboxCalories.Text);
                    count++;
                }

                // Append total calories and recipe steps
                recipeListBuilder.AppendLine($"Total Calories: {totalCalories}");
                recipeListBuilder.AppendLine();
                recipeListBuilder.AppendLine("Steps:");

                count = 1;
                foreach (var step in recipe.Steps)
                {
                    recipeListBuilder.AppendLine($"{count}) {step.textboxStepDetails.Text}");
                    count++;
                }

                // Add health advice based on calorie content
                if (totalCalories > 300)
                {
                    recipeListBuilder.AppendLine("YOU HAVE EXCEEDED YOUR CALORIE INTAKE OF 300!!!");
                    recipeListBuilder.AppendLine("Consuming large amounts of calories can be harmful to your health.");
                    recipeListBuilder.AppendLine("Please try scaling the recipe by 0.5.");
                }
                else
                {
                    recipeListBuilder.AppendLine("This recipe is under 300 calories, which is a healthy calorie limit for a meal.");
                    recipeListBuilder.AppendLine("Eating meals under 300 calories can aid in weight loss and can also improve mental health.");
                }

                // Append a blank line between recipes
                recipeListBuilder.AppendLine();
            }

            // Display the built recipe list in the text block
            textBlockDisplay.Text = recipeListBuilder.ToString();
        }

    }
}
