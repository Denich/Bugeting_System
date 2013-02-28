using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Budget.Services.Tests
{
    [TestFixture]
    public class XmlReportTest
    {
        private string _xmlDoc;
        [SetUp]
        public void SetUp()
        {
            _xmlDoc = "<DECLAR>" +
                                "<DECLARHEAD>" +
                                    "<TIN>87009943</TIN>" +
                                    "<PERIOD_MONTH>6</PERIOD_MONTH>" +
                                    "<PERIOD_TYPE>2</PERIOD_TYPE>" +
                                    "<PERIOD_YEAR>2011</PERIOD_YEAR>" +
                                "</DECLARHEAD>" +
                                "<DECLARBODY>" +
                                    "<HNAME>ТОВ 'Добро'</HNAME>" +
                                    "<R010G3>112922</R010G3>" +
                                    "<R002G3>128819</R002G3>" +
                                    "<R003G3>-15897</R003G3>" +
                                    "<R040G3>1091663</R040G3>" +
                                    "<R005G3>299583</R005G3>" +
                                    "<R051G3>299583</R051G3>" +
                                    "<R006G3>792080</R006G3>" +
                                    "<R061G3>471623</R061G3>" +
                                    "<R062G3>303637</R062G3>" +
                                    "<R063G3>20766</R063G3>" +
                                    "<R065G3>-3946</R065G3>" +
                                    "<HBOS>Ергешев Ш.З.</HBOS>" +
                                    "<HBUH>Тедремаа Т.Е.</HBUH>" +
                                "</DECLARBODY>" +
                            "</DECLAR>";
        }


    }
}
