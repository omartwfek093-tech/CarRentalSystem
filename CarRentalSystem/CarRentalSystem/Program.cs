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

        #region Constructor
        public Vehicle(string plateNumber, string model, string color, double dailyPrice)
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

            Console.WriteLine($"----- Vehicle Info -----\n" +
                $"PlateNumber: {PlateNumber}\n" +
                $"Model: {Model}\n" +
                $"Color: {Color}\n" +
                $"Daily Price: {DailyPrice} Egp\n" +
                $"Status: {status}" +
                $"-------------------------");
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
            : base(plateNumber, model, color, dailyPrice)
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

    #region person5

    #region Invoice
    public class Invoice
    {
        #region properties
        public int InvoiceId { get; }
        public RentalContract Contract { get; }
        public string PaymentMethod { get; set; }
        public double BaseAmount { get; }
        public double DiscountAmount { get; }
        public double LateFee { get; }
        public double FinalTotal { get; }
        #endregion

        #region constractor
        public Invoice(int invoiceId, RentalContract contract, string paymentmethod)
        {
            InvoiceId = invoiceId;
            Contract = contract;
            PaymentMethod = paymentmethod;

            BaseAmount = contract.Vehicle.CalculateRent(contract.PlannedDays);
            DiscountAmount = BaseAmount * contract.Customer.GetDiscountRate();
            LateFee = contract.GetLateDays() * (contract.Vehicle.DailyPrice * 1.5);
            FinalTotal = BaseAmount - DiscountAmount + LateFee;
        }
        #endregion

        #region Methods
        public void PrintInvoice()
        {
            Console.WriteLine($"Invoice ID: {InvoiceId}");
            Console.WriteLine($"Payment Method: {PaymentMethod}");
            Console.WriteLine($"BaseAmount :{BaseAmount} ");
            Console.WriteLine($"DiscountAmount :{DiscountAmount} ");
            Console.WriteLine($"LateFee :{LateFee} ");
            Console.WriteLine($"FinalTotal :{FinalTotal} ");
        }
        #endregion
    }
    #endregion

    #endregion

    #region person6

    #region Showroom

    public class Showroom
    {
        #region properties

        public List<Vehicle> Vehicles { get; private set; }
        public List<Customer> Customers { get; private set; }
        public List<RentalContract> Contracts { get; private set; }
        public List<Invoice> Invoices { get; private set; }

        #endregion

        #region Constructor

        public Showroom()
        {
            Vehicles = new List<Vehicle>();
            Customers = new List<Customer>();
            Contracts = new List<RentalContract>();
            Invoices = new List<Invoice>();
        }

        #endregion

        #region Methods

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
                Vehicles.Add(vehicle);
        }

        public void AddCustomer(Customer customer)
        {
            if (customer != null)
                Customers.Add(customer);
        }

        public void ShowAvailableVehicles()
        {
            Console.WriteLine("===== Available Vehicles =====");
            foreach (var v in Vehicles)
            {
                if (v.IsAvailable)
                {
                    v.DisplayInfo();
                    Console.WriteLine("-----------------------------------------------");
                }
            }
        }

        public RentalContract StartRental(int contractId, Customer customer, Vehicle vehicle, int days)
        {
            RentalContract contract = new RentalContract(contractId, customer, vehicle, days);
            Contracts.Add(contract);
            return contract;
        }

        public Invoice EndRental(int invoiceId, RentalContract contract, int actualDays, string paymentMethod)
        {
            contract.CloseContract(actualDays);
            Invoice invoice = new Invoice(invoiceId, contract, paymentMethod);
            Invoices.Add(invoice);
            invoice.PrintInvoice();
            return invoice;
        }

        #endregion
    }

    #endregion

    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            Showroom showroom = new Showroom();

            showroom.AddVehicle(new Car("ABC-123", "Toyota Corolla", "White", 800, 5, true));
            showroom.AddVehicle(new Car("KIA-201", "Kia Cerato", "Black", 750, 5, true));
            showroom.AddVehicle(new Car("HYU-305", "Hyundai Elantra", "Silver", 850, 5, true));
            showroom.AddVehicle(new Car("BMW-520", "BMW 520i", "Blue", 2000, 5, true));
            showroom.AddVehicle(new Motorcycle("MOT-456", "Yamaha R3", "Black", 300, 321, true));
            showroom.AddVehicle(new Motorcycle("HON-600", "Honda CBR", "Red", 450, 600, true));
            showroom.AddVehicle(new Van("VAN-789", "Mercedes Sprinter", "Silver", 1200, 1500));
            showroom.AddVehicle(new Van("TOY-900", "Toyota HiAce", "White", 1000, 1200));

            int nextContractId = 1;
            int nextInvoiceId = 101;
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n========= Car Rental System =========");
                Console.WriteLine("1. Show Available Vehicles");
                Console.WriteLine("2. Rent a Vehicle");
                Console.WriteLine("3. Return Vehicle & Print Invoice");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            showroom.ShowAvailableVehicles();
                            break;

                        case "2":
                            showroom.ShowAvailableVehicles();

                            Console.WriteLine("\n--- Enter Your Details ---");
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Street: ");
                            string street = Console.ReadLine();
                            Console.Write("City: ");
                            string city = Console.ReadLine();
                            Console.Write("Phone: ");
                            string phone = Console.ReadLine();
                            Console.Write("License Number: ");
                            string license = Console.ReadLine();
                            Console.Write("Are you a VIP customer? (y/n): ");
                            bool isVip = Console.ReadLine()?.Trim().ToLower() == "y";

                            Customer customer = new Customer(name, street, city, phone, license, isVip);
                            showroom.AddCustomer(customer);

                            Console.Write("\nEnter Vehicle Plate Number (e.g., KIA-201): ");
                            string plate = Console.ReadLine()?.Trim();
                            Vehicle selectedVehicle = showroom.Vehicles.Find(v => v.PlateNumber.Equals(plate, StringComparison.OrdinalIgnoreCase) && v.IsAvailable);

                            if (selectedVehicle == null)
                            {
                                Console.WriteLine("Vehicle not found or not available!");
                                break;
                            }

                            Console.Write("Planned Rental Days: ");
                            int days = int.Parse(Console.ReadLine());

                            RentalContract contract = showroom.StartRental(nextContractId++, customer, selectedVehicle, days);
                            Console.WriteLine("\nRental Successful!");
                            contract.DisplayContract();
                            break;

                        case "3":
                            Console.Write("\nEnter Contract ID: ");
                            int contractId = int.Parse(Console.ReadLine());

                            RentalContract activeContract = showroom.Contracts.Find(c => c.ContractId == contractId && !c.IsClosed);

                            if (activeContract == null)
                            {
                                Console.WriteLine("Active contract not found!");
                                break;
                            }

                            Console.Write("Actual Days Used: ");
                            int actualDays = int.Parse(Console.ReadLine());

                            Console.Write("Payment Method (Cash / Visa): ");
                            string paymentMethod = Console.ReadLine();

                            Console.WriteLine("\n===== Invoice =====");
                            showroom.EndRental(nextInvoiceId++, activeContract, actualDays, paymentMethod);
                            break;

                        case "4":
                            running = false;
                            Console.WriteLine("Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid option, try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
