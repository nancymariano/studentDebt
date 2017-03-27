// Nancy Mariano
// debtStats.cs
// January 8th, 2016
// REVISION HISTORY: January 15th, 2016
// Constructor simplified and assumptions added for client. No internal checks for
// valid parameters. Internal checks for negative loans and grants have also been
// removed from payLoan, addGrant and addLoan
// Accessor added to grants
// Accessor added to matriculation
// toggleActive() changed to deactivate()


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_Project_2
{


    /* DESCIPTION:
    
        The debtStats class will answer the queries: has graduation year been extended?
        has loan amount increased or decreased? has a "passed debt threshold" been 
        exceeded? It will hold information relevant to a student's accumulated 
        debt and use primitive types of data. It's legal states are activated and 
        not activated. When it is not activated, its pubclic accessor functions will 
        return false information. Each debtStat object's information is dependent upon
        client initialization AT construction. It is anticipated that the debtStat
        object will be used to identify loan burden, loan burden amortized per year,
        and all loans that exceed a given threshold.           
    */

    /* ASSUMPTIONS: 
        
        The client cannot and will not have use for the debtStat's ID after 
        its contruction. The loans for a debtStat's object are a cumulative lump
        sum. The graduation year can change so long as it is not before matriculation. 
        The client can calculate added interest on their own and then add that amount
        to the currentLoans. The client must know ID, initialLoans, grants, matriculation,
        and anticipated graduation date at declaration of the object. Active/ inactive is 
        controlled by the client and is up to them to define. Loan amount increase or 
        decrease is a net comparison e.g. if I start with $10,000 of debt, it increases
        by $100 due to interest and I pay $50, the loan amount will have still increased. If
        I pay $150 it will have decreased. If I pay $100, it will have neither increased nor
        decreased. The client will be aware of the current loans before paying, it will
        be their responsibility to check loan burden before making a payment therefore 
        negative "currentLoans" is allowed. It is the client's responsibility to construct
        each debtStats object with valid information e.g., non-negative loan/ grant amounts,
        graduation date comes after matriculation date. 
        */
    class debtStats
    {
        private int id;
        private double initialLoans; //cumulative loans, a large lump sum
        private double currentLoans;
        private double grants; //grants recieved in total by student, a large lump sum
        private int matriculation; //year student started school
        private int gradDate; //expected graduation year

        private bool active; //switches to inactive when the student graduates or drops out
        private bool gradExtended;

        private const double falseInfo = -1;
        private const double smallestLoan = 0;
        private const double smallestPayment = 0;
        private const double smallestGrant = 0;
        private const int minYear = 1636; // year the first college in the US was founded  
        private const bool successful = true;
        private const bool yes = true;

        // the client has the ability to choose (and must choose)
        // any debtStat object's starting ID, loan amount, grant amount, 
        // matriculation, and intended graduation year. they will also
        // have limited ability to alter loan amount, grant amount and intended
        // grad year

        // decription: constructs the debtStats object
        // preconditions: must be information passed in by client
        // postconditions: a debtStats object will be created with specifications
        // according to the given parameters
        public debtStats(int id, double loans,
            double grants, int matriculation, int gradYear)
        {
            this.id = id;
            this.initialLoans = loans;
            this.currentLoans = loans;
            this.grants = grants;
            this.matriculation = matriculation;
            this.gradDate = gradYear;

            active = yes;  //at first assumed that the student is currently enrolled
            gradExtended = !yes;
        }

        // description: will add to the currentLoan amount 
        // postconditions: currentLoan will be greater than it was previously if 
        // newLoan is > 0, otherwise it will be unchanged and return unsuccessful
        public bool addLoan(double newLoan)
        {
            if (active)
            {
                currentLoans += newLoan;
                return successful;
            }
            else
                return !successful;
        }

        // description: allows client to decrease loan amount
        // postconditions: currentLoan will be smaller than it was previously if
        // amountPaid > 0, otherwise it will be unchanged and return unsuccessful
        public bool payLoan(double amountPaid)
        {
            if (active)
            {
                currentLoans -= amountPaid;
                return successful;
            }
            else
                return !successful;
        }

        // description: allows client to add a grant to the lump sum of grants
        // postconditions: currentLoan will be smaller than it was previously if
        // amountPaid > 0, otherwise it will be unchanged and return unsuccessful
        public bool addGrant(double newGrant)
        {
            if (active)
            {
                grants += newGrant;
                return successful;
            }
            else
                return !successful;
        }

        // description: allows client to update graduation year
        // postconditions: if passed in newYear is > matriculation year,
        // it will update the currently stored anticipated graduation, if passed in
        // newYear is > current Grade Year, it will switch gradExtended to true
        public bool changeGradYear(int newYear)
        {
            if (active)
            {
                if (newYear >= gradDate)
                {
                    gradExtended = yes;
                }
                gradDate = newYear;

                return successful;
            }
            else
                return !successful;
        }

        // description: allows client to look at the currentLoan amount
        // preconditions: the object must be active
        public double findcurrentLoans()
        {
            if (active)
            {
                return currentLoans;
            }
            else
                return falseInfo;
        }

        // description: allows client to check if the graduation year has been extended
        // for this particular person
        // preconditions: the object must be active
        public bool gradYearExtended()
        {
            if (active)
                return gradExtended;
            else
                return !gradExtended; // return whatever gradChanged isn't 
        }

        // description: allows client to check if the loan amount has been increased
        // preconditions: the object must be active
        public bool checkLoanIncrease()
        {
            if (active)
                return currentLoans > initialLoans;
            else
                return !(currentLoans > initialLoans);
        }

        // description: allows client to check if the loan amount has decreased
        // preconditions: the object must be active
        public bool checkLoanDecrease()
        {
            if (active)
                return (currentLoans < initialLoans);
            else
                return !(currentLoans < initialLoans);
        }

        // description: allows client to check if the debt threshold has been exceeded
        // preconditions: a threshold is passed in and the object is active
        // postconitions: will return false information if inactive, and a comparison to the threshold
        // if the object is active
        public bool threshExceeded(double threshold)
        {
            if (active)
                return (currentLoans > threshold);
            else
                return !(currentLoans > threshold);
        }

        // precondition: must currently be active
        // postcondition: state is changed
        public void deactivate()
        {
            if (active)
                active = !active;
        }

        public bool isActive()
        {
            return active;
        }

        public double findCurrentGrants()
        {
            if (active)
                return grants;
            else
                return falseInfo; 
        }
        public int findMatriculation()
        {
            return matriculation;
        }
    }
}