using System;
using System.Drawing;
using System.Net.NetworkInformation;

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

    #region person2

    #region Vehicle
    public abstract class Vehicle
    {
        private double _dailyPrice;

        #region properties
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public bool IsAvailable { get; private set; }
        public double DailyPrice
        {
            get { return _dailyPrice; }
            set
            {
                if (value > 0)
                {
                    _dailyPrice = value;
                }
                else
                {
                    _dailyPrice = 500;
                }
            }
        }
        #endregion

        public Vehicle(string plateNumber, string model, string color, double dailyPrice)
        {
            DailyPrice = dailyPrice;
            PlateNumber = plateNumber;
            Model = model;
            Color = color;
            IsAvailable = true;
        }

        #region methods
        public void MarkAsRented()
        {
            IsAvailable = false;
        }

        public void MarkAsAvailable()
        {
            IsAvailable = true;
        }

        public abstract double CalculateRent(int days);

        public double CalculateRent(int days, double extraFees)
        {
            return CalculateRent(days) + extraFees;
        }

        public virtual void DisplayInfo()
        {
            string status;
            if (IsAvailable == true)
            {
                status = "Available";
            }
            else
            {
                status = "Rented";
            }

            Console.WriteLine(
                $"PlateNumber: {PlateNumber}\n" +
                $"Model: {Model}\n" +
                $"Color: {Color}\n" +
                $"Daily Price: {DailyPrice} Egp\n" +
                $"Status: {status}");
        }
        #endregion
    }
    #endregion

    #region car
    public class Car : Vehicle
    {
        private int _seatsCount;

        #region properties
        public int SeatsCount
        {
            get { return _seatsCount; }
            set
            {
                if (value <= 5 && value > 0)
                {
                    _seatsCount = value;
                }
                else
                {
                    _seatsCount = 5;
                }
            }
        }
        public bool HasAC { get; set; }
        #endregion

        public Car(string plateNumber, string model, string color, double dailyPrice, int seatsCount, bool hasAC)
            : base(plateNumber, model, color, dailyPrice)
        {
            SeatsCount = seatsCount;
            HasAC = hasAC;
        }

        #region Methods
        public override double CalculateRent(int days)
        {
            return (DailyPrice * days) + 100;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            string AcStatus;
            if (HasAC == true)
            {
                AcStatus = "Yes";
            }
            else
            {
                AcStatus = "No";
            }

            Console.WriteLine($"Car Details: {SeatsCount} Seats , AC: {AcStatus}");
        }
        #endregion
    }
    #endregion

    #endregion

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

            Console.WriteLine();

            Car car1 = new Car(
                plateNumber: "ABC-1234",
                model: "Toyota Corolla",
                color: "Silver",
                dailyPrice: 800,
                seatsCount: 5,
                hasAC: true
            );

            car1.MarkAsRented();
            car1.DisplayInfo();
            Console.WriteLine($"Total Rent (3 days): {car1.CalculateRent(3)} Egp");
        }
    }
}