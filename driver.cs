//Nancy Mariano
//January 20th, 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_Project_2
{
    class p2

    {

        
    /*DESCRIPTION:

        This driver will test all public functionality of the studentStats and cohortsStats classes 
        To space out text, pauses that require hitting return have been 
        placed for less overwhelming readability and minimalization of scrolling back up.   
    */

    /* ASSUMPTIONS:

        Error handling is done via checking if the objects are empty before running tests. 
    */

        const int minLoans = 1;
        const int maxLoans = 1000000000;
        const int minGrants = 1;
        const int maxGrants = 1000000000;
        const int minMatriculation = 2014;
        const int maxMatriculation = 2019;
        const int minGrad = 2019;
        const int maxGrad = 2023;
        const int minStudent = 0;
        const int maxStudent = 25;
        const int maxCohort = 5;
        const int numDegrees = 5; 

        static void Main(string[] args)
        {

            studentStats[] bestKidsEver = new studentStats[maxStudent];
            cohortStats[] bestYearEver = new cohortStats[maxCohort];
            int currentCohort = minMatriculation;

            welcome(); 

            //populates a studentStats array
            for (int i = 0; i < maxStudent; i++)
            {
                bestKidsEver[i] = generateStudent();                 
            }
            //tests each student in studentStats array
            for (int i = 0; i < maxStudent; i++)
            {
                Console.WriteLine("Testing studentStat class for lucky student #" + i);
                testStudentStats(bestKidsEver[i]);
            }

            Console.WriteLine("Press Enter to Continue...");
            pause(); 
            //populates a cohortStats array
            for (int i = 0; i < maxCohort; i++)
            {
                bestYearEver[i] = generateCohort(bestKidsEver, currentCohort);
                currentCohort++; 
            }


            currentCohort = minMatriculation; 

            for (int i = 0; i < maxCohort; i++)
            {
                Console.WriteLine("Testing cohortStats class for lucky cohort of year: " + currentCohort);
                if (bestYearEver[i].empty())
                {
                    Console.WriteLine("Whoops. I guess no one matriculated that year.");
                    currentCohort++; 
                }
                else
                {
                    testCohortStats(bestYearEver[i], currentCohort);
                    currentCohort++;
                }
            }

            goodbye();

            Console.WriteLine("Press Enter to Continue...");
            pause();

        }
        static void welcome()
        {
            Console.WriteLine("Welcome! This is the p2 driver. It will test the functionality of the");
            Console.WriteLine("studentStats and cohortStats classes. Upon testing studentStats, an array");
            Console.WriteLine(" of studentStats objects will be made to test cohortStats Let us begin: ");
            Console.WriteLine(" there will be a total of " + maxStudent + "students in the initial student stats array");
            Console.WriteLine(" which will be sorted into cohortStats objects via matriculation year. Let us begin: "); 


            Console.WriteLine("Press Enter to Continue...");
            pause(); 
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

        }
        static void goodbye()
        {
            Console.WriteLine("Thanks for being patient! Have a nice day!");

        }
        static void pause()
        {
            Console.ReadLine(); 
        }

        static studentStats generateStudent()
        {
            Random debtStatData = new Random();
            int newID = debtStatData.Next();
            
            double[] theseLoans = new double[numDegrees];
            double[] theseGrants = new double[numDegrees];
            int[] theseStartYrs = new int[numDegrees];
            int[] theseGrdYrs = new int[numDegrees]; 
            int tempGrants = 0;
            int tempLoans = 0; 

            
            //generates a random array of possible grants
            for (int i = 0; i < numDegrees; i++)
            { 
                tempGrants = debtStatData.Next(minGrants, maxGrants);
                theseGrants[i] = (double)tempGrants; 
            }
            //generates a random array of possible loans
            for (int i = 0; i < numDegrees; i++)
            {
                tempLoans = debtStatData.Next(minLoans, maxLoans);
                theseLoans[i] = (double)tempLoans; 
            }
            //generates a random array of possible matriculation years
            for (int i = 0; i < numDegrees; i++)
            {
                theseStartYrs[i] = debtStatData.Next(minMatriculation, maxMatriculation + 1); 
            }
            //generates a random array of possble grad years
            for (int i = 0; i < numDegrees; i++)
            {
                theseGrdYrs[i] = debtStatData.Next(minGrad, maxGrad); 
            }
          
            studentStats newStudent = new studentStats(newID); 

            for (int i = 0; i < numDegrees; i++)
            {
                newStudent.addDegree(theseStartYrs[i], theseGrdYrs[i],
                    theseLoans[i], theseGrants[i]);
            }

            return newStudent; 

        }
        static cohortStats generateCohort(studentStats []bestStudents, int cohortYr)
        {
            cohortStats newCohort = new cohortStats(cohortYr, bestStudents);

            return newCohort; 

        }

        static void testStudentStats(studentStats testStudent)
        {
            Random debtStatData = new Random();
            

            int tempLoans = debtStatData.Next(minLoans, maxLoans);
            int tempGrants = debtStatData.Next(minGrants, maxGrants);
            double newGrants = (double)tempGrants;
            double newLoans = (double)tempLoans;
            int newMatriculation = debtStatData.Next(minMatriculation,
                maxMatriculation);
            int newGradYear = debtStatData.Next(minGrad, maxGrad);

            double oldBurden = 0;
            double newBurden = 0; 

       
            Console.WriteLine("Data has already been preloaded. Testing if empty: ");
            if (testStudent.isEmpty())
            {
                Console.WriteLine("True");
            }
            else
                Console.WriteLine("False");

            Console.WriteLine("Test for most burden: " + testStudent.checkMostBurden());

            Console.WriteLine("Test for least burden: " + testStudent.findLeastBurden());

            Console.WriteLine("Test for numDeactive: " + testStudent.numDeactive()); 

            Console.WriteLine("Test for default on biggest loan: ");

            if (testStudent.anyActive())
            {
                testStudent.defaultMostBurden(); 
            }

            Console.WriteLine("numDeactive is now: " + testStudent.numDeactive());

            Console.WriteLine("Previous Biggest Loan now inactive. Biggest Loan is now: " + testStudent.checkMostBurden());

            oldBurden = testStudent.totalLoans();
            Console.WriteLine("Test for total burden:" + oldBurden);

            Console.WriteLine("Test for adding degree:");
            testStudent.addDegree(newMatriculation, newGradYear, newLoans, newGrants);

            newBurden =testStudent.totalLoans(); 

            if (oldBurden < newBurden)
                Console.WriteLine("The loan total has increased, meaning add was successful."); 
            else
                Console.WriteLine("The loan total has not increased, meaning add was unsuccessful.");

        }

        static void testCohortStats(cohortStats testCohort, int cohortYear)
        {
            studentStats testStudent = generateStudent();

            double oldBurden = 0;
            double newBurden = 0;

            Console.WriteLine("Data has already been preloaded. Testing if empty: ");
            if (testCohort.empty())
            {
                Console.WriteLine("True");
            }
            else
                Console.WriteLine("False");

            Console.WriteLine("Test for most burden: " + testCohort.findMostBurden());

            Console.WriteLine("Test for least burden: " + testCohort.findLeastBurden());

            Console.WriteLine("Test for numDeactive: " + testCohort.numDeactive());

            oldBurden = testCohort.totalLoans();

            Console.WriteLine("Test for total Burden: " + oldBurden);

            Console.WriteLine("Test for adding student: ");

            while (testStudent.checkMatriculation(cohortYear) == false)
            {
                testStudent = generateStudent();
            }

            newBurden = testCohort.totalLoans();

            if (newBurden > oldBurden)
                Console.WriteLine("The loan total has increased, meaning add was successful.");
            else
                Console.WriteLine("The loan total has not increased, meaning add was unsuccessful.");

        }
    }
}
