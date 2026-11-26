using System;

namespace CarRentalSystem
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }

        public Address(string street, string city)
        {
            Street = string.IsNullOrWhiteSpace(street)
                ? throw new ArgumentException("Street cannot be empty.", nameof(street))
                : street.Trim();

            City = string.IsNullOrWhiteSpace(city)
                ? throw new ArgumentException("City cannot be empty.", nameof(city))
                : city.Trim();
        }

        public string GetFullAddress()
        {
            return $"{Street}, {City}";
        }
    }

    public class Customer
    {
        private const double VipDiscountRate = 0.15;
        private const double RegularDiscountRate = 0.0;

        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string Phone { get; private set; }
        public string LicenseNumber { get; private set; }
        public bool IsVIP { get; private set; }
        public int RentalsCount { get; private set; }

        public Customer(
            string name,
            string street,
            string city,
            string phone,
            string licenseNumber,
            bool isVIP = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone number cannot be empty.", nameof(phone));

            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("License number cannot be empty.", nameof(licenseNumber));

            Name = name.Trim();
            Address = new Address(street, city);
            Phone = phone.Trim();
            LicenseNumber = licenseNumber.Trim();
            IsVIP = isVIP;
            RentalsCount = 0;
        }

        public double GetDiscountRate()
        {
            return IsVIP ? VipDiscountRate : RegularDiscountRate;
        }

        public void IncrementRentals()
        {
            RentalsCount++;
        }

        public void DisplayInfo()
        {
            string customerType = IsVIP ? "VIP" : "Regular";
            double discountPercent = GetDiscountRate() * 100;

            Console.WriteLine("=== Customer Details ===");
            Console.WriteLine($"Name           : {Name}");
            Console.WriteLine($"Address        : {Address.GetFullAddress()}");
            Console.WriteLine($"Phone          : {Phone}");
            Console.WriteLine($"License Number : {LicenseNumber}");
            Console.WriteLine($"Customer Type  : {customerType} ({discountPercent}% discount)");
            Console.WriteLine($"Rentals Count  : {RentalsCount}");
            Console.WriteLine("========================");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            
            Customer customer1 = new Customer(
                name: "Nour Hassan",
                street: "15 El-Galaa St.",
                city: "Kafr El-Sheikh",
                phone: "01012345678",
                licenseNumber: "LIC-984512",
                isVIP: true
            );

            customer1.IncrementRentals();
            customer1.IncrementRentals();
            customer1.DisplayInfo();
        }
    }
}