namespace CashiersLib
{
    public static class CustomerFactory
    {
        private static readonly char[] SplitChars = {' '};

        public static Customer CreateCustomer(string customerLine)
        {
            if (string.IsNullOrWhiteSpace(customerLine)) return null;
            string[] tokens = customerLine.Split(SplitChars);
            if (tokens.Length < 3) return null;
            int arrivalTime;
            if (!int.TryParse(tokens[1], out arrivalTime)) return null;
            int cartCount;
            if (!int.TryParse(tokens[2], out cartCount)) return null;

            Customer customer;
            if (tokens[0] == "A")
            {
                customer = new CustomerA(arrivalTime, cartCount);
            }
            else if (tokens[0] == "B")
            {
                customer = new CustomerB(arrivalTime, cartCount);
            }
            else return null;

            return customer;
        }
    }
}