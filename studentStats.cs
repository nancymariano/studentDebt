// Nancy Mariano
// January 15th, 2016


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPSC_3200_Project_2;

namespace CPSC_3200_Project_2
{
    /*
    CLASS INVARIANTS:
    - object will hold same student ID as its debtStats objects
    - no checks for empty, must be externally done
    */

    /*
    INTERFACE INVARIANTS:
    - deep copying not supported
    - must have properly called "addDegree" at least once before queries are made
    - do not have to worry about adding too many degrees
    - must be an active degree before dropping it 
    - false information given if you try to access information on an inactive degree
    */


    /*
    IMPLEMENTATION INVARIANTS:
    - degree array will be automatically resized, a student can have
    as many degress as they wish (warning: too many will create an overhead
    - degree array starts out empty, user must provide information for each 
    individually
    - warning given if user has tried to find information when all debtStats
    objects are inactive
    - makes its own debtStats objects with user information 
    */


    class studentStats
    {
        const int maxDegrees = 7;
        const string inactive = "NONE ACTIVE";
        const int moreDegrees = 2;
        const int falseData = -1;

        private int numDegrees;
        private debtStats[] degreeStats;
        int studentID;

        //private string[] degreeNames;

        //preconditions: 
        //postconditions: student ID is stored
        public studentStats(int ID)
        {
            numDegrees = 0;
            this.studentID = ID;
            degreeStats = new debtStats[maxDegrees];            
        }

        //preconditions: 
        //postconditions: array of degree debtStats may be full
        public void addDegree(int startYr, int gradYr, double loans, double grants)
        {
            if (numDegrees >= maxDegrees)
            {
                Array.Resize(ref degreeStats, degreeStats.Length + moreDegrees);
            }

            degreeStats[numDegrees] = new debtStats(studentID, loans, grants, startYr, gradYr);
           
            numDegrees++;
        }


        //preconditions: degreeStats must not be empty
        //postconditions: previously largest burdened debtStat is inactive, all debtStats could be inactive
        public void defaultMostBurden()
        {
            int largest = -1;

            largest = findLargest(); 

            degreeStats[largest].deactivate(); //doesn't matter if already inactive 
        }

        //precondition: most be currently active loans
        //postconditions: 
        public double checkMostBurden()
        {
            int largest = findLargest();

            if (largest != falseData)
                return degreeStats[largest].findcurrentLoans();
            else
                return falseData; 

        }

        private int findLargest()
        {
            int biggest = 0;
             
            for (int i = 0; i < numDegrees; i++)
            {
                if (degreeStats[i].isActive())
                {
                    if (degreeStats[i].findcurrentLoans() > degreeStats[biggest].findcurrentLoans())
                    {
                        biggest = i;
                    }
                }
            }

            return biggest; 
        }
        //preconditions: degree list cannot be empty
        //postconditions: 
        public int numDeactive()
        {

            int deactivations = 0;

            for (int i = 0; i < numDegrees; i++)
            {
                if (degreeStats[i].isActive() == false) // is there a more efficient way to write this? 
                {
                    deactivations++;
                }
            }

            return deactivations;
        }

        //preconditions: degreeStats cannot be empty
        //postconditions:
        public double totalLoans()
        {
            double total = 0;

            for (int i = 0; i < numDegrees; i++)
            {
                if (degreeStats[i].isActive())
                {
                    total += degreeStats[i].findcurrentLoans();
                }
            }

            return total;
        }

        //preconditions: degreeStats cannot be empty
        //postconditions:
        public double findLeastBurden()
        {
            int least = 0;

            for (int i = 0; i < numDegrees; i++)
            {
                if (degreeStats[i].isActive() && (degreeStats[i].findcurrentLoans() <
                    degreeStats[least].findcurrentLoans()))
                    least = i;
            }

            return degreeStats[least].findcurrentLoans();
        }
        

        //preconditions: degreeStats cannot be empty
        //postconditions:
        public bool checkMatriculation(int startYr)
        {
            bool matriculated = false;

            for (int i = 0; i < numDegrees; i++)
            {
                if (degreeStats[i].findMatriculation() == startYr)
                    matriculated = true; 
            }

            return matriculated;
        }

        //preconditions:
        //postcondistions:
        public bool isEmpty()
        {
            return (numDegrees == 0);
        }

        //preconditions: array must not be empty
        //postconditions: 
        public bool anyActive()
        {
            int numActive = 0; 

            for (int i = 0; i < numDegrees; i++)
            {
                if (degreeStats[i].isActive())
                    numActive++;
            }

            return (numActive != 0);
        }
    }
}
