using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Net.Mime;
using System;
using System.IO;
using System.Windows;

public class bankingApplication
{

    public static void Main(string[] args)
    {
       bool bankingHub = true;
       while(bankingHub)
       {
         Homemenu();
       }
    }
   private static void Homemenu()
   {
    Console.WriteLine("WELCOME TO ZENITH BANK PLC");
    Console.WriteLine("1) Create An Account");
    Console.WriteLine("2) Login into your Account");
    Console.WriteLine("3) Close the Application");
    var option = Console.ReadLine();

    if (option == "1")
    {
        createAccount();
        Homemenu();
    }
    else if (option == "2")
    {
        loginAccount();
        Homemenu();
        
    }
    else if(option == "3")
    {
        Environment.Exit(0);
        
    }
    else
    {
        Console.WriteLine("Invalid Selection. Try Again");
        Homemenu();        
    }
   }

    private static void Mainmenu()
    {
        Console.WriteLine("1) Deposit into your Account");
        Console.WriteLine("2) Check your Account Balance");
        Console.WriteLine("3) Check your Account Summary");
        Console.WriteLine("4) Withdraw");
        Console.WriteLine("5) Back to previous menu");

        var result = (Console.ReadLine());

        if (result == "1")
        {
            deposit();
            Mainmenu();
            
        }
        else if (result == "2")
        {
            accountBalance();
            Mainmenu();
            
        }
        else if (result == "3")
        {
            accountSummary();
            Mainmenu();
            
        }
        else if (result == "4")
        {
            Withdraw();
            Mainmenu();
            
        }
        else if (result == "5")
        {
            Homemenu();
        }
        else
        {
            Mainmenu();
        }
        
    }

    private static void createAccount()
    {
        Console.Write("Are sure you want to create an Account?  y/n  ");
        var newUser = Console.ReadLine();

        if (newUser == "y")
        {
            createAccount();
        }
        else if(newUser == "n")
        {
            Homemenu();
        }
        else if(newUser != "y"  || newUser != "n" )
        {
            Console.WriteLine("Input a right character");
            createAccount();
        }
        else{
            Homemenu();
        } 
        Console.WriteLine("PLEASE PROVIDE THE FOLLOWING INFORMATION");
        Console.Write("What is your User Name: ");
        var  myUserName = Console.ReadLine();

        Console.Write("Enter your Email Address: ");
        var myEmail = Console.ReadLine();

        Console.Write("Create a password: ");
        var myPassword = Console.ReadLine();

        Console.Write("What's your age: ");
        int myAge;
        myAge = Convert.ToInt32(Console.ReadLine());


        Console.Write("What is your Phone Number: ");
        int phoneNumber = Convert.ToInt32(Console.ReadLine());


        File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "${userData.txt}"), string.Empty);
        // File.AppendAllLines(Path.Combine(Directory.GetCurrentDirectory(), "userData.txt"));


        // Writing to a file
        try
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("userData.txt", true);
            //Write a line of text
            sw.WriteLine($" \n{myUserName}");
            //Write a second line of text
            sw.WriteLine($"{phoneNumber}");
            sw.WriteLine($"{myEmail}");
            sw.WriteLine($"{myAge}");
            sw.WriteLine($"{myPassword}");


            //Close the file
            sw.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("You have successfully create an account.");
        }
        // Console.WriteLine("Please Confirm you Info: " + myFirstname + " " + myLastname + "  \n" +  phoneNumber);
        // Console.WriteLine("You have successfully create an account");
        Console.ReadLine();

        
    }

    private static void loginAccount()
    {
        string agn;
        do
        {
        Console.Write("Enter your User Name: ");
        var myUserName = Console.ReadLine();
        
        Console.Write("Enter your password: ");
        var myPassword = (Console.ReadLine());

        

        string filepath = ("userData.txt");

        List<string> lines = File.ReadAllLines(filepath).ToList();

        
    
            if (myUserName == lines[0] && myPassword == lines[4])
            {
                // Console.WriteLine(lines[0]);
                Console.WriteLine($"Welcome back Mr. {myUserName}");
                
            }
            else
            {
                Console.WriteLine("Incorrect input");
                Homemenu();
            }

            Console.WriteLine("Do you want to check other services? (y/n)");
            agn = Convert.ToString(Console.ReadLine());

            if (agn == "y")
            {
                Mainmenu();
            }
            else if(agn == "n")
            {
                Homemenu();
            }
            else
            {
                Homemenu();
            }

         }while (agn == "y");
        Console.ReadKey();
    }

    private static void deposit()
    {

        DateTime depositTime = DateTime.Now;
        Console.Write("Enter your User Name: ");
        var myUserName = Console.ReadLine();
        Console.Write("Enter Deposit Amount: \n");
        // int addDeposite = 2
        int deposit = 0;
        var userInput = Console.ReadLine();

        var convertInput = int.TryParse(userInput, out deposit);
        if (convertInput == false)
        {
            Console.WriteLine( "Wrong  user Input " + userInput);
            Mainmenu();
        }
        
        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "${depositeAmount.txt}"), string.Empty);

        FileStream fs = new FileStream("depositeAmount.txt", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        sr.BaseStream.Seek(0, SeekOrigin.Begin);
        var str = sr.ReadLine();
        var preBal = Convert.ToInt32(str);
        var newBal = preBal + Convert.ToInt32(userInput);
        sr.Close();
        fs.Close();

        StreamWriter ds = new StreamWriter("depositeAmount.txt");
        ds.WriteLine(newBal.ToString());
        // Console.WriteLine($"You have successfully deposited: {userInput} ========== {depositTime}");
        Console.WriteLine($"Hello,  {myUserName} \n Your  Account Summary as at ============ {depositTime} ========= is as follows You deposited: {userInput} ========== Current Balance:  {newBal} ==============");
        ds.Close();
        var data = $"Hello,  {myUserName}  Your  Account Summary as at ============ {depositTime} ========= is as follows You deposited: {userInput} ========== Current Balance:  {newBal} ==============";
        WriteInto(data);
    }

     private static void Withdraw()
    {
        DateTime depositTime = DateTime.Now;

        Console.WriteLine("How much do you want to withdraw: ");
        var userWithdraw = Console.ReadLine();
        Console.Write("Enter your User Name: ");
        var myUserName = Console.ReadLine();
        int take =0;
    
        //int newBal


        var convertWithdraw = int.TryParse(userWithdraw, out take);
        if (convertWithdraw == false)
        {
            Console.WriteLine($"Wrong Withdrawal amount  {userWithdraw}");
            Mainmenu();
        }

       FileStream wf = new FileStream("depositeAmount.txt", FileMode.Open, FileAccess.Read);
       StreamReader sw = new StreamReader(wf);
       sw.BaseStream.Seek(0, SeekOrigin.Begin);
       var Wstr = sw.ReadLine();
       var oldCash = Convert.ToInt32(Wstr);
       var subBal = oldCash - Convert.ToInt32(userWithdraw);
       sw.Close();
       wf.Close();
    
        StreamWriter ws = new StreamWriter("depositeAmount.txt");
        ws.WriteLine($"{subBal}");
        Console.WriteLine($"You have successfully Withdraw: {userWithdraw} ========== {depositTime}");

        ws.Close();
        var data = $"Hello,  {myUserName}  Your  Account Summary as at ============ {depositTime} ========= is as follows You deposited: {userWithdraw} ========== Current Balance:  {subBal} ==============";
        WriteInto(data);
        
    }

    private static void accountBalance()
    {
        DateTime depositTime = DateTime.Now;
        Console.Write("Enter your User Name: ");
        var myUserName = Console.ReadLine();
        Console.WriteLine("Do you want to Check your Account Balance? Y/N ");
        var checkBal = Convert.ToString(Console.ReadLine());

       FileStream wf = new FileStream("depositeAmount.txt", FileMode.Open, FileAccess.Read);
       StreamReader sw = new StreamReader(wf);
       sw.BaseStream.Seek(0, SeekOrigin.Begin);
       var Wstr = sw.ReadLine();
       var oldCash = Convert.ToInt32(Wstr);
       var subBal = oldCash ;
       sw.Close();
       wf.Close();
    
        StreamWriter ws = new StreamWriter("depositeAmount.txt");
        ws.WriteLine($"{subBal}");
        
        ws.Close();
        if (checkBal == "y")
        {
        Console.WriteLine($"Hello,  {myUserName} \n Your  Account balance as at ============ {depositTime} ========= is as follows Current Balance:  {subBal} ===========");
        }
        else if(checkBal == "n")
        {
            Mainmenu();
        }
        else{
            Homemenu();
        }
        
        

    }

    public static void accountSummary( )
    {
        DateTime depositTime = DateTime.Now;
       FileStream sf = new FileStream("accountSum.txt", FileMode.Open, FileAccess.Read);
       StreamReader sw = new StreamReader(sf);
    //    sw.BaseStream.Seek(0, SeekOrigin.Begin);
       var Astr = sw.ReadToEnd();

       Console.WriteLine(Astr);

        sw.Close();
        sf.Close();
        
    }

    private static void WriteInto(string data)
    {
        FileStream wf = new FileStream("accountSum.txt", FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(wf);
        // sw.BaseStream.Seek(0, SeekOrigin.Begin);
        sw.WriteLine(data);
        sw.Close();
        wf.Close();
        wf.Dispose();
    }
    
}