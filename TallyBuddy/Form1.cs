using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;

namespace TallyBuddy
{
    public partial class Form1 : Form
    {
        SqlConnection Source;

        SqlCommand Retrivecmd;
        DataTable SalesInvoices;
        String companyName = "", serviceaddress = "", connectionString = "",city_id="",CRPrefixCode="";

        int AltCount = 0, CreatedCount = 0, ErrorCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }


        public String CreditNoteCreateXml(CreditNote creditNote) // request xml and response for ledger creation
        {
            String CreditNoteResponse = "";

            try
            {
                String xmlstc = "";
                xmlstc = xmlstc + "<ENVELOPE>";
                xmlstc = xmlstc + "<HEADER>";
                xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>";
                xmlstc = xmlstc + "</HEADER>";
                xmlstc = xmlstc + "<BODY>";
                xmlstc = xmlstc + "<IMPORTDATA>";
                xmlstc = xmlstc + "<REQUESTDESC>";
                xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>";

                xmlstc = xmlstc + "<STATICVARIABLES>";
                xmlstc = xmlstc + "<SVCURRENTCOMPANY>" + companyName + "</SVCURRENTCOMPANY>";
                xmlstc = xmlstc + "</STATICVARIABLES>";
                xmlstc = xmlstc + "</REQUESTDESC>";
                xmlstc = xmlstc + "<REQUESTDATA>";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=\"TallyUDF\">";
                xmlstc = xmlstc + "<VOUCHER REMOTEID=\"" + creditNote.getVoucherNumber() + creditNote.getDate() + "\" VCHTYPE=\"Credit Note\" ACTION=\"Create\" OBJVIEW=\"Accounting Voucher View\">";
                xmlstc = xmlstc + "<OLDAUDITENTRYIDS.LIST TYPE=\"Number\">";
                xmlstc = xmlstc + "<OLDAUDITENTRYIDS>-1</OLDAUDITENTRYIDS>";
                xmlstc = xmlstc + "</OLDAUDITENTRYIDS.LIST>";
                xmlstc = xmlstc + "<DATE>" + creditNote.getDate() + "</DATE>";
                xmlstc = xmlstc + "<NARRATION>" + creditNote.getNARRATION() + "</NARRATION>";
                xmlstc = xmlstc + "<VOUCHERTYPENAME>Credit Note</VOUCHERTYPENAME>";
                xmlstc = xmlstc + "<VOUCHERNUMBER>" + creditNote.getVoucherNumber() + "</VOUCHERNUMBER>";
                xmlstc = xmlstc + "<PARTYLEDGERNAME>" + creditNote.getParty_Code() + "</PARTYLEDGERNAME>";
                xmlstc = xmlstc + "<CSTFORMISSUETYPE/>";
                xmlstc = xmlstc + "<CSTFORMRECVTYPE/>";
                xmlstc = xmlstc + "<FBTPAYMENTTYPE>Default</FBTPAYMENTTYPE>";
                xmlstc = xmlstc + "<PERSISTEDVIEW>Accounting Voucher View</PERSISTEDVIEW>";
                xmlstc = xmlstc + "<BASICDATETIMEOFREMOVAL>" + creditNote.getDate() + "</BASICDATETIMEOFREMOVAL>";
                xmlstc = xmlstc + "<VCHGSTCLASS/>";
                xmlstc = xmlstc + "<ENTEREDBY>sunanda</ENTEREDBY>";
                xmlstc = xmlstc + " <DIFFACTUALQTY>No</DIFFACTUALQTY>";
                xmlstc = xmlstc + "<ISMSTFROMSYNC>No</ISMSTFROMSYNC>";
                xmlstc = xmlstc + "<ASORIGINAL>No</ASORIGINAL>";
                xmlstc = xmlstc + "<AUDITED>No</AUDITED>";
                xmlstc = xmlstc + "<FORJOBCOSTING>No</FORJOBCOSTING>";
                xmlstc = xmlstc + "<ISOPTIONAL>No</ISOPTIONAL>";
                xmlstc = xmlstc + "<EFFECTIVEDATE>" + creditNote.getDate() + "</EFFECTIVEDATE>";
                xmlstc = xmlstc + "<USEFOREXCISE>No</USEFOREXCISE>";
                xmlstc = xmlstc + "<ISFORJOBWORKIN>No</ISFORJOBWORKIN>";
                xmlstc = xmlstc + "<ALLOWCONSUMPTION>No</ALLOWCONSUMPTION>";
                xmlstc = xmlstc + "<USEFORINTEREST>No</USEFORINTEREST>";
                xmlstc = xmlstc + "<USEFORGAINLOSS>No</USEFORGAINLOSS>";
                xmlstc = xmlstc + "<USEFORGODOWNTRANSFER>No</USEFORGODOWNTRANSFER>";
                xmlstc = xmlstc + "<USEFORCOMPOUND>No</USEFORCOMPOUND>";
                xmlstc = xmlstc + "<USEFORSERVICETAX>No</USEFORSERVICETAX>";
                xmlstc = xmlstc + "<ISEXCISEVOUCHER>No</ISEXCISEVOUCHER>";
                xmlstc = xmlstc + "<EXCISETAXOVERRIDE>No</EXCISETAXOVERRIDE>";
                xmlstc = xmlstc + "<USEFORTAXUNITTRANSFER>No</USEFORTAXUNITTRANSFER>";
                xmlstc = xmlstc + "<EXCISEOPENING>No</EXCISEOPENING>";
                xmlstc = xmlstc + "<USEFORFINALPRODUCTION>No</USEFORFINALPRODUCTION>";
                xmlstc = xmlstc + "<ISTDSOVERRIDDEN>No</ISTDSOVERRIDDEN>";
                xmlstc = xmlstc + "<ISTCSOVERRIDDEN>No</ISTCSOVERRIDDEN>";
                xmlstc = xmlstc + "<ISTDSTCSCASHVCH>No</ISTDSTCSCASHVCH>";
                xmlstc = xmlstc + "<INCLUDEADVPYMTVCH>No</INCLUDEADVPYMTVCH>";
                xmlstc = xmlstc + "<ISSUBWORKSCONTRACT>No</ISSUBWORKSCONTRACT>";
                xmlstc = xmlstc + "<ISVATOVERRIDDEN>No</ISVATOVERRIDDEN>";
                xmlstc = xmlstc + "<IGNOREORIGVCHDATE>No</IGNOREORIGVCHDATE>";
                xmlstc = xmlstc + "<ISSERVICETAXOVERRIDDEN>No</ISSERVICETAXOVERRIDDEN>";
                xmlstc = xmlstc + "<ISISDVOUCHER>No</ISISDVOUCHER>";
                xmlstc = xmlstc + "<ISEXCISEOVERRIDDEN>No</ISEXCISEOVERRIDDEN>";
                xmlstc = xmlstc + "<ISEXCISESUPPLYVCH>No</ISEXCISESUPPLYVCH>";
                xmlstc = xmlstc + "<ISGSTOVERRIDDEN>No</ISGSTOVERRIDDEN>";
                xmlstc = xmlstc + "<GSTNOTEXPORTED>No</GSTNOTEXPORTED>";
                xmlstc = xmlstc + "<ISVATPRINCIPALACCOUNT>No</ISVATPRINCIPALACCOUNT>";
                xmlstc = xmlstc + "<ISSHIPPINGWITHINSTATE>No</ISSHIPPINGWITHINSTATE>";
                xmlstc = xmlstc + "<ISCANCELLED>No</ISCANCELLED>";
                xmlstc = xmlstc + "<HASCASHFLOW>No</HASCASHFLOW>";
                xmlstc = xmlstc + "<ISPOSTDATED>No</ISPOSTDATED>";
                xmlstc = xmlstc + "<USETRACKINGNUMBER>No</USETRACKINGNUMBER>";
                xmlstc = xmlstc + "<ISINVOICE>No</ISINVOICE>";
                xmlstc = xmlstc + "<MFGJOURNAL>No</MFGJOURNAL>";
                xmlstc = xmlstc + "<HASDISCOUNTS>No</HASDISCOUNTS>";
                xmlstc = xmlstc + "<ASPAYSLIP>No</ASPAYSLIP>";
                xmlstc = xmlstc + "<ISCOSTCENTRE>No</ISCOSTCENTRE>";
                xmlstc = xmlstc + "<ISSTXNONREALIZEDVCH>No</ISSTXNONREALIZEDVCH>";
                xmlstc = xmlstc + "<ISEXCISEMANUFACTURERON>No</ISEXCISEMANUFACTURERON>";
                xmlstc = xmlstc + "<ISBLANKCHEQUE>No</ISBLANKCHEQUE>";
                xmlstc = xmlstc + "<ISVOID>No</ISVOID>";
                xmlstc = xmlstc + "<ISONHOLD>No</ISONHOLD>";
                xmlstc = xmlstc + "<ORDERLINESTATUS>No</ORDERLINESTATUS>";
                xmlstc = xmlstc + "<VATISAGNSTCANCSALES>No</VATISAGNSTCANCSALES>";
                xmlstc = xmlstc + "<VATISPURCEXEMPTED>No</VATISPURCEXEMPTED>";
                xmlstc = xmlstc + "<ISVATRESTAXINVOICE>No</ISVATRESTAXINVOICE>";
                xmlstc = xmlstc + "<VATISASSESABLECALCVCH>No</VATISASSESABLECALCVCH>";
                xmlstc = xmlstc + "<ISVATDUTYPAID>Yes</ISVATDUTYPAID>";
                xmlstc = xmlstc + "<ISDELIVERYSAMEASCONSIGNEE>No</ISDELIVERYSAMEASCONSIGNEE>";
                xmlstc = xmlstc + "<ISDISPATCHSAMEASCONSIGNOR>No</ISDISPATCHSAMEASCONSIGNOR>";
                xmlstc = xmlstc + "<ISDELETED>No</ISDELETED>";
                xmlstc = xmlstc + "<CHANGEVCHMODE>No</CHANGEVCHMODE>";

                xmlstc = xmlstc + "<EXCLUDEDTAXATIONS.LIST>      </EXCLUDEDTAXATIONS.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>      </OLDAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>      </ACCOUNTAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<AUDITENTRIES.LIST>      </AUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>      </DUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<SUPPLEMENTARYDUTYHEADDETAILS.LIST>      </SUPPLEMENTARYDUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEDELNOTES.LIST>      </INVOICEDELNOTES.LIST>";
                xmlstc = xmlstc + "<INVOICEORDERLIST.LIST>      </INVOICEORDERLIST.LIST>";
                xmlstc = xmlstc + "<INVOICEINDENTLIST.LIST>      </INVOICEINDENTLIST.LIST>";
                xmlstc = xmlstc + "<ATTENDANCEENTRIES.LIST>      </ATTENDANCEENTRIES.LIST>";
                xmlstc = xmlstc + "<ORIGINVOICEDETAILS.LIST>      </ORIGINVOICEDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEEXPORTLIST.LIST>      </INVOICEEXPORTLIST.LIST>";
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRYIDS.LIST TYPE=\"Number\">";
                xmlstc = xmlstc + "<OLDAUDITENTRYIDS>-1</OLDAUDITENTRYIDS>";
                xmlstc = xmlstc + "</OLDAUDITENTRYIDS.LIST>";
                xmlstc = xmlstc + "<LEDGERNAME>" + creditNote.getCredit_LEDGERNAME() + "</LEDGERNAME>";
                xmlstc = xmlstc + "<GSTCLASS/>";
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>";
                xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>";
                xmlstc = xmlstc + "<ISPARTYLEDGER>Yes</ISPARTYLEDGER>";
                xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>No</ISLASTDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<AMOUNT>" + creditNote.getCredit_Amount() + "</AMOUNT>";
                xmlstc = xmlstc + "<VATEXPAMOUNT>" + creditNote.getCredit_Amount() + "</VATEXPAMOUNT>";
                xmlstc = xmlstc + "<SERVICETAXDETAILS.LIST>       </SERVICETAXDETAILS.LIST>";
                xmlstc = xmlstc + "<BANKALLOCATIONS.LIST>       </BANKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<NAME>" + creditNote.getInvoiceNo() + "</NAME>";
                xmlstc = xmlstc + "<BILLTYPE>Agst Ref</BILLTYPE>";
                xmlstc = xmlstc + "<TDSDEDUCTEEISSPECIALRATE>No</TDSDEDUCTEEISSPECIALRATE>";
                xmlstc = xmlstc + "<AMOUNT>" + creditNote.getCredit_Amount() + "</AMOUNT>";
                xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>        </INTERESTCOLLECTION.LIST>";
                xmlstc = xmlstc + "<STBILLCATEGORIES.LIST>        </STBILLCATEGORIES.LIST>";
                xmlstc = xmlstc + "</BILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>       </INTERESTCOLLECTION.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>       </OLDAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>       </ACCOUNTAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<AUDITENTRIES.LIST>       </AUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<INPUTCRALLOCS.LIST>       </INPUTCRALLOCS.LIST>";
                xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>       </DUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEDUTYHEADDETAILS.LIST>       </EXCISEDUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>       </RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<SUMMARYALLOCS.LIST>       </SUMMARYALLOCS.LIST>";
                xmlstc = xmlstc + "<STPYMTDETAILS.LIST>       </STPYMTDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEPAYMENTALLOCATIONS.LIST>       </EXCISEPAYMENTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXBILLALLOCATIONS.LIST>       </TAXBILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXOBJECTALLOCATIONS.LIST>       </TAXOBJECTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TDSEXPENSEALLOCATIONS.LIST>       </TDSEXPENSEALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<VATSTATUTORYDETAILS.LIST>       </VATSTATUTORYDETAILS.LIST>";
                xmlstc = xmlstc + "<COSTTRACKALLOCATIONS.LIST>       </COSTTRACKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<REFVOUCHERDETAILS.LIST>       </REFVOUCHERDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEWISEDETAILS.LIST>       </INVOICEWISEDETAILS.LIST>";
                xmlstc = xmlstc + "<VATITCDETAILS.LIST>       </VATITCDETAILS.LIST>";
                xmlstc = xmlstc + "<ADVANCETAXDETAILS.LIST>       </ADVANCETAXDETAILS.LIST>";
                xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRYIDS.LIST TYPE=\"Number\">";
                xmlstc = xmlstc + "<OLDAUDITENTRYIDS>-1</OLDAUDITENTRYIDS>";
                xmlstc = xmlstc + "</OLDAUDITENTRYIDS.LIST>";
                xmlstc = xmlstc + "<LEDGERNAME>" + creditNote.getDebit_0_LEDGERNAME() + "</LEDGERNAME>";
                xmlstc = xmlstc + "<GSTCLASS/>";
                xmlstc = xmlstc + "<GSTOVRDNNATURE>Sales Taxable</GSTOVRDNNATURE>";
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>";
                xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>";
                xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>";
                xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>Yes</ISLASTDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<AMOUNT>-" + creditNote.getDebit_0_Amount() + "</AMOUNT>";
                xmlstc = xmlstc + "<VATEXPAMOUNT>-" + creditNote.getDebit_0_Amount() + "</VATEXPAMOUNT>";
                xmlstc = xmlstc + "<SERVICETAXDETAILS.LIST>       </SERVICETAXDETAILS.LIST>";
                xmlstc = xmlstc + "<BANKALLOCATIONS.LIST>       </BANKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>       </BILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>       </INTERESTCOLLECTION.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>       </OLDAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>       </ACCOUNTAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<AUDITENTRIES.LIST>       </AUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<INPUTCRALLOCS.LIST>       </INPUTCRALLOCS.LIST>";
                xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>       </DUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEDUTYHEADDETAILS.LIST>       </EXCISEDUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<GSTRATEDUTYHEAD>Central Tax</GSTRATEDUTYHEAD>";
                xmlstc = xmlstc + "<GSTRATEVALUATIONTYPE>Based on Value</GSTRATEVALUATIONTYPE>";
                xmlstc = xmlstc + "<GSTRATE> 6</GSTRATE>";
                xmlstc = xmlstc + "</RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<GSTRATEDUTYHEAD>State Tax</GSTRATEDUTYHEAD>";
                xmlstc = xmlstc + "<GSTRATEVALUATIONTYPE>Based on Value</GSTRATEVALUATIONTYPE>";
                xmlstc = xmlstc + "<GSTRATE> 6</GSTRATE>";
                xmlstc = xmlstc + "</RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<GSTRATEDUTYHEAD>Integrated Tax</GSTRATEDUTYHEAD>";
                xmlstc = xmlstc + "<GSTRATEVALUATIONTYPE>Based on Value</GSTRATEVALUATIONTYPE>";
                xmlstc = xmlstc + "</RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<GSTRATEDUTYHEAD>Cess</GSTRATEDUTYHEAD>";
                xmlstc = xmlstc + "<GSTRATEVALUATIONTYPE>Based on Value</GSTRATEVALUATIONTYPE>";
                xmlstc = xmlstc + "</RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<SUMMARYALLOCS.LIST>       </SUMMARYALLOCS.LIST>";
                xmlstc = xmlstc + "<STPYMTDETAILS.LIST>       </STPYMTDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEPAYMENTALLOCATIONS.LIST>       </EXCISEPAYMENTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXBILLALLOCATIONS.LIST>       </TAXBILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXOBJECTALLOCATIONS.LIST>       </TAXOBJECTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TDSEXPENSEALLOCATIONS.LIST>       </TDSEXPENSEALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<VATSTATUTORYDETAILS.LIST>       </VATSTATUTORYDETAILS.LIST>";
                xmlstc = xmlstc + "<COSTTRACKALLOCATIONS.LIST>       </COSTTRACKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<REFVOUCHERDETAILS.LIST>       </REFVOUCHERDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEWISEDETAILS.LIST>       </INVOICEWISEDETAILS.LIST>";
                xmlstc = xmlstc + "<VATITCDETAILS.LIST>       </VATITCDETAILS.LIST>";
                xmlstc = xmlstc + "<ADVANCETAXDETAILS.LIST>       </ADVANCETAXDETAILS.LIST>";
                xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRYIDS.LIST TYPE=\"Number\">";
                xmlstc = xmlstc + "<OLDAUDITENTRYIDS>-1</OLDAUDITENTRYIDS>";
                xmlstc = xmlstc + "</OLDAUDITENTRYIDS.LIST>";
                xmlstc = xmlstc + "<LEDGERNAME>" + creditNote.getDebit_1_LEDGERNAME() + "</LEDGERNAME>";
                xmlstc = xmlstc + "<GSTCLASS/>";
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>";
                xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>";
                xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>";
                xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>Yes</ISLASTDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<AMOUNT>-" + creditNote.getDebit_1_Amount() + "</AMOUNT>";
                xmlstc = xmlstc + "<VATEXPAMOUNT>-" + creditNote.getDebit_1_Amount() + "</VATEXPAMOUNT>";
                xmlstc = xmlstc + "<SERVICETAXDETAILS.LIST>       </SERVICETAXDETAILS.LIST>";
                xmlstc = xmlstc + "<BANKALLOCATIONS.LIST>       </BANKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>       </BILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>       </INTERESTCOLLECTION.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>       </OLDAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>       </ACCOUNTAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<AUDITENTRIES.LIST>       </AUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<INPUTCRALLOCS.LIST>       </INPUTCRALLOCS.LIST>";
                xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>       </DUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEDUTYHEADDETAILS.LIST>       </EXCISEDUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>       </RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<SUMMARYALLOCS.LIST>       </SUMMARYALLOCS.LIST>";
                xmlstc = xmlstc + "<STPYMTDETAILS.LIST>       </STPYMTDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEPAYMENTALLOCATIONS.LIST>       </EXCISEPAYMENTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXBILLALLOCATIONS.LIST>       </TAXBILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXOBJECTALLOCATIONS.LIST>       </TAXOBJECTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TDSEXPENSEALLOCATIONS.LIST>       </TDSEXPENSEALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<VATSTATUTORYDETAILS.LIST>       </VATSTATUTORYDETAILS.LIST>";
                xmlstc = xmlstc + "<COSTTRACKALLOCATIONS.LIST>       </COSTTRACKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<REFVOUCHERDETAILS.LIST>       </REFVOUCHERDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEWISEDETAILS.LIST>       </INVOICEWISEDETAILS.LIST>";
                xmlstc = xmlstc + "<VATITCDETAILS.LIST>       </VATITCDETAILS.LIST>";
                xmlstc = xmlstc + "<ADVANCETAXDETAILS.LIST>       </ADVANCETAXDETAILS.LIST>";
                xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>";

                //if (creditNote.getInvoiceNo().Substring(0, 2) != "OS")
                //{
                if (creditNote.getInvoiceNo().Substring(0, 2) != "OS")
                {
                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>";
                    xmlstc = xmlstc + "<OLDAUDITENTRYIDS.LIST TYPE=\"Number\">";
                    xmlstc = xmlstc + "<OLDAUDITENTRYIDS>-1</OLDAUDITENTRYIDS>";
                    xmlstc = xmlstc + "</OLDAUDITENTRYIDS.LIST>";
                    xmlstc = xmlstc + "<LEDGERNAME>" + creditNote.getDebit_2_LEDGERNAME() + "</LEDGERNAME>";
                    xmlstc = xmlstc + "<GSTCLASS/>";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>";
                    xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>Yes</ISLASTDEEMEDPOSITIVE>";
                    xmlstc = xmlstc + "<AMOUNT>-" + creditNote.getDebit_2_Amount() + "</AMOUNT>";
                    xmlstc = xmlstc + "<VATEXPAMOUNT>-" + creditNote.getDebit_2_Amount() + "</VATEXPAMOUNT>";
                    xmlstc = xmlstc + "<SERVICETAXDETAILS.LIST>       </SERVICETAXDETAILS.LIST>";
                    xmlstc = xmlstc + "<BANKALLOCATIONS.LIST>       </BANKALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>       </BILLALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>       </INTERESTCOLLECTION.LIST>";
                    xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>       </OLDAUDITENTRIES.LIST>";
                    xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>       </ACCOUNTAUDITENTRIES.LIST>";
                    xmlstc = xmlstc + "<AUDITENTRIES.LIST>       </AUDITENTRIES.LIST>";
                    xmlstc = xmlstc + "<INPUTCRALLOCS.LIST>       </INPUTCRALLOCS.LIST>";
                    xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>       </DUTYHEADDETAILS.LIST>";
                    xmlstc = xmlstc + "<EXCISEDUTYHEADDETAILS.LIST>       </EXCISEDUTYHEADDETAILS.LIST>";
                    xmlstc = xmlstc + "<RATEDETAILS.LIST>       </RATEDETAILS.LIST>";
                    xmlstc = xmlstc + "<SUMMARYALLOCS.LIST>       </SUMMARYALLOCS.LIST>";
                    xmlstc = xmlstc + "<STPYMTDETAILS.LIST>       </STPYMTDETAILS.LIST>";
                    xmlstc = xmlstc + "<EXCISEPAYMENTALLOCATIONS.LIST>       </EXCISEPAYMENTALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<TAXBILLALLOCATIONS.LIST>       </TAXBILLALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<TAXOBJECTALLOCATIONS.LIST>       </TAXOBJECTALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<TDSEXPENSEALLOCATIONS.LIST>       </TDSEXPENSEALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<VATSTATUTORYDETAILS.LIST>       </VATSTATUTORYDETAILS.LIST>";
                    xmlstc = xmlstc + "<COSTTRACKALLOCATIONS.LIST>       </COSTTRACKALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<REFVOUCHERDETAILS.LIST>       </REFVOUCHERDETAILS.LIST>";
                    xmlstc = xmlstc + "<INVOICEWISEDETAILS.LIST>       </INVOICEWISEDETAILS.LIST>";
                    xmlstc = xmlstc + "<VATITCDETAILS.LIST>       </VATITCDETAILS.LIST>";
                    xmlstc = xmlstc + "<ADVANCETAXDETAILS.LIST>       </ADVANCETAXDETAILS.LIST>";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>";
                }



                xmlstc = xmlstc + "<PAYROLLMODEOFPAYMENT.LIST>      </PAYROLLMODEOFPAYMENT.LIST>";
                xmlstc = xmlstc + "<ATTDRECORDS.LIST>      </ATTDRECORDS.LIST>";
                xmlstc = xmlstc + "<TEMPGSTRATEDETAILS.LIST>      </TEMPGSTRATEDETAILS.LIST>";
                xmlstc = xmlstc + "</VOUCHER>";
                xmlstc = xmlstc + "</TALLYMESSAGE>";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=\"TallyUDF\">";
                xmlstc = xmlstc + "<COMPANY>";
                xmlstc = xmlstc + "<REMOTECMPINFO.LIST MERGE=\"Yes\">";
                xmlstc = xmlstc + "<REMOTECMPNAME>" + companyName + "</REMOTECMPNAME>";
                xmlstc = xmlstc + "</REMOTECMPINFO.LIST>";
                xmlstc = xmlstc + "</COMPANY>";
                xmlstc = xmlstc + "</TALLYMESSAGE>";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=\"TallyUDF\">";
                xmlstc = xmlstc + "<COMPANY>";
                xmlstc = xmlstc + "<REMOTECMPINFO.LIST MERGE=\"Yes\">";
                xmlstc = xmlstc + "<REMOTECMPNAME>" + companyName + "</REMOTECMPNAME>";
                xmlstc = xmlstc + "</REMOTECMPINFO.LIST>";
                xmlstc = xmlstc + "</COMPANY>";
                xmlstc = xmlstc + "</TALLYMESSAGE>";
                xmlstc = xmlstc + "</REQUESTDATA>";
                xmlstc = xmlstc + "</IMPORTDATA>";
                xmlstc = xmlstc + "</BODY>";
                xmlstc = xmlstc + "</ENVELOPE>";
                String xml = xmlstc;

                CreditNoteResponse = SendReqst(xml);
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            //MessageBox.Show(lLedgerResponse);
            return CreditNoteResponse;
        }

        public String SalesInvoiceCreateXml(SalesInvoice invoice) // request xml and response for ledger creation
        {
            String lLedgerResponse = "";

            try
            {
                String xmlstc = "";
                xmlstc = xmlstc + "<ENVELOPE>";
                xmlstc = xmlstc + "<HEADER>";
                xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>";
                xmlstc = xmlstc + "</HEADER>";
                xmlstc = xmlstc + "<BODY>";
                xmlstc = xmlstc + "<IMPORTDATA>";
                xmlstc = xmlstc + "<REQUESTDESC>";
                xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>";
                xmlstc = xmlstc + "<STATICVARIABLES>";
                xmlstc = xmlstc + "<SVCURRENTCOMPANY>" + companyName + "</SVCURRENTCOMPANY>";
                xmlstc = xmlstc + "</STATICVARIABLES>";
                xmlstc = xmlstc + "</REQUESTDESC>";
                xmlstc = xmlstc + "<REQUESTDATA>";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=\"TallyUDF\">";
                xmlstc = xmlstc + "<VOUCHER REMOTEID=\"" + invoice.getVoucherNumber() + invoice.getDate() + "\"  VCHTYPE=\"Sales\" ACTION=\"Create\" OBJVIEW=\"Invoice Voucher View\">";
                xmlstc = xmlstc + "<DATE>" + invoice.getDate() + "</DATE>";
                xmlstc = xmlstc + "<NARRATION>Invoice No - " + invoice.getVoucherNumber() + " Inv Qty - " + invoice.getNARRATION() + "</NARRATION>";
                xmlstc = xmlstc + "<GUID>" + invoice.getVoucherNumber() + invoice.getDate() + "</GUID>";
                xmlstc = xmlstc + "<STATENAME/>";
                xmlstc = xmlstc + "<GSTREGISTRATIONTYPE/>";
                xmlstc = xmlstc + "<VATDEALERTYPE>Regular</VATDEALERTYPE>";
                xmlstc = xmlstc + "<VOUCHERTYPENAME>Sales</VOUCHERTYPENAME>";
                xmlstc = xmlstc + "<VOUCHERNUMBER>" + invoice.getVoucherNumber() + "</VOUCHERNUMBER>";
                xmlstc = xmlstc + "<PARTYLEDGERNAME>" + invoice.getParty_Code() + "</PARTYLEDGERNAME>";
                xmlstc = xmlstc + "<BASICBASEPARTYNAME>" + invoice.getParty_Code() + "</BASICBASEPARTYNAME>";
                xmlstc = xmlstc + "<CSTFORMISSUETYPE/>";
                xmlstc = xmlstc + "<CSTFORMRECVTYPE/>";
                xmlstc = xmlstc + "<PERSISTEDVIEW>Invoice Voucher View</PERSISTEDVIEW>";
                xmlstc = xmlstc + "<BASICBUYERNAME>" + invoice.getParty_Code() + "</BASICBUYERNAME>";
                xmlstc = xmlstc + "<BASICDATETIMEOFINVOICE>" + invoice.getDate() + "</BASICDATETIMEOFINVOICE>";
                xmlstc = xmlstc + "<BASICDATETIMEOFREMOVAL>" + invoice.getDate() + "</BASICDATETIMEOFREMOVAL>";
                xmlstc = xmlstc + "<BUYERADDRESSTYPE/>";
                xmlstc = xmlstc + "<VCHGSTCLASS/>";
                xmlstc = xmlstc + "<DIFFACTUALQTY>No</DIFFACTUALQTY>";
                xmlstc = xmlstc + "<ASORIGINAL>No</ASORIGINAL>";
                xmlstc = xmlstc + "<FORJOBCOSTING>No</FORJOBCOSTING>";
                xmlstc = xmlstc + "<ISOPTIONAL>No</ISOPTIONAL>";
                xmlstc = xmlstc + "<EFFECTIVEDATE>" + invoice.getDate() + "</EFFECTIVEDATE>";
                xmlstc = xmlstc + "<USEFOREXCISE>No</USEFOREXCISE>";
                xmlstc = xmlstc + "<USEFORINTEREST>No</USEFORINTEREST>";
                xmlstc = xmlstc + "<USEFORGAINLOSS>No</USEFORGAINLOSS>";
                xmlstc = xmlstc + "<USEFORGODOWNTRANSFER>No</USEFORGODOWNTRANSFER>";
                xmlstc = xmlstc + "<USEFORCOMPOUND>No</USEFORCOMPOUND>";
                xmlstc = xmlstc + "<USEFORSERVICETAX>No</USEFORSERVICETAX>";
                xmlstc = xmlstc + "<EXCISETAXOVERRIDE>No</EXCISETAXOVERRIDE>";
                xmlstc = xmlstc + "<ISTDSOVERRIDDEN>No</ISTDSOVERRIDDEN>";
                xmlstc = xmlstc + "<ISTCSOVERRIDDEN>No</ISTCSOVERRIDDEN>";
                xmlstc = xmlstc + "<ISVATOVERRIDDEN>No</ISVATOVERRIDDEN>";
                xmlstc = xmlstc + "<ISSERVICETAXOVERRIDDEN>No</ISSERVICETAXOVERRIDDEN>";
                xmlstc = xmlstc + "<ISEXCISEOVERRIDDEN>No</ISEXCISEOVERRIDDEN>";
                xmlstc = xmlstc + "<ISGSTOVERRIDDEN>No</ISGSTOVERRIDDEN>";
                xmlstc = xmlstc + "<ISCANCELLED>No</ISCANCELLED>";
                xmlstc = xmlstc + "<HASCASHFLOW>No</HASCASHFLOW>";
                xmlstc = xmlstc + "<ISPOSTDATED>No</ISPOSTDATED>";
                xmlstc = xmlstc + "<USETRACKINGNUMBER>No</USETRACKINGNUMBER>";
                xmlstc = xmlstc + "<ISINVOICE>Yes</ISINVOICE>";
                xmlstc = xmlstc + "<MFGJOURNAL>No</MFGJOURNAL>";
                xmlstc = xmlstc + "<HASDISCOUNTS>No</HASDISCOUNTS>";
                xmlstc = xmlstc + "<ASPAYSLIP>No</ASPAYSLIP>";
                xmlstc = xmlstc + "<ISCOSTCENTRE>No</ISCOSTCENTRE>";
                xmlstc = xmlstc + "<ISSTXNONREALIZEDVCH>No</ISSTXNONREALIZEDVCH>";
                xmlstc = xmlstc + "<ISBLANKCHEQUE>No</ISBLANKCHEQUE>";
                xmlstc = xmlstc + "<ISVOID>No</ISVOID>";
                xmlstc = xmlstc + "<ISONHOLD>No</ISONHOLD>";
                xmlstc = xmlstc + "<ORDERLINESTATUS>No</ORDERLINESTATUS>";
                xmlstc = xmlstc + "<ISVATDUTYPAID>Yes</ISVATDUTYPAID>";
                xmlstc = xmlstc + "<ISDELETED>No</ISDELETED>";
                xmlstc = xmlstc + "<CURRPARTYLEDGERNAME>" + invoice.getParty_Code() + "</CURRPARTYLEDGERNAME>";
                xmlstc = xmlstc + "<CURRBASICBUYERNAME>" + invoice.getParty_Code() + "</CURRBASICBUYERNAME>";
                xmlstc = xmlstc + "<CURRPARTYNAME/>";
                xmlstc = xmlstc + " <CURRBUYERADDRESSTYPE/>";
                xmlstc = xmlstc + "<CURRBASICPURCHASEORDERNO/>";
                xmlstc = xmlstc + "<CURRBASICSHIPDELIVERYNOTE/>";
                xmlstc = xmlstc + "<EXCLUDEDTAXATIONS.LIST>      </EXCLUDEDTAXATIONS.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>      </OLDAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>      </ACCOUNTAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<AUDITENTRIES.LIST>      </AUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>      </DUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<SUPPLEMENTARYDUTYHEADDETAILS.LIST>      </SUPPLEMENTARYDUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEDELNOTES.LIST>      </INVOICEDELNOTES.LIST>";
                xmlstc = xmlstc + "<INVOICEORDERLIST.LIST>      </INVOICEORDERLIST.LIST>";
                xmlstc = xmlstc + "<INVOICEINDENTLIST.LIST>      </INVOICEINDENTLIST.LIST>";
                xmlstc = xmlstc + "<ATTENDANCEENTRIES.LIST>      </ATTENDANCEENTRIES.LIST>";
                xmlstc = xmlstc + "<ORIGINVOICEDETAILS.LIST>      </ORIGINVOICEDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEEXPORTLIST.LIST>      </INVOICEEXPORTLIST.LIST>";
                xmlstc = xmlstc + "<LEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<LEDGERNAME>" + invoice.getDebit_LEDGERNAME() + "</LEDGERNAME>";
                xmlstc = xmlstc + "<VOUCHERFBTCATEGORY/>";
                xmlstc = xmlstc + "<GSTCLASS/>";
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>";
                xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>";
                xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>Yes</ISLASTDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<AMOUNT>-" + invoice.getDebit_Amount() + "</AMOUNT>";
                xmlstc = xmlstc + "<VATEXPAMOUNT>-" + invoice.getDebit_Amount() + "</VATEXPAMOUNT>";
                xmlstc = xmlstc + "<SERVICETAXDETAILS.LIST>       </SERVICETAXDETAILS.LIST>";
                xmlstc = xmlstc + "<BANKALLOCATIONS.LIST>       </BANKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<NAME>" + invoice.getVoucherNumber() + "</NAME>";
                xmlstc = xmlstc + "<BILLTYPE>New Ref</BILLTYPE>";
                xmlstc = xmlstc + "<TDSDEDUCTEEISSPECIALRATE>No</TDSDEDUCTEEISSPECIALRATE>";
                xmlstc = xmlstc + "<AMOUNT>-" + invoice.getDebit_Amount() + "</AMOUNT>";
                xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>        </INTERESTCOLLECTION.LIST>";
                xmlstc = xmlstc + "<STBILLCATEGORIES.LIST>        </STBILLCATEGORIES.LIST>";
                xmlstc = xmlstc + "</BILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>       </INTERESTCOLLECTION.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>       </OLDAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>       </ACCOUNTAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<AUDITENTRIES.LIST>       </AUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<INPUTCRALLOCS.LIST>       </INPUTCRALLOCS.LIST>";
                xmlstc = xmlstc + "<INVENTORYALLOCATIONS.LIST>       </INVENTORYALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>       </DUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEDUTYHEADDETAILS.LIST>       </EXCISEDUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>       </RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<SUMMARYALLOCS.LIST>       </SUMMARYALLOCS.LIST>";
                xmlstc = xmlstc + "<STPYMTDETAILS.LIST>       </STPYMTDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEPAYMENTALLOCATIONS.LIST>       </EXCISEPAYMENTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXBILLALLOCATIONS.LIST>       </TAXBILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXOBJECTALLOCATIONS.LIST>       </TAXOBJECTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TDSEXPENSEALLOCATIONS.LIST>       </TDSEXPENSEALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<VATSTATUTORYDETAILS.LIST>       </VATSTATUTORYDETAILS.LIST>";
                xmlstc = xmlstc + "<REFVOUCHERDETAILS.LIST>       </REFVOUCHERDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEWISEDETAILS.LIST>       </INVOICEWISEDETAILS.LIST>";
                xmlstc = xmlstc + "<VATITCDETAILS.LIST>       </VATITCDETAILS.LIST>";
                xmlstc = xmlstc + "<ADVANCETAXDETAILS.LIST>       </ADVANCETAXDETAILS.LIST>";
                xmlstc = xmlstc + "</LEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<LEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<TAXCLASSIFICATIONNAME/>";
                xmlstc = xmlstc + "<LEDGERNAME>" + invoice.getCredit_0_LEDGERNAME() + "</LEDGERNAME>";
                xmlstc = xmlstc + "<VOUCHERFBTCATEGORY/>";
                xmlstc = xmlstc + "<GSTCLASS/>";
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>";
                xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>";
                xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>No</ISLASTDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<AMOUNT>" + invoice.getCredit_0_Amount() + "</AMOUNT>";
                xmlstc = xmlstc + "<VATEXPAMOUNT>" + invoice.getCredit_0_Amount() + "</VATEXPAMOUNT>";
                xmlstc = xmlstc + "<SERVICETAXDETAILS.LIST>       </SERVICETAXDETAILS.LIST>";
                xmlstc = xmlstc + "<BANKALLOCATIONS.LIST>       </BANKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>       </BILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>       </INTERESTCOLLECTION.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>       </OLDAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>       </ACCOUNTAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<AUDITENTRIES.LIST>       </AUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<INPUTCRALLOCS.LIST>       </INPUTCRALLOCS.LIST>";
                xmlstc = xmlstc + "<INVENTORYALLOCATIONS.LIST>       </INVENTORYALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>       </DUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEDUTYHEADDETAILS.LIST>       </EXCISEDUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>       </RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<SUMMARYALLOCS.LIST>       </SUMMARYALLOCS.LIST>";
                xmlstc = xmlstc + "<STPYMTDETAILS.LIST>       </STPYMTDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEPAYMENTALLOCATIONS.LIST>       </EXCISEPAYMENTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXBILLALLOCATIONS.LIST>       </TAXBILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXOBJECTALLOCATIONS.LIST>       </TAXOBJECTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TDSEXPENSEALLOCATIONS.LIST>       </TDSEXPENSEALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<VATSTATUTORYDETAILS.LIST>       </VATSTATUTORYDETAILS.LIST>";
                xmlstc = xmlstc + "<REFVOUCHERDETAILS.LIST>       </REFVOUCHERDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEWISEDETAILS.LIST>       </INVOICEWISEDETAILS.LIST>";
                xmlstc = xmlstc + "<VATITCDETAILS.LIST>       </VATITCDETAILS.LIST>";
                xmlstc = xmlstc + "<ADVANCETAXDETAILS.LIST>       </ADVANCETAXDETAILS.LIST>";
                xmlstc = xmlstc + "</LEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<LEDGERENTRIES.LIST>";
                xmlstc = xmlstc + "<BASICRATEOFINVOICETAX.LIST TYPE=\"Number\">";
                //xmlstc = xmlstc + "<BASICRATEOFINVOICETAX> 6</BASICRATEOFINVOICETAX>";
                xmlstc = xmlstc + "</BASICRATEOFINVOICETAX.LIST>";
                xmlstc = xmlstc + "<ROUNDTYPE/>";
                xmlstc = xmlstc + "<LEDGERNAME>" + invoice.getCredit_1_LEDGERNAME() + "</LEDGERNAME>";
                xmlstc = xmlstc + "<VOUCHERFBTCATEGORY/>";
                xmlstc = xmlstc + "<GSTCLASS/>";
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>";
                xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>";
                xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>No</ISLASTDEEMEDPOSITIVE>";
                xmlstc = xmlstc + "<AMOUNT>" + invoice.getCredit_1_Amount() + "</AMOUNT>";
                xmlstc = xmlstc + "<VATEXPAMOUNT>" + invoice.getCredit_1_Amount() + "</VATEXPAMOUNT>";
                xmlstc = xmlstc + "<SERVICETAXDETAILS.LIST>       </SERVICETAXDETAILS.LIST>";
                xmlstc = xmlstc + "<BANKALLOCATIONS.LIST>       </BANKALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>       </BILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>       </INTERESTCOLLECTION.LIST>";
                xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>       </OLDAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>       </ACCOUNTAUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<AUDITENTRIES.LIST>       </AUDITENTRIES.LIST>";
                xmlstc = xmlstc + "<INPUTCRALLOCS.LIST>       </INPUTCRALLOCS.LIST>";
                xmlstc = xmlstc + "<INVENTORYALLOCATIONS.LIST>       </INVENTORYALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>       </DUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEDUTYHEADDETAILS.LIST>       </EXCISEDUTYHEADDETAILS.LIST>";
                xmlstc = xmlstc + "<RATEDETAILS.LIST>       </RATEDETAILS.LIST>";
                xmlstc = xmlstc + "<SUMMARYALLOCS.LIST>       </SUMMARYALLOCS.LIST>";
                xmlstc = xmlstc + "<STPYMTDETAILS.LIST>       </STPYMTDETAILS.LIST>";
                xmlstc = xmlstc + "<EXCISEPAYMENTALLOCATIONS.LIST>       </EXCISEPAYMENTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXBILLALLOCATIONS.LIST>       </TAXBILLALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TAXOBJECTALLOCATIONS.LIST>       </TAXOBJECTALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<TDSEXPENSEALLOCATIONS.LIST>       </TDSEXPENSEALLOCATIONS.LIST>";
                xmlstc = xmlstc + "<VATSTATUTORYDETAILS.LIST>       </VATSTATUTORYDETAILS.LIST>";
                xmlstc = xmlstc + "<REFVOUCHERDETAILS.LIST>       </REFVOUCHERDETAILS.LIST>";
                xmlstc = xmlstc + "<INVOICEWISEDETAILS.LIST>       </INVOICEWISEDETAILS.LIST>";
                xmlstc = xmlstc + "<VATITCDETAILS.LIST>       </VATITCDETAILS.LIST>";
                xmlstc = xmlstc + "<ADVANCETAXDETAILS.LIST>       </ADVANCETAXDETAILS.LIST>";
                xmlstc = xmlstc + "</LEDGERENTRIES.LIST>";


                //if (invoice.getVoucherNumber().Substring(0, 2) == localinvoice)
                //{
                if (invoice.getCredit_2_Amount() != "0")
                {
                    xmlstc = xmlstc + "<LEDGERENTRIES.LIST>";
                    xmlstc = xmlstc + "<BASICRATEOFINVOICETAX.LIST TYPE=\"Number\">";
                    //xmlstc = xmlstc + "<BASICRATEOFINVOICETAX> 6</BASICRATEOFINVOICETAX>";
                    xmlstc = xmlstc + "</BASICRATEOFINVOICETAX.LIST>";
                    xmlstc = xmlstc + "<ROUNDTYPE/>";
                    xmlstc = xmlstc + "<LEDGERNAME>" + invoice.getCredit_2_LEDGERNAME() + "</LEDGERNAME>";
                    xmlstc = xmlstc + "<VOUCHERFBTCATEGORY/>";
                    xmlstc = xmlstc + "<GSTCLASS/>";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>";
                    xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>No</ISLASTDEEMEDPOSITIVE>";
                    xmlstc = xmlstc + "<AMOUNT>" + invoice.getCredit_2_Amount() + "</AMOUNT>";
                    xmlstc = xmlstc + "<VATEXPAMOUNT>" + invoice.getCredit_2_Amount() + "</VATEXPAMOUNT>";
                    xmlstc = xmlstc + "<SERVICETAXDETAILS.LIST>       </SERVICETAXDETAILS.LIST>";
                    xmlstc = xmlstc + "<BANKALLOCATIONS.LIST>       </BANKALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>       </BILLALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<INTERESTCOLLECTION.LIST>       </INTERESTCOLLECTION.LIST>";
                    xmlstc = xmlstc + "<OLDAUDITENTRIES.LIST>       </OLDAUDITENTRIES.LIST>";
                    xmlstc = xmlstc + "<ACCOUNTAUDITENTRIES.LIST>       </ACCOUNTAUDITENTRIES.LIST>";
                    xmlstc = xmlstc + "<AUDITENTRIES.LIST>       </AUDITENTRIES.LIST>";
                    xmlstc = xmlstc + "<INPUTCRALLOCS.LIST>       </INPUTCRALLOCS.LIST>";
                    xmlstc = xmlstc + "<INVENTORYALLOCATIONS.LIST>       </INVENTORYALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<DUTYHEADDETAILS.LIST>       </DUTYHEADDETAILS.LIST>";
                    xmlstc = xmlstc + "<EXCISEDUTYHEADDETAILS.LIST>       </EXCISEDUTYHEADDETAILS.LIST>";
                    xmlstc = xmlstc + "<RATEDETAILS.LIST>       </RATEDETAILS.LIST>";
                    xmlstc = xmlstc + "<SUMMARYALLOCS.LIST>       </SUMMARYALLOCS.LIST>";
                    xmlstc = xmlstc + "<STPYMTDETAILS.LIST>       </STPYMTDETAILS.LIST>";
                    xmlstc = xmlstc + "<EXCISEPAYMENTALLOCATIONS.LIST>       </EXCISEPAYMENTALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<TAXBILLALLOCATIONS.LIST>       </TAXBILLALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<TAXOBJECTALLOCATIONS.LIST>       </TAXOBJECTALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<TDSEXPENSEALLOCATIONS.LIST>       </TDSEXPENSEALLOCATIONS.LIST>";
                    xmlstc = xmlstc + "<VATSTATUTORYDETAILS.LIST>       </VATSTATUTORYDETAILS.LIST>";
                    xmlstc = xmlstc + "<REFVOUCHERDETAILS.LIST>       </REFVOUCHERDETAILS.LIST>";
                    xmlstc = xmlstc + "<INVOICEWISEDETAILS.LIST>       </INVOICEWISEDETAILS.LIST>";
                    xmlstc = xmlstc + "<VATITCDETAILS.LIST>       </VATITCDETAILS.LIST>";
                    xmlstc = xmlstc + "<ADVANCETAXDETAILS.LIST>       </ADVANCETAXDETAILS.LIST>";
                    xmlstc = xmlstc + "</LEDGERENTRIES.LIST>";
                }






                xmlstc = xmlstc + "<ALLINVENTORYENTRIES.LIST>      </ALLINVENTORYENTRIES.LIST>";
                xmlstc = xmlstc + "<VCHLEDTOTALTREE.LIST>      </VCHLEDTOTALTREE.LIST>";
                xmlstc = xmlstc + "<PAYROLLMODEOFPAYMENT.LIST>      </PAYROLLMODEOFPAYMENT.LIST>";
                xmlstc = xmlstc + "<ATTDRECORDS.LIST>      </ATTDRECORDS.LIST>";
                xmlstc = xmlstc + "<TEMPGSTRATEDETAILS.LIST>      </TEMPGSTRATEDETAILS.LIST>";
                xmlstc = xmlstc + "</VOUCHER>";
                xmlstc = xmlstc + "</TALLYMESSAGE>";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=\"TallyUDF\">";
                xmlstc = xmlstc + "<COMPANY>";
                xmlstc = xmlstc + "<REMOTECMPINFO.LIST MERGE=\"Yes\">";
                xmlstc = xmlstc + "<REMOTECMPNAME>" + companyName + "</REMOTECMPNAME>";
                xmlstc = xmlstc + "</REMOTECMPINFO.LIST>";
                xmlstc = xmlstc + "</COMPANY>";
                xmlstc = xmlstc + "</TALLYMESSAGE>";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=\"TallyUDF\">";
                xmlstc = xmlstc + "<COMPANY>";
                xmlstc = xmlstc + "<REMOTECMPINFO.LIST MERGE=\"Yes\">";
                xmlstc = xmlstc + "<REMOTECMPNAME>" + companyName + "</REMOTECMPNAME>";
                xmlstc = xmlstc + "</REMOTECMPINFO.LIST>";
                xmlstc = xmlstc + "</COMPANY>";
                xmlstc = xmlstc + "</TALLYMESSAGE>";
                xmlstc = xmlstc + "</REQUESTDATA>";
                xmlstc = xmlstc + "</IMPORTDATA>";
                xmlstc = xmlstc + "</BODY>";
                xmlstc = xmlstc + "</ENVELOPE>";
                String xml = xmlstc;

                lLedgerResponse = SendReqst(xml);
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return lLedgerResponse;
        }

        public string SendReqst(string pWebRequstStr)
        {
            String lResponseStr = "";
            String lResult = "";

            try
            {
                String lTallyLocalHost = "http://" + serviceaddress;
                //String lTallyLocalHost = "http://127.0.0.1:18000";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(lTallyLocalHost);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = (long)pWebRequstStr.Length;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                StreamWriter lStrmWritr = new StreamWriter(httpWebRequest.GetRequestStream());
                lStrmWritr.Write(pWebRequstStr);
                lStrmWritr.Close();
                HttpWebResponse lhttpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream lreceiveStream = lhttpResponse.GetResponseStream();
                StreamReader lStreamReader = new StreamReader(lreceiveStream, Encoding.UTF8);
                lResponseStr = lStreamReader.ReadToEnd();
                lhttpResponse.Close();
                lStreamReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            lResult = lResponseStr;
            return lResult;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            String line;
            using (StreamReader reader = new StreamReader("ServerConfig.txt"))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("connectionString="))
                    {
                        connectionString = line.Substring(line.IndexOf("=") + 1);
                    }
                    else if (line.Contains("serviceaddress="))
                    {
                        serviceaddress = line.Substring(line.IndexOf("=") + 1);
                    }
                    else if (line.Contains("CompanyName="))
                    {
                        companyName = line.Substring(line.IndexOf("=") + 1);
                    }
                    else if (line.Contains("city_id="))
                    {
                       city_id=line.Substring(line.IndexOf("=") + 1);
                    }
                    //else if (line.Contains("intersateinvoice="))
                    //{
                    //    intersateinvoice = line.Substring(line.IndexOf("=") + 1);
                    //}
                }

                reader.Close();
                CRPrefixCode = getCRPrefix(city_id);

            }


            Source = new SqlConnection(connectionString);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread myNewThread = new Thread(() => Retrive_Bind_Data());
            myNewThread.Start();
        }


        private void Retrive_Bind_Data()
        {
            try
            {
                label3.Invoke(new Action(() => { label3.Visible = true; }));
                //label3.Visible = true;
                Source.Close();
                Source.Open();
                AltCount = 0;
                CreatedCount = 0;
                ErrorCount = 0;
                string FromDate = dateTimePicker1.Value.ToString("yyyyMMdd");
                string ToDate = dateTimePicker2.Value.ToString("yyyyMMdd");



                Retrivecmd = new SqlCommand("select  I.invoiceno VoucherNo,I.InvoiceDate Date,P.party_Code,p.party_name,I.AmountAfterDiscount as Amt,ceiling(i.S_UTGST_Amt) As SGST,Ceiling(i.CGST_Amt) As CGST,ceiling(i.IGST_Amt) As IGST,(I.AmountAfterDiscount+ceiling(i.S_UTGST_Amt)+Ceiling(i.CGST_Amt)+ceiling(i.IGST_Amt)) As TotalInvoiceAmt,sum(cast(isnull(o.Rqty,0) as int) + cast(isnull(o.Lqty,0) as int)) AS totQty  from invoicemaster I"
                                                + " inner join  partydetails P on I.party_code = P.Party_code"
                                                + " inner join  ordermaster O on i.invoiceno =O.invoiceno where P.party_Code<>'' and (i.invoicedate between '" + FromDate + "' and '" + ToDate + "') and i.invoiceno='MH2018-19/019130'"
                                                + " group by P.party_Code,P.party_name,I.invoiceno,I.InvoiceDate,I.AmountAfterDiscount,I.InvoiceAmount,i.SalesTax,i.S_UTGST_Amt,i.CGST_Amt,i.IGST_Amt,i.EduCessAmt,i.HigherEduCessAmt order by I.invoiceno asc", Source);
                SalesInvoices = new DataTable();
                SalesInvoices.Load(Retrivecmd.ExecuteReader());
                Source.Close();

                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = SalesInvoices; }));
                dataGridView1.Invoke(new Action(() => { label4.Text = (dataGridView1.RowCount - 1).ToString(); }));



                label13.Invoke(new Action(() => { label13.Text = SalesInvoices.Compute("sum(TotalInvoiceAmt)", "").ToString(); }));
                label15.Invoke(new Action(() => { label15.Text = SalesInvoices.Compute("sum(Amt)", "").ToString(); }));
                label17.Invoke(new Action(() => { label17.Text = SalesInvoices.Compute("sum(SGST)", "").ToString(); }));
                label19.Invoke(new Action(() => { label19.Text = SalesInvoices.Compute("sum(CGST)", "").ToString(); }));
                label21.Invoke(new Action(() => { label21.Text = SalesInvoices.Compute("sum(IGST)", "").ToString(); }));
                //dataGridView1.DataSource = SalesInvoices;

                //label3.Text = "Done";

                label3.Invoke(new Action(() => { label3.Text = "Done"; }));
            }
            catch (Exception aer)
            {

            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            Thread myNewThread = new Thread(() => calltoRequestingthread_button3());
            myNewThread.Start();
        }


        private void calltoRequestingthread_button3()
        {
            ErrorCount = 0;
            CreatedCount = 0;
            AltCount = 0;

            //reset indication
            label6.Invoke(new Action(() => { label6.Text = AltCount.ToString(); }));
            label5.Invoke(new Action(() => { label5.Text = CreatedCount.ToString(); }));
            label7.Invoke(new Action(() => { label7.Text = ErrorCount.ToString(); }));


            label3.Invoke(new Action(() => { label3.Text = "Working On It Please Wait..."; }));
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                Requesting(i);

            }
            label3.Invoke(new Action(() => { label3.Text = "completed invoice generation"; }));


        }


        private void Requesting(int i)
        {
            String voucherNo, date, party_Code, party_Name, amt, CGST, SGST, IGST, totalamount, Qty;
            String ResponseTally;

            voucherNo = dataGridView1.Rows[i].Cells[0].Value.ToString();
            date = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value.ToString()).ToString("yyyyMMdd");
            party_Code = dataGridView1.Rows[i].Cells[2].Value.ToString();
            party_Name = dataGridView1.Rows[i].Cells[3].Value.ToString();
            amt = dataGridView1.Rows[i].Cells[4].Value.ToString();


            CGST = dataGridView1.Rows[i].Cells[5].Value.ToString();
            if (CGST != null || CGST != "")
            {
                CGST = dataGridView1.Rows[i].Cells[5].Value.ToString();
            }
            else
            {
                CGST = "0";
            }
            SGST = dataGridView1.Rows[i].Cells[6].Value.ToString();

            if (SGST != null || SGST != "")
            {
                SGST = dataGridView1.Rows[i].Cells[6].Value.ToString();
            }
            else
            {
                SGST = "0";
            }
            IGST = dataGridView1.Rows[i].Cells[7].Value.ToString();
            if (IGST != null || IGST != "")
            {
                IGST = dataGridView1.Rows[i].Cells[7].Value.ToString();
            }
            else
            {
                IGST = "0";
            }

            totalamount = dataGridView1.Rows[i].Cells[8].Value.ToString();



            Qty = dataGridView1.Rows[i].Cells[9].Value.ToString();


            if (IGST == "0" && CGST != "0" && SGST != "0")
            {
                //create object of the sales invoice and pass it to the sales invoice create method
                ResponseTally = SalesInvoiceCreateXml(new SalesInvoice(voucherNo, date, party_Code, party_Name, Qty, totalamount, "Local Sales", amt, "Output CGST 6%", CGST, "Output SGST 6%", SGST));
                if (ResponseTally.Contains("<ALTERED>1</ALTERED>") || ResponseTally.Contains("<ALTERED>3</ALTERED>"))
                {
                    AltCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    label6.Invoke(new Action(() => { label6.Text = AltCount.ToString(); }));
                }
                else if (ResponseTally.Contains("<CREATED>1</CREATED>"))
                {
                    CreatedCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    label5.Invoke(new Action(() => { label5.Text = CreatedCount.ToString(); }));

                }
                else
                {
                    ErrorCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    label7.Invoke(new Action(() => { label7.Text = ErrorCount.ToString(); }));
                }

            }
            else if (IGST != "0" && CGST == "0" && SGST == "0")
            {
                //create object of the sales invoice and pass it to the sales invoice create method
                ResponseTally = SalesInvoiceCreateXml(new SalesInvoice(voucherNo, date, party_Code, party_Name, Qty, totalamount, "Interstate Sale", amt, "Output IGST 12%", IGST, "Output SGST 6%", "0"));
                if (ResponseTally.Contains("<ALTERED>1</ALTERED>") || ResponseTally.Contains("<ALTERED>3</ALTERED>"))
                {
                    AltCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    label6.Invoke(new Action(() => { label6.Text = AltCount.ToString(); }));
                }
                else if (ResponseTally.Contains("<CREATED>1</CREATED>"))
                {
                    CreatedCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    label5.Invoke(new Action(() => { label5.Text = CreatedCount.ToString(); }));

                }
                else
                {
                    ErrorCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    label7.Invoke(new Action(() => { label7.Text = ErrorCount.ToString(); }));
                }

            } //for mumbai only
            else if (IGST == "0" && CGST == "0" && SGST == "0" && "EX" == voucherNo.Substring(0,2))
            {
                //create object of the sales invoice and pass it to the sales invoice create method
                ResponseTally = SalesInvoiceCreateXml(new SalesInvoice(voucherNo, date, party_Code, party_Name, Qty, totalamount, "Export Sales", amt, "Output IGST 12%", "0", "Output SGST 6%", "0"));
                if (ResponseTally.Contains("<ALTERED>1</ALTERED>") || ResponseTally.Contains("<ALTERED>3</ALTERED>"))
                {
                    AltCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    label6.Invoke(new Action(() => { label6.Text = AltCount.ToString(); }));
                }
                else if (ResponseTally.Contains("<CREATED>1</CREATED>"))
                {
                    CreatedCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    label5.Invoke(new Action(() => { label5.Text = CreatedCount.ToString(); }));

                }
                else
                {
                    ErrorCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    label7.Invoke(new Action(() => { label7.Text = ErrorCount.ToString(); }));
                }

            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Thread myNewThread = new Thread(() => Retrive_Bind_CreditNote_date());
            myNewThread.Start();
        }

        private void Retrive_Bind_CreditNote_date()
        {
            try
            {
                label3.Invoke(new Action(() => { label3.Visible = true; }));
                //label3.Visible = true;
                Source.Close();
                Source.Open();
                AltCount = 0;
                CreatedCount = 0;
                ErrorCount = 0;
                string FromDate = dateTimePicker1.Value.ToString("yyyyMMdd");
                string ToDate = dateTimePicker2.Value.ToString("yyyyMMdd");



                //create new view with command
                Retrivecmd = new SqlCommand("SELECT '"+CRPrefixCode+"'+cnm.CreditNoteNo,cnm.InvoiceNo AS InvoiceNo,cnm.PartyCode AS Party_Code,cnm.UpdateDate AS UpdateDate," +
                                            "ceiling(sum(cnt.orderAmount)) AS Amt,ceiling(sum(cnt.orderAmount)*p.S_UTGST/100) as SGST,ceiling(sum(cnt.orderAmount)*p.CGST/100) as CGST," +
                                            "ceiling(sum(cnt.orderAmount)*p.IGST/100) as IGST,ceiling(sum(cnt.orderAmount))+ceiling(sum(cnt.orderAmount)*p.S_UTGST/100)+ceiling(sum(cnt.orderAmount)*p.CGST/100)+ceiling(sum(cnt.orderAmount)*p.IGST/100) AS CreditNoteAmount FROM dbo.CreditNoteMaster cnm" +
                                            " inner join dbo.invoicemaster inm on inm.invoiceno=cnm.InvoiceNo" +
                                            " inner join CreditNotetran cnt on cnm.CreditNoteNo = cnt.CreditNoteNo" +
                                            " INNER JOIN dbo.partydetails p ON cnm.partycode = p.party_code" +
                                            " WHERE   (cnm.UpdateDate BETWEEN '" + FromDate + "' AND '" + ToDate + "') group by cnm.CreditNoteNo,cnm.InvoiceNo,cnm.PartyCode,cnm.UpdateDate,p.S_UTGST, p.CGST,p.IGST order by  cast(cnm.CreditNoteNo AS int)", Source);

                SalesInvoices = new DataTable();
                SalesInvoices.Load(Retrivecmd.ExecuteReader());
                Source.Close();

                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = SalesInvoices; }));
                dataGridView1.Invoke(new Action(() => { label4.Text = (dataGridView1.RowCount - 1).ToString(); }));


                label13.Invoke(new Action(() => { label13.Text = SalesInvoices.Compute("sum(CreditNoteAmount)", "").ToString(); }));
                label15.Invoke(new Action(() => { label15.Text = SalesInvoices.Compute("sum(Amt)", "").ToString(); }));
                label17.Invoke(new Action(() => { label17.Text = SalesInvoices.Compute("sum(SGST)", "").ToString(); }));
                label19.Invoke(new Action(() => { label19.Text = SalesInvoices.Compute("sum(CGST)", "").ToString(); }));
                label21.Invoke(new Action(() => { label21.Text = SalesInvoices.Compute("sum(IGST)", "").ToString(); }));

                //dataGridView1.DataSource = SalesInvoices;

                //label3.Text = "Done";

                label3.Invoke(new Action(() => { label3.Text = "Done"; }));
            }
            catch (Exception aer)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread myNewThread = new Thread(() => calltoCreditNoteRequestingthread_button4());
            myNewThread.Start();
        }


        private void calltoCreditNoteRequestingthread_button4()
        {
            label3.Invoke(new Action(() => { label3.Text = "Working On It Please Wait..."; }));
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                RequestingCreditNote(i);

            }
            label3.Invoke(new Action(() => { label3.Text = "completed Credit note generation"; }));


        }
        private void RequestingCreditNote(int i)
        {
            String creditnoteno, date, party_Code, invoiceno, amt, sgst, cgst, igst, creditnoteamt;
            String ResponseTally;

            creditnoteno = dataGridView1.Rows[i].Cells[0].Value.ToString();
            date = Convert.ToDateTime(dataGridView1.Rows[i].Cells[3].Value.ToString()).ToString("yyyyMMdd");
            party_Code = dataGridView1.Rows[i].Cells[2].Value.ToString();
            invoiceno = dataGridView1.Rows[i].Cells[1].Value.ToString();
            amt = dataGridView1.Rows[i].Cells[4].Value.ToString();
            sgst = dataGridView1.Rows[i].Cells[5].Value.ToString();
            cgst = dataGridView1.Rows[i].Cells[6].Value.ToString();
            igst = dataGridView1.Rows[i].Cells[7].Value.ToString();
            creditnoteamt = dataGridView1.Rows[i].Cells[8].Value.ToString();



            if (sgst != "0" && cgst != "0" && igst == "0")
            {
                //create object of the sales invoice and pass it to the sales invoice create method
                ResponseTally = CreditNoteCreateXml(new CreditNote(creditnoteno, date, party_Code, "Being credit note issued to the above party", invoiceno, creditnoteamt, "Local Sales", amt, "Output SGST 6%", sgst, "Output CGST 6%", cgst));
                if (ResponseTally.Contains("<ALTERED>1</ALTERED>") || ResponseTally.Contains("<ALTERED>3</ALTERED>"))
                {
                    AltCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    label6.Invoke(new Action(() => { label6.Text = AltCount.ToString(); }));
                }
                else if (ResponseTally.Contains("<CREATED>1</CREATED>"))
                {
                    CreatedCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    label5.Invoke(new Action(() => { label5.Text = CreatedCount.ToString(); }));

                }
                else
                {
                    ErrorCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    label7.Invoke(new Action(() => { label7.Text = ErrorCount.ToString(); }));
                }

            }
            else if (sgst == "0" && cgst == "0" && igst != "0")
            {
                //create object of the sales invoice and pass it to the sales invoice create method
                ResponseTally = CreditNoteCreateXml(new CreditNote(creditnoteno, date, party_Code, "Being credit note issued to the above party", invoiceno, creditnoteamt, "Interstate Sale", amt, "Output IGST 12%", igst, "Output CGST 6%", cgst));
                if (ResponseTally.Contains("<ALTERED>1</ALTERED>") || ResponseTally.Contains("<ALTERED>3</ALTERED>"))
                {
                    AltCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    label6.Invoke(new Action(() => { label6.Text = AltCount.ToString(); }));
                }
                else if (ResponseTally.Contains("<CREATED>1</CREATED>"))
                {
                    CreatedCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    label5.Invoke(new Action(() => { label5.Text = CreatedCount.ToString(); }));

                }
                else
                {
                    ErrorCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    label7.Invoke(new Action(() => { label7.Text = ErrorCount.ToString(); }));
                }
            }else if (sgst == "0" && cgst == "0" && igst == "0")
            {
                //create object of the sales invoice and pass it to the sales invoice create method
                ResponseTally = CreditNoteCreateXml(new CreditNote(creditnoteno, date, party_Code, "Being credit note issued to the above party", invoiceno, creditnoteamt, "Export Sales", amt, "Output IGST 12%", igst, "Output CGST 6%", cgst));
                if (ResponseTally.Contains("<ALTERED>1</ALTERED>") || ResponseTally.Contains("<ALTERED>3</ALTERED>"))
                {
                    AltCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    label6.Invoke(new Action(() => { label6.Text = AltCount.ToString(); }));
                }
                else if (ResponseTally.Contains("<CREATED>1</CREATED>"))
                {
                    CreatedCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    label5.Invoke(new Action(() => { label5.Text = CreatedCount.ToString(); }));

                }
                else
                {
                    ErrorCount++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    label7.Invoke(new Action(() => { label7.Text = ErrorCount.ToString(); }));
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public String getCRPrefix(String city_id)
        {
            String result="";
            switch (city_id)
            {
                case "2":
                    result = "DL";
                    break;
                case "3":
                    result = "MH";
                    break;
                case "5":
                   
                    break;
                case "7":
                    result = "CH";
                    break;
                case "9":
                     result = "AH";
                    break;
                case "11":
                    result = "GA";
                    break;
                case "12":
                    result = "HY";
                    break;
                case "13":
                    result = "MN";
                    break;
                case "14":
                    result = "CO";
                    break;
                case "17":
                    result = "BL";
                    break;
                case "19":
                    result = "SU";
                    break;
                case "20":
                    result = "KH";
                    break;
                case "21":
                    result = "HU";
                    break;
                case "22":
                    result = "MD";
                    break;
                case "23":
                    result = "AB";
                    break;
                case "24":
                    result = "LK";
                    break;
                case "26":
                    result = "CG";
                    break;
                case "27":
                    result = "JP";
                    break;
                default:
                    Console.WriteLine("Invalid city_id");
                    break;
            }
            return result;
        }


    }



}
