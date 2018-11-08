using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TallyBuddy
{
    public class SalesInvoice
    {
        private String VoucherNumber;
        private String Date;
        private String Party_Code;
        private String Party_Name;
        private String NARRATION;

        private String Debit_LEDGERNAME;
        private String Debit_Amount;



        private String Credit_0_LEDGERNAME;
        private String Credit_0_Amount;

        private String Credit_1_LEDGERNAME;
        private String Credit_1_Amount;

        private String Credit_2_LEDGERNAME;
        private String Credit_2_Amount;

        public SalesInvoice(String VoucherNumber, String Date, String Party_Code, String Party_Name, String NARRATION, String Debit_Amount, String Credit_0_LEDGERNAME, String Credit_0_Amount, String Credit_1_LEDGERNAME, String Credit_1_Amount, String Credit_2_LEDGERNAME, String Credit_2_Amount)
        {
            this.VoucherNumber = VoucherNumber;
            this.Date = Date;
            this.Party_Code = Party_Code;
            this.Party_Name = Party_Name;
            this.NARRATION = NARRATION;

            this.Debit_LEDGERNAME = Party_Code;
            this.Debit_Amount = Debit_Amount;

            this.Credit_0_LEDGERNAME = Credit_0_LEDGERNAME;
            this.Credit_0_Amount = Credit_0_Amount;

            this.Credit_1_LEDGERNAME = Credit_1_LEDGERNAME;
            this.Credit_1_Amount = Credit_1_Amount;

            this.Credit_2_LEDGERNAME = Credit_2_LEDGERNAME;
            this.Credit_2_Amount = Credit_2_Amount;
        }

        public String getVoucherNumber()
        {
            return this.VoucherNumber;
        }



        public String getDate()
        {
            return this.Date;
        }


        public String getParty_Code()
        {
            return this.Party_Code;
        }


        public String getParty_Name()
        {
            return this.Party_Name;
        }

        public String getNARRATION()
        {
            return this.NARRATION;
        }

        public String getDebit_LEDGERNAME()
        {
            return this.Debit_LEDGERNAME;
        }

        public String getDebit_Amount()
        {
            return this.Debit_Amount;
        }




        public String getCredit_0_LEDGERNAME()
        {
            return this.Credit_0_LEDGERNAME;
        }
        public String getCredit_0_Amount()
        {
            return this.Credit_0_Amount;
        }


        public String getCredit_1_LEDGERNAME()
        {
            return this.Credit_1_LEDGERNAME;
        }
        public String getCredit_1_Amount()
        {
            return this.Credit_1_Amount;
        }


        public String getCredit_2_LEDGERNAME()
        {
            return this.Credit_2_LEDGERNAME;
        }
        public String getCredit_2_Amount()
        {
            return this.Credit_2_Amount;
        }

    }
}
