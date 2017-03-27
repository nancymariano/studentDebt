// Nancy Mariano
// January 19th, 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_Project_2
{
    /*
    CLASS INVARIANTS:
    - object will hold same matriculation year as its studentStat's debtStats objects
    - no checks for empty, must be externally done
    */

    /*
    INTERFACE INVARIANTS:
    - deep copying not supported
    - must preload an existing studentStats array upon construction
    - must not be empty to perform functionality, external empty checks
    must be made
    */

    /*
    IMPLEMENTATION INVARIANTS:
    - no mutators provided, accessors only
    - hence why aliasing/ shallow copying is done to provide efficiency since
    no mutators are given

    */

    class cohortStats
    {
        const int moreSlots = 5; 
        int matriculation;
        studentStats[] students;
        int numStudents;
        int currentStudent;


        //preconditions: all students must have same matriculation year, array passed 
        // in must not be empty
        //postconditions: students array is now pointing to given array
        public cohortStats(int startYr, studentStats[] a)
        {
            numStudents = a.Length;
            currentStudent = 0; 

            matriculation = startYr;
            students = new studentStats[numStudents];

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].checkMatriculation(startYr))
                {
                    students[i] = a[i];
                    currentStudent++; 
                }

            }
        }

        //preconditions: grad year must match the cohort's 
        //postconditions: new student added, cohort list may be full
        public bool addStudent(studentStats newStudent)
        {
            if (newStudent.checkMatriculation(matriculation))
            {
                if(currentStudent == students.Length)
                {
                    Array.Resize(ref students, students.Length + moreSlots);
                }

                students[currentStudent] = newStudent;
                currentStudent++;

                return true;
            }
            else
                return false; 
        }

        //preconditions: must be students already stored in the class 
        //postconditions:
        public int numDeactive()
        {

            int deactivations = 0;

            for (int i = 0; i < currentStudent; i++)
            {
                deactivations += students[i].numDeactive(); 
            }
            
            return deactivations;
        }

        //preconditions: must be students already stored in the class
        //postconditions:
        public double totalLoans()
        {
            double total = 0;

            for (int i = 0; i < currentStudent; i++)
            {
                total += students[i].totalLoans(); 
            }

            return total;
        }

        //preconditions: must be students stored in the class
        //postconditions:
        public double findMostBurden()
        {
            int largest = 0;

            for (int i = 0; i < currentStudent; i++)
            {
                
                if (students[i].totalLoans() > students[largest].totalLoans())
                    largest = i;
                
            }

            return students[largest].totalLoans();
        }

        //preconditions: must be students already stored in the class
        //postconditions:
        public double findLeastBurden()
        {
            int least = 0;

            for (int i = 0; i < currentStudent; i++)
            {
                if (students[i].totalLoans() < students[least].totalLoans())
                    least = i;
            }

            return students[least].totalLoans();
        }

        //preconditions:
        //postconditions: 
        public bool empty()
        {
            return (currentStudent == 0); 
        }

    }
}
