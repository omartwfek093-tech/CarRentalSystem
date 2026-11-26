using System.Drawing;
using System.Net.NetworkInformation;

namespace CarRentalSystem
{

 #region person2

    #region Vehicle
    public abstract class Vehicle
    {
        #region Attributes
        private double _dailyPrice;
        #endregion

        #region properties
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public bool IsAvailable { get; private set; }
        public double DailyPrice 
        {
            get {  return _dailyPrice; }
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

        #region Constructor
        public Vehicle (string plateNumber, string model, string color, double dailyPrice)
        {
            DailyPrice = dailyPrice;
            PlateNumber = plateNumber;
            Model = model;
            Color = color;
            IsAvailable = true;
        }
        #endregion

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
        #region Attributes
        private int _seatsCount;
        #endregion

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

        #region Constructor
        public Car(string plateNumber, string model, string color, double dailyPrice, int seatsCount, bool hasAC)
            : base( plateNumber, model, color, dailyPrice)
        {
            SeatsCount = seatsCount;
            HasAC = hasAC;
        }
        #endregion

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
           
        }
    }
}
