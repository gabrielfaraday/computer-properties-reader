using System;
using computer_check.Services;

namespace computer_check
{
    public class Program
    {
        static void Main(string[] args)
        {
            var computerCheckService = new ComputerCheckService();
            var computer = computerCheckService.CheckUpProperties();
            computerCheckService.SaveComputerToDB(computer);
        }
    }
}
