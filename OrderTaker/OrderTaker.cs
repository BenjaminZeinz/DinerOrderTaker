using System;
using System.Collections.Generic;

namespace orderTaker
{
    /// Class library to handle logic for taking resturant orders
    public class Order
    {
        // maps time/DishType to a string description of that dish 
        private Dictionary<KeyValuePair<Enums.TimeOfDay, Enums.DishType>, string> dishesAvailable = new Dictionary<KeyValuePair<Enums.TimeOfDay, Enums.DishType>, string>();

        // List of time/DishType pairs that customer can order multiples of in the same meal.
        // Any time/DishType pairs not in this list only allow 1 in the same meal. 
        private List<KeyValuePair<Enums.TimeOfDay, Enums.DishType>> multipleDishesAllowed = new List<KeyValuePair<Enums.TimeOfDay, Enums.DishType>>();

        public Order()
        {
            // populate dish names to the time/type mappings
            dishesAvailable.Add(new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.morning, Enums.DishType.entree), "eggs");
            dishesAvailable.Add(new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.morning, Enums.DishType.side), "toast");
            dishesAvailable.Add(new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.morning, Enums.DishType.drink), "coffee");
            dishesAvailable.Add(new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.night, Enums.DishType.entree), "steak");
            dishesAvailable.Add(new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.night, Enums.DishType.side), "potato");
            dishesAvailable.Add(new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.night, Enums.DishType.drink), "wine");
            dishesAvailable.Add(new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.night, Enums.DishType.dessert), "cake");

            // populate which pairs allow multiple orders in the same meal
            multipleDishesAllowed.Add( new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.morning, Enums.DishType.drink));
            multipleDishesAllowed.Add( new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(Enums.TimeOfDay.night, Enums.DishType.side));
        }

        public string MakeOrder(string order)
        {
            // base case no input or no csv
            if ( order.Contains(",") == false )
            {
                return "error";
            }

            // Read in time of day
            string[] orderPieces = order.Split(',');
            Enums.TimeOfDay timeOfDay;
            try
            {
                timeOfDay = (Enums.TimeOfDay)Enum.Parse(typeof(Enums.TimeOfDay), orderPieces[0], true);    // 3rd arg takes care of capitalization
            }
            catch(Exception)
            {
                // Invalid time of day. exception overhead for this simple case is not ideal, 
                // but more flexible to future TimeOfDay values than hardcoded switches
                return "error";
            }

            // Base case no items
            if( orderPieces.Length < 2)
            {
                return "error";
            }

            // Main loop. skip 1st element (time). Validate as we go to retain to the point of error.
            // Save items to hash w/count & postpone building string until completed or error. 
            var orderHash = new Dictionary<KeyValuePair<Enums.TimeOfDay, Enums.DishType>, int>();
            bool errorReached = false;
            for(int i = 1; i < orderPieces.Length; i++ )
            {
                // Base Case if item not an int or not a dishtype, error out
                int intItem = new int(); 
                if( int.TryParse(orderPieces[i], out intItem) == false
                    || Enum.IsDefined(typeof(Enums.DishType), intItem) == false )
                {
                    errorReached = true;
                    break;
                }
                Enums.DishType dishType = (Enums.DishType)intItem;

                // If this combo exists in menu
                KeyValuePair<Enums.TimeOfDay, Enums.DishType> dish = new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(timeOfDay, dishType);
                if ( dishesAvailable.ContainsKey(dish) == true )
                {
                    if( orderHash.ContainsKey(dish) == false )
                    {
                        orderHash.Add(dish, 1); 
                    }
                    else if ( multipleDishesAllowed.Contains(dish) == true  ) 
                    {
                        // Already ordered, but OK to duplicate
                        orderHash[dish]++; 
                    }
                    else 
                    {
                        // Already ordered, Not OK to duplicate
                        errorReached = true;
                        break;
                    }
                }
                else 
                {
                    //time/dish combo not available to order. 
                    errorReached = true;
                    break;
                }

            }

            // Inputs collected. translate into english descriptions in correct order. 
            return ConvertOrderHashToString(orderHash, timeOfDay, errorReached);
        }

        private string ConvertOrderHashToString(Dictionary<KeyValuePair<Enums.TimeOfDay, Enums.DishType>, int> orderHash, 
                                                Enums.TimeOfDay timeOfDay, bool error)
        {
            string output = string.Empty;

            // We know time from input, so only loop through the dishTypes in order
            foreach( Enums.DishType dishType in Enum.GetValues(typeof(Enums.DishType)))
            {
                var dish = new KeyValuePair<Enums.TimeOfDay, Enums.DishType>(timeOfDay, dishType);
                if ( orderHash.ContainsKey(dish) )
                {
                    output += dishesAvailable[dish];
                    if( orderHash[dish] > 1 )
                    {
                        output += "(x" + orderHash[dish] + ")";
                    }
                    output += ", ";
                }
            }

            if ( error )
            {
                output += "error";
            }

            // Delete last comma in series before returning. 
            else if( output.EndsWith(", ") == true )
            {
                output = output.Substring(0, output.Length-2);
            }

            return output;
        }

    }
}