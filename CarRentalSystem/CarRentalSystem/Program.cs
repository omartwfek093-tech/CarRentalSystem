namespace CarRentalSystem
{
    #region person1

    #region Address
    public class Address
    {
        #region properties
        public string Street { get; private set; }
        public string City { get; private set; }
        #endregion

        public Address(string street, string city)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty.");

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.");

            Street = street.Trim();
            City = city.Trim();
        }

        #region methods
        public string GetFullAddress()
        {
            return $"{Street}, {City}";
        }
        #endregion
    }
    #endregion

    #region Customer
    public class Customer
    {
        #region properties
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string Phone { get; private set; }
        public string LicenseNumber { get; private set; }
        public bool IsVIP { get; private set; }
        public int RentalsCount { get; private set; }
        #endregion

        public Customer(string name, string street, string city, string phone, string licenseNumber, bool isVIP)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty.");

            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("License number cannot be empty.");

            Name = name.Trim();
            Address = new Address(street, city);
            Phone = phone.Trim();
            LicenseNumber = licenseNumber.Trim();
            IsVIP = isVIP;
            RentalsCount = 0;
        }

        #region methods
        public double GetDiscountRate()
        {
            return IsVIP ? 0.15 : 0.0;
        }

        public void IncrementRentals()
        {
            RentalsCount++;
        }

        public void DisplayInfo()
        {
            string customerType = IsVIP ? "VIP" : "Regular";

            Console.WriteLine("----- Customer Info -----");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Address: {Address.GetFullAddress()}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"License Number: {LicenseNumber}");
            Console.WriteLine($"Customer Type: {customerType}");
            Console.WriteLine($"Rentals Count: {RentalsCount}");
            Console.WriteLine("-------------------------");
        }
        #endregion
    }
    #endregion

    #endregion

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

    #region person3

    #region Motorcycle
    public class Motorcycle : Vehicle
    {
        #region attributes
        private int _engineCC;
        #endregion

        #region properties
        public int EngineCC
        {
            get { return _engineCC; }
            set
            {
                if (value > 0)
                {
                    _engineCC = value;
                }
                else
                {
                    _engineCC = 150;
                }
            }
        }
        public bool HasHelmet { get; set; }
        #endregion

        #region Constructor
        public Motorcycle(string plateNumber, string model, string color, double dailyPrice, int engineCC, bool hasHelmet)
            : base(plateNumber, model, color, dailyPrice)
        {
            EngineCC = engineCC;
            HasHelmet = hasHelmet;
        }
        #endregion

        #region Methods
        public override double CalculateRent(int days)
        {
            return (DailyPrice * days) + 50;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            string helmetStatus;
            if (HasHelmet == true)
            {
                helmetStatus = "Yes";
            }
            else
            {
                helmetStatus = "No";
            }

            Console.WriteLine($"Motorcycle Details: {EngineCC} CC , Has Helmet: {helmetStatus}");
        }
        #endregion
    }
    #endregion

    #region Van
    public class Van : Vehicle
    {
        #region attributes
        private double _cargoCapacity;
        #endregion

        #region properties
        public double CargoCapacity
        {
            get { return _cargoCapacity; }
            set
            {
                if (value > 0)
                {
                    _cargoCapacity = value;
                }
                else
                {
                    _cargoCapacity = 500;
                }
            }
        }
        #endregion

        #region Constructor
        public Van(string plateNumber, string model, string color, double dailyPrice, double cargoCapacity)
            : base(plateNumber, model, color, dailyPrice)
        {
            CargoCapacity = cargoCapacity;
        }
        #endregion

        #region Methods
        public override double CalculateRent(int days)
        {
            return (DailyPrice * days) + 300;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Van Details: Cargo Capacity = {CargoCapacity} Kg");
        }
        #endregion
    }
    #endregion

    #endregion

    #region person4

    #region RentalContract
    public class RentalContract
    {
        #region properties
        public int ContractId { get; private set; }
        public Customer Customer { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public int PlannedDays { get; private set; }
        public int ActualDays { get; private set; }
        public bool IsClosed { get; private set; }
        #endregion

        #region Constructor
        public RentalContract(int contractId, Customer customer, Vehicle vehicle, int plannedDays)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "Customer data is required");

            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Vehicle data is required");

            if (plannedDays <= 0)
                throw new ArgumentException("The agreed-upon number of days must be greater than zero", nameof(plannedDays));

            if (!vehicle.IsAvailable)
                throw new InvalidOperationException($"The vehicle is not available now ({vehicle.Model})");

            ContractId = contractId;
            Customer = customer;
            Vehicle = vehicle;
            PlannedDays = plannedDays;
            ActualDays = 0;
            IsClosed = false;

            vehicle.MarkAsRented();
            customer.IncrementRentals();
        }
        #endregion

        #region Methods
        public void CloseContract(int actualDays)
        {
            if (IsClosed)
                throw new InvalidOperationException("The cintract is already locked.");

            if (actualDays < 0)
                throw new ArgumentException("The actualdays must be greater than zero", nameof(actualDays));

            ActualDays = actualDays;
            IsClosed = true;
            Vehicle.MarkAsAvailable();
        }

        public int GetLateDays()
        {
            if (ActualDays > PlannedDays)
                return ActualDays - PlannedDays;

            return 0;
        }

        public void DisplayContract()
        {
            string status = IsClosed ? "Closed" : "Open";
            int lateDays = GetLateDays();

            Console.WriteLine("----- Rental Contract Details -----");
            Console.WriteLine($"Contract ID: {ContractId}");
            Console.WriteLine($"Customer: {Customer.Name}");
            Console.WriteLine($"Vehicle: {Vehicle.Model}");
            Console.WriteLine($"Planned Days: {PlannedDays}");
            Console.WriteLine($"Actual Days: {(IsClosed ? ActualDays.ToString() : "Not returned yet")}");
            Console.WriteLine($"Status: {status}");
            Console.WriteLine($"Late Days: {lateDays}");
            Console.WriteLine("-----------------------------------");
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
