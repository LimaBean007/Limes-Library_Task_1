using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library_System
{
    class Validation
    { 
        //Validation method to check if there is something in the string
        public bool checkStringInput(string example)
        {

            bool isString = example.Length > 0;


            return isString;
        }
        //Validation method to check if a string only has numbers
        public bool checkNumberOnly(string example)
        {
            bool isNumber;
            Regex newReg = new Regex("[0-9]");
            isNumber = newReg.IsMatch(example);
            return isNumber;
        }
        //Validation method to check if a string has numbers and other characters
        public bool isNumber(string example)
        {
            bool isNumber;
            Regex newReg = new Regex("^[0-9]*$");
            isNumber = newReg.IsMatch(example);
            return isNumber;
        }
        //Validation method to check if a string has any special characters in it
        public bool checkSpecial(string example)
        {
            Regex newReg = new Regex("[^A-Za-z0-9 ]");
            bool isSpecial = newReg.IsMatch(example);

            return isSpecial;
        }
    }
}

