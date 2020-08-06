using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Tools
{
    class Methods
    {
        Entity context = new Entity();

        /// <summary>
        /// all param must be unique, h_a=> health/account number, depending if there is doctor or patient
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pasword"></param>
        /// <param name="h_a"></param>
        /// <param name="jmbg"></param>
        /// <returns></returns>
        public bool ValidateCredentials(string username,string pasword,string h_a,string jmbg)
        {
            List<tblDoctor> doctorList = context.tblDoctors.ToList();
            List<tblPatient> patientList = context.tblPatients.ToList();

            List<string> usernames = new List<string>();
            List<string> h_a_numbers = new List<string>();
            List<string> jmbgs = new List<string>();
            List<string> paswords = new List<string>();

            foreach (tblDoctor item in doctorList)
            {
                usernames.Add(item.Username);
                paswords.Add(item.Pasword);
                jmbgs.Add(item.JMBG);
                h_a_numbers.Add(item.AccountNumber);
            }
            foreach (tblPatient item in patientList)
            {
                usernames.Add(item.Username);
                paswords.Add(item.Pasword);
                jmbgs.Add(item.JMBG);
                h_a_numbers.Add(item.HealthNumber);
            }

            if (!usernames.Contains(username) && !paswords.Contains(pasword) && !jmbgs.Contains(jmbg) && !h_a_numbers.Contains(h_a))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValiationJMBG(string input)
        {

            char[] array = input.ToCharArray();

            int counter = 0;
            //there must be 13 characaters
            for (int i = 0; i < array.Length; i++)
            {
                if (Char.IsDigit(array[i]))
                {
                    counter++;
                }
            }
            if (counter == 13)
            {
                int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                char[] inputToArray = input.ToCharArray(0, 13); // string to char array

                // checking year of birth   
                char[] yearOfBirth = input.ToCharArray(4, 3); // extract digits that represent year
                int helpYear = 100 * (Convert.ToInt32(yearOfBirth[0] - '0')) +
                                 10 * (Convert.ToInt32(yearOfBirth[1] - '0')) +
                                       Convert.ToInt32(yearOfBirth[2] - '0'); // create year of birth

                if (yearOfBirth[0] == '0') // born in XXI century ...
                    helpYear += 2000;
                else
                    helpYear += 1000; // born in XX century

                if (helpYear < 1900) // entered year smaller than 1900
                {
                    MessageBox.Show("Entered year smaller than 1900");
                    return false;
                }
                else
                {
                    if (helpYear > DateTime.Now.Year) // entered year bigger than current year !
                    {
                        MessageBox.Show("Entered year bigger than current year");
                        return false;
                    }
                }

                // checking month of birth
                char[] monthOfBirth = input.ToCharArray(2, 2); // extract digits that represent month
                int helpMonth = 10 * (Convert.ToInt32(monthOfBirth[0] - '0')) +
                                      Convert.ToInt32(monthOfBirth[1] - '0');
                if (helpMonth > 12 || helpMonth < 1) // mont must be <= 12 and > 0 
                {
                    MessageBox.Show("Wrong month of birth (third and fourth digit)");
                    return false;
                }

                // check if february has 29 days
                if (((helpYear % 4) == 0) && (((helpYear % 100) != 0) || ((helpYear % 400) == 0))) // prestupna year
                {
                    daysInMonth[1] = 29; // correction for days in february
                }

                // check if month and days are compatible
                char[] dayOfBirth = input.ToCharArray(0, 2);
                int helpDay = 10 * (Convert.ToInt32(dayOfBirth[0] - '0')) +
                                   Convert.ToInt32(dayOfBirth[1] - '0');

                if (helpDay > daysInMonth[helpMonth - 1] || helpDay < 1)
                {
                    MessageBox.Show("Wrong day of birth (first and second digit)");
                    return false;
                }

            }
            //first and thirs number must be correct
            if (counter == 13 && Convert.ToInt32(array[0].ToString()) < 4 && Convert.ToInt32(array[2].ToString()) < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
