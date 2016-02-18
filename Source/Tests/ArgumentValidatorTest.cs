using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Util;

namespace PayPal.Testing
{
    [TestClass]
    public class ArgumentValidatorTest
    {
        [TestMethod, TestCategory("Unit")]
        public void EmptyStringMustThrow()
        {
            try
            {
                ArgumentValidator.Validate("", "EmptyString");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void NullStringMustThrow()
        {
            try
            {
                string str = null;
                ArgumentValidator.Validate(str, "NullString");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void BooleanMustDoesntThrow()
        {
            try
            {
                ArgumentValidator.Validate(false, "NullString");
            }
            catch (Exception ex)
            {
                Assert.IsFalse(ex is ArgumentNullException);
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void IntegerMustDoesntThrow()
        {
            try
            {
                ArgumentValidator.Validate(15, "NullString");
            }
            catch (Exception ex)
            {
                Assert.IsFalse(ex is ArgumentNullException);
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void ObjectMustDoesntThrow()
        {
            try
            {
                ArgumentValidator.Validate(new Object(), "NullString");
            }
            catch (Exception ex)
            {
                Assert.IsFalse(ex is ArgumentNullException);
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void NullObjectMustThrow()
        {
            try
            {
                ArgumentValidator.Validate(null, "NullString");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [TestMethod, TestCategory("Unit")]
        public void NullableBooleanMustThrow()
        {
            try
            {
                bool? nullableBool = null;
                ArgumentValidator.Validate(nullableBool, "NullString");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }
    }
}
