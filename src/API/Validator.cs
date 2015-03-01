using System;
using System.Text.RegularExpressions;


namespace API
{
    /// <summary>
    /// This class provides validations to Person, Member, and Organization objects.
    /// Current fields being validated in the Person object include: birth date, death date, 
    /// first name, last name, email address, and SIN.
    /// 
    /// Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
    /// December 01, 2014
    /// </summary>
    public class Validator
    {   
        /// <summary>
        /// The year of the earliest allowable date that is valid for a birth date or death date.
        /// Any birth date or death date before this date is invalid.
        /// </summary>
        public const int OUTDATED_YEAR = 1900;
        
        
        /// <summary>
        /// The month of the earliest allowable date that is valid for a birth date or death date.
        /// Any birth date or death date before this date is invalid.
        /// </summary>
        public const int OUTDATED_MONTH = 1;
       
        
        /// <summary>
        /// The day of the earliest allowable date that is valid for a birth date or death date.
        /// Any birth date or death date before this date is invalid.
        /// </summary>
        public const int OUTDATED_DATE = 1;
        
        
        /// <summary>
        /// Validate a Person object by validating the birth date, death date, email address, 
        /// first name, last name, and SIN.
        /// </summary>
        /// <param name="p">The Person instance to be validated</param>
        public static void ValidatePerson(Person p)
        {
            DateTime currentTime = DateTime.Now;
            DateTime outDated    = new DateTime(OUTDATED_YEAR, OUTDATED_MONTH, OUTDATED_DATE);

            if (p.BirthDate != null &&
			   (p.BirthDate > currentTime ||
                p.BirthDate < outDated))
            {
                throw new InvalidDateException();
            }

            if (p.DeathDate != null &&
			   (p.DeathDate > currentTime ||
                p.DeathDate < outDated))
            {
                throw new InvalidDateException();
            }

            if (p.Email != null && !IsValidEmail(p.Email))
            {
                throw new InvalidEmailException();
            }

            if (p.Sin != null && !IsValidSin(p.Sin))
            {
                throw new InvalidSinException();
            }

            if (p.FirstName == null      ||
                p.LastName  == null      ||
                !IsValidName(p.LastName) ||
                !IsValidName(p.FirstName))
            {
                throw new InvalidNameException();
            }
        }


        /// <summary>
        /// Validate a Member object.
        /// </summary>
        public static void ValidateMember()
        {
            return;
        }


        /// <summary>
        /// Validate an Organization object.
        /// </summary>
        public static void ValidateOrganization()
        {
            return;
        }

        
        /// <summary>
        /// Validate an email address.
        /// </summary>
        /// <param name="email">Email address to be validated</param>
        /// <returns>True if valid, false if invalid</returns>
        private static bool IsValidEmail(string email)
        {
            // Email can contain characters a–z, A–Z, 0-9, ! # $ % & ‘ * + – / = ? ^ _ ` { | } ~
            // At least one valid local-part character not including a period.
            // in the format of any@any.any(2-4 letters domain)
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@"
                                  + @"(([\-\w]+\.)+[a-zA-Z]{2,4})");

            return regex.Match(email).Success;
        }


        /// <summary>
        /// Validate a name.
        /// </summary>
        /// <param name="name">First name or last name</param>
        /// <returns>True if valid, false if invalid</returns>
        private static bool IsValidName(string name)
        {
            Regex regex = new Regex(@"^[\p{L}\s'.-]+$");

            return regex.Match(name).Success;    
        }


        /// <summary>
        /// Validate the provided Canadian Social Insurance Number (SIN) 
        /// according to the official equation.
        /// </summary>
        /// <param name="sinChars">SIN</param>
        /// <returns>True if valid, false if invalid</returns>
        private static bool IsValidSin(string sinChars)
        {
            int[] sin = new int[9];
            const int odd = 0;
            const int even = 1;
            const int oddMax = 8;
            const int evenMax = 9;
            const int growthRate = 2;
            const int decimalMax = 10;
            int evenTotal = 0;
            int oddTotal = 0;
            int checkDigit;

            int temp = int.Parse(sinChars);

            //parse SIN
            for (int q = 8; q >= 0; q--)
            {
                sin[q] = temp % decimalMax;
                temp = temp / decimalMax;
            }

            checkDigit = sin[oddMax];

            //first alternating set
            for (int i = even; i < evenMax; i += growthRate)
            {
                sin[i] *= 2;
                while (sin[i] > decimalMax)
                {
                    evenTotal += sin[i] % decimalMax;
                    sin[i] /= decimalMax;
                }
                evenTotal += sin[i] % decimalMax;
                for(int x = 0; x < 9; x++)
                 Console.Write(sin[x]);
                Console.WriteLine();
                Console.WriteLine("Eventotal {0}", evenTotal);
            }

            //second alternating set
            for (int j = odd; j < oddMax; j += growthRate)
                oddTotal += sin[j];

            //add sets and validate 1's position
            if ((decimalMax - ((oddTotal + evenTotal) % decimalMax)) != checkDigit)
            {                
                return false;
            }

            return true;
        }
    }
}