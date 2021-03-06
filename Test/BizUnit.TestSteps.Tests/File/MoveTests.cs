﻿
using System.IO;
using System;
using BizUnit.TestSteps.File;
using NUnit.Framework;

namespace BizUnit.TestSteps.Tests.File
{
    /// <summary>
    /// Summary description for FileCreateTests
    /// </summary>
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void MoveFileTest()
        {
            TestHelper.DeleteFile(@"..\..\TestData\PurchaseOrder001.testdel2xml");
            TestHelper.DeleteFile(@"..\..\TestData\PurchaseOrder001.testdel1xml");

            System.IO.File.Copy(@"..\..\TestData\PurchaseOrder001.xml",
                @"..\..\TestData\PurchaseOrder001.testdel1xml");

            var step = new MoveStep();
            step.SourcePath = @"..\..\TestData\PurchaseOrder001.testdel1xml";
            step.DestinationPath = @"..\..\TestData\PurchaseOrder001.testdel2xml";

            step.Execute(new Context());

            Assert.IsTrue(System.IO.File.Exists(@"..\..\TestData\PurchaseOrder001.testdel2xml"));
        }

        [Test]
        public void MoveFileTest_Negative()
        {
            TestHelper.DeleteFile(@"..\..\..\TestData\PurchaseOrder001.testdel2xml");
            TestHelper.DeleteFile(@"..\..\..\TestData\PurchaseOrder001.testdel1xml");

            System.IO.File.Copy(@"..\..\TestData\PurchaseOrder001.xml",
                @"..\..\TestData\PurchaseOrder001.testdel1xml");

            var step = new MoveStep();
            step.SourcePath = string.Empty;
            step.DestinationPath = @"..\..\..\TestData\PurchaseOrder001.testdel2xml";

            try
            {
                step.Validate(new Context());

            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("SourcePath is either null or of zero length"));
            }


            step.SourcePath = @"..\..\..\TestData\PurchaseOrder001.testdel1xml";
            step.DestinationPath = string.Empty;

            try
            {
                step.Validate(new Context());

            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("DestinationPath is either null or of zero length"));
            }


            step.SourcePath = @"..\..\..\TestData\PurchaseOrder001.testdel1xmlerror";
            step.DestinationPath = @"..\..\..\TestData\PurchaseOrder001.testdel1xmlerror";

            try
            {
                var context = new Context();
                step.Validate(context);
                step.Execute(context);

            }
            catch (FileNotFoundException fex)
            {
                Assert.IsTrue(fex.Message.Contains("Could not find file"));
            }
        }
    }
}
