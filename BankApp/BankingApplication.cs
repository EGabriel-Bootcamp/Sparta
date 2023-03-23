using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class BankingApplication
    {
        public List<User> Users;

        public BankingApplication()
        {
            Users = new List<User>();
            // create text if not exist
            if(!File.Exists("users.txt"))
                File.AppendAllText("users.txt", string.Empty, Encoding.UTF8);
            // read the text file
            var lines = File.ReadAllLines("users.txt");
            if(lines != null)
            {
                foreach (var line in lines)
                {
                    var userDetails = line.Split(",");
                    var user = new User
                    {
                        Id = Convert.ToInt32(userDetails[0]),
                        Name = userDetails[1],
                        Email = userDetails[2],
                        Password = userDetails[3],
                        Balance = decimal.Parse(userDetails[4])
                    };
                    Users.Add(user);
                }
            }
        }

     
        public void Start()
        {
            var signUp = false;
            while (true)
            {
                Console.WriteLine("=================================Welcome to the Banking Application!===================================");
            AfterSignUp:
                Console.WriteLine("Enter the number of action you wish to perform: ");
                if (!signUp) Console.WriteLine("1. Sign Up");
                Console.WriteLine("2. Login");
                Console.WriteLine("X. Exit");

                var enteredChar = Console.ReadLine();

                switch (enteredChar)
                {
                    case "1":
                        SignUp();
                        signUp = true;
                        goto AfterSignUp;
                    case "2":
                        Login();
                        return;
                    case "X":
                    case "x":
                        return;
                    default:
                        Console.WriteLine("You have entered wrong character. Try again");
                        goto AfterSignUp;
                }
            }
        }

        private void Withdrawal(string email)
        {
            var user = FindUserByEmail(email);

            Console.WriteLine("Your current balance is: " + user.Balance);

            Console.WriteLine(" Please enter the amount you wish to withdraw: ");
            var amount = decimal.Parse(Console.ReadLine() ?? "0.0");
            DateTime depositTime = DateTime.Now;
            if (user.Balance >= amount)
            {
                user.Balance -= amount;
                Console.WriteLine("Your new balance is: " + user.Balance    + depositTime);
            }
            else
            {
                Console.WriteLine("Insufficient Funds");
            }
            UpdateFile();
        }

        private void Deposit(string email)
        {
           
            var user = FindUserByEmail(email);
            Console.WriteLine("Your current balance is: " + user.Balance);
        DepositStart:
            Console.WriteLine("How much do you wish to deposit?");
            var validDeposit = decimal.TryParse(Console.ReadLine(), out var deposit);
            DateTime depositTime = DateTime.Now;
            if (!validDeposit)
            {
                Console.Write("You have entered invalid deposit amount! Try again: ");
                goto DepositStart;
            }
           
            user.Balance += deposit;
            Console.WriteLine("Your new balance is: " + user.Balance,  $" {depositTime}");
            UpdateFile();
        }

        private void Login()
        {
        Start:
            Console.Write("Please enter your email: ");
            var email = Console.ReadLine();
            var user = FindUserByEmail(email);
            if (user == null)
            {
                Console.WriteLine();
                Console.WriteLine("Email not registered");
                goto Start;
            }
        PasswordStart:
            Console.WriteLine();
            Console.Write("Please enter your password: ");
            var password = Console.ReadLine();
            if (user.Password != password)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid password");
                goto PasswordStart;
            }

            Console.WriteLine($"Welcome, {user.Name}!");
        Action:
            Console.WriteLine();
            Console.WriteLine("Welcome! Enter the number of action to perform: ");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdrawal");
            Console.WriteLine("5. Account Summary");
            Console.WriteLine("X. Exit");
            var action = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(action))
            {
                Console.WriteLine("Invalid action entered");
                goto Action;
            }
            switch (action.ToUpper())
            {
                case "3":
                    Deposit(email);
                    goto Action;
                case "4":
                    Withdrawal(email);
                    goto Action;
                case "5":
                    ShowSummary(email);
                    goto Action;
                case "X":
                    return;
            }
        }

        private void ShowSummary(string email)
        {
            var user = FindUserByEmail(email);
            Console.WriteLine("*****Account Summary******");
            Console.WriteLine();
            Console.WriteLine($"Name:         {user.Name}");
            Console.WriteLine($"Email:         {user.Email}");
            Console.WriteLine($"Balance:         {user.Balance}");
            Console.WriteLine("END");
            Console.WriteLine();


        }

        private void SignUp()
        {
            var newUser = new User();
            while (true)
            {
                Console.WriteLine("Please enter your email");
                var email = Console.ReadLine();

                var user = FindUserByEmail(email);
                if (user != null)
                {
                    Console.WriteLine("Email already registered, please enter different email");
                    continue;
                }
                newUser.Email = email;
                break;
            }

            Console.WriteLine("Please enter your name");
            newUser.Name = Console.ReadLine();

            Console.WriteLine("Please enter your password");
            newUser.Password = Console.ReadLine();

            Save(newUser);
            Console.WriteLine("Congratulations!! Registration complete.");
        }

        private User FindUserByEmail(string email)
        {
            return Users.FirstOrDefault(user => user.Email == email);
        }

        private void Save(User newUser)
        {
            newUser.Id = Users.Count + 1;
            Users.Add(newUser);
            UpdateFile();
        }

        private void UpdateFile() 
        {
            var text = string.Empty;
            foreach (var user in Users)
            {
                text += $"{user.Id},{user.Name},{user.Email},{user.Password},{user.Balance}\n";
            }
            File.WriteAllText("users.txt", text);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; } = 0;
    }
}
