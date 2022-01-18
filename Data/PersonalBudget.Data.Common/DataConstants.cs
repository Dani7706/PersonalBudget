

namespace PersonalBudget.Data
{
    public class DataConstants
    {
        public class Transfer
        {
            public const double MinCapitalValue = 0.01;
            public const double MaxCapitalValue = 100000000.00;
            public const double MaxQuantity = 1000;
            public const double MinQuantity = 0.01;
            public const string MinDate = "01.01.1950";
            public const double MinUnitPrice = 0.01;
            public const double MaxUnitPrice = 1000000.00;
            public const double MinItemValue = 0.01;
            public const double MaxItemValue = 10000000.00;
        }

        public class Record
        {
            public const double MinCapitalValue = 0.01;
            public const double MaxCapitalValue = 100000000.00;
            public const double MaxQuantity = 1000;
            public const double MinQuantity = 0.01;
            public const string MinDate = "01.01.1950";
            public const string MaxDate = "31.12.2200";
            public const double MinUnitPrice = 0.01;
            public const double MaxUnitPrice = 1000000.00;
            public const double MinItemValue = 0.01;
            public const double MaxItemValue = 10000000.00;
        }

        public class Category
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 20;
        }

        public class FinanceInstitution
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 30;
            public const double MinCapitalValue = 1.00;
            public const double MaxCapitalValue = 100000000.00;
        }

        public class FinanceType
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 20;
        }

        public class Item
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public class MeasureUnit
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 20;
            public const int MinShortNameLength = 1;
            public const int MaxShortNameLength = 10;
        }

        public class Member
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public class Partner
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public class Payment
        {
            public const double MinCapitalValue = 1.00;
            public const double MaxCapitalValue = 100000000.00;
        }

        public class SubCategory
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public class Town
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
            public const int MinCountryNameLength = 3;
            public const int MaxCountryNameLength = 30;
        }

        public class ShoppingCart
        {
            public const int MinCartNameLength = 2;
            public const int MaxCartNameLength = 30;
        }

        public class ProductList
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 30;
            public const double MinCapitalValue = 0.01;
            public const double MaxCapitalValue = 100000000.00;
            public const double MaxQuantity = 1000;
            public const double MinQuantity = 0.01;
            public const double MinUnitPrice = 0.01;
            public const double MaxUnitPrice = 1000000.00;
            public const double MinItemValue = 0.01;
            public const double MaxItemValue = 10000000.00;
        }
    }
}
