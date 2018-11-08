using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TallyBuddy
{
    public class CreditNote
    {
        private String VoucherNumber;
        private String Date;
        private String Party_Code;
        private String InvoiceNo;
        private String NARRATION;

        private String Credit_LEDGERNAME;
        private String Credit_Amount;



        private String Debit_0_LEDGERNAME;
        private String Debit_0_Amount;

        private String Debit_1_LEDGERNAME;
        private String Debit_1_Amount;

        private String Debit_2_LEDGERNAME;
        private String Debit_2_Amount;



        public CreditNote(String VoucherNumber, String Date, String Party_Code, String NARRATION, String InvoiceNo, String Credit_Amount, String Debit_0_LEDGERNAME, String Debit_0_Amount, String Debit_1_LEDGERNAME, String Debit_1_Amount, String Debit_2_LEDGERNAME, String Debit_2_Amount)
        {
            this.VoucherNumber = VoucherNumber;
            this.Date = Date;
            this.Party_Code = Party_Code;
            this.InvoiceNo = InvoiceNo;
            this.NARRATION = NARRATION;

            this.Credit_LEDGERNAME = Party_Code;
            this.Credit_Amount = Credit_Amount;

            this.Debit_0_LEDGERNAME = Debit_0_LEDGERNAME;
            this.Debit_0_Amount = Debit_0_Amount;

            this.Debit_1_LEDGERNAME = Debit_1_LEDGERNAME;
            this.Debit_1_Amount = Debit_1_Amount;

            this.Debit_2_LEDGERNAME = Debit_2_LEDGERNAME;
            this.Debit_2_Amount = Debit_2_Amount;

        }

        public String getVoucherNumber()
        {
            return this.VoucherNumber;
        }

        public String getInvoiceNo()
        {
            return this.InvoiceNo;
        }




       public String getDate()
        {
            return this.Date;
        }


       public String getParty_Code()
        {
            return this.Party_Code;
        }



       public String getNARRATION()
        {
            return this.NARRATION;
        }

        public String getCredit_LEDGERNAME()
        {
            return this.Credit_LEDGERNAME;
        }

        public String getCredit_Amount()
        {
            return this.Credit_Amount;
        }




        public String getDebit_0_LEDGERNAME()
        {
            return this.Debit_0_LEDGERNAME;
        }
        public String getDebit_0_Amount()
        {
            return this.Debit_0_Amount;
        }


        public String getDebit_1_LEDGERNAME()
        {
            return this.Debit_1_LEDGERNAME;
        }
        public String getDebit_1_Amount()
        {
            return this.Debit_1_Amount;
        }

        public String getDebit_2_LEDGERNAME()
        {
            return this.Debit_2_LEDGERNAME;
        }
        public String getDebit_2_Amount()
        {
            return this.Debit_2_Amount;
        }


    }
}
