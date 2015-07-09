using System;
using StructureMap.AutoMocking.Moq;

namespace Jama.JustTest.Xunit
{
    public abstract class BaseTestFixture<TSubjectUnderTest> where TSubjectUnderTest : class
    {
        protected MoqAutoMocker<TSubjectUnderTest> AutoMocker;

        protected TSubjectUnderTest SubjectUnderTest;
        protected Exception CaughtException;
        protected virtual void SetupDependencies() { }
        protected virtual void Given() { }
        protected virtual void When() { }
        protected virtual void Finally() { }

        protected BaseTestFixture()
        {
            Setup();
        }
        
        public void Setup()
        {
            BuildMocks();
            SetupDependencies();
            SubjectUnderTest = BuildSubjectUnderTest();

            Given();

            try
            {
                When();
            }
            catch (Exception exception)
            {
                CaughtException = exception;
            }
            finally
            {
                Finally();
            }
        }

        private void BuildMocks()
        {
            AutoMocker = new MoqAutoMocker<TSubjectUnderTest>();
        }

        private TSubjectUnderTest BuildSubjectUnderTest()
        {
            return AutoMocker.ClassUnderTest;
        }
    }
}