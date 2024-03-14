﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerManagerAPI;
using System.ComponentModel;

namespace PowerManagement.ApiWrapper.Tests
{
    /// <remarks>
    /// These tests interact with the windows API, and as such they are integration tests rather than unit tests. 
    /// Running the tests will create, change, and delete power plans, and may in some cases (errors in DeletePlan,
    /// etc.) leave behind some artifacts. The <code>powercfg</code>command line tool tool is the simplest way to 
    /// list and delete plans. <code>powercfg /L</code> will list all existing plans. <code>powercfg /D {GUID}</code> 
    /// will delete it.
    /// </remarks>
    [TestClass]
    [TestCategory("API")] // This test category is used by the CI build to exclude tests that depend on the API
    public class PowerManagerTests
    {

        private Guid _balancedPlanGuid = new Guid("381b4222-f694-41f0-9685-ff5bb260df2e");
        private Guid _nonExistentPlanGuid1 = new Guid("59e6c766-9f90-40d9-a6bf-1637db4e1943");
        private Guid _nonExistentPlanGuid2 = new Guid("e9daf066-2cd9-4261-82b5-82d15fde51e3");

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void DuplicatePlan_Given_NonExistentSourcePlan_Throws_Win32Exception()
        {
            PowerManager.DuplicatePlan(_nonExistentPlanGuid1);
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void DuplicatePlan_Given_PreexistingTargetPlan_ThrowsWin32Exception()
        {
            PowerManager.DuplicatePlan(_balancedPlanGuid, _balancedPlanGuid);
        }

        [TestMethod]
        public void DuplicatePlan_Given_NoTargetId_Creates_NewPlan()
        {
            var res = PowerManager.DuplicatePlan(_balancedPlanGuid);
            try
            {
                Assert.IsNotNull(res);
                Assert.AreNotEqual(Guid.Empty, res);
            }
            finally
            {
                PowerManager.DeletePlan(res);
            }
        }

        [TestMethod]
        public void DuplicatePlan_Given_TargetId_Creates_NewPlan()
        {
            try
            {
                var res = PowerManager.DuplicatePlan(_balancedPlanGuid, _nonExistentPlanGuid1);
                Assert.AreEqual(_nonExistentPlanGuid1, res);
            }
            finally
            {
                PowerManager.DeletePlan(_nonExistentPlanGuid1);
            }
        }

        [TestMethod]
        public void GetActivePlan_Returns_Guid()
        {
            var res = PowerManager.GetActivePlan();

            Assert.IsNotNull(res);
            Assert.AreNotEqual(Guid.Empty, res);
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void SetActivePlan_Given_NonExitentPlan_Throws_Win32Exception()
        {
            PowerManager.SetActivePlan(_nonExistentPlanGuid1);
        }

        [TestMethod]
        public void SetActivePlan_Sets_ActivePlan()
        {
            var sourcePlan = PowerManager.GetActivePlan();
            Assert.IsNotNull(sourcePlan);
            Assert.AreNotEqual(Guid.Empty, sourcePlan);
            try
            {
                PowerManager.DuplicatePlan(_balancedPlanGuid, _nonExistentPlanGuid1);
                PowerManager.SetActivePlan(_nonExistentPlanGuid1);

                var active = PowerManager.GetActivePlan();
                Assert.AreEqual(_nonExistentPlanGuid1, active);
            }
            finally
            {
                PowerManager.SetActivePlan(sourcePlan);
                PowerManager.DeletePlan(_nonExistentPlanGuid1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void GetPlanName_Given_NonExistentPlanId_Throws_Win32Exception()
        {
            PowerManager.GetPlanName(_nonExistentPlanGuid1);
        }

        [TestMethod]
        public void GetPlanName_Given_BalancedPlanGuid_Returns_Balanced()
        {
            var res = PowerManager.GetPlanName(_balancedPlanGuid);
            Assert.AreEqual(res, "Balanced");
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void SetPlanName_Given_NonExitentPlanId_Throws_Win32Exception()
        {
            PowerManager.SetPlanName(_nonExistentPlanGuid1, "Name");
        }

        [TestMethod]
        public void SetPlanName_Sets_PlanName()
        {
            var planId = PowerManager.DuplicatePlan(_balancedPlanGuid, _nonExistentPlanGuid1);
            try
            {
                var expectedName = "Name";

                PowerManager.SetPlanName(planId, expectedName);
                var actualName = PowerManager.GetPlanName(planId);

                Assert.AreEqual(expectedName, actualName);
            }
            finally
            {
                PowerManager.DeletePlan(planId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void SetPlanDescription_Given_NonExitentPlanId_Throws_Win32Exception()
        {
            PowerManager.SetPlanDescription(_nonExistentPlanGuid1, "Description");
        }

        [TestMethod]
        public void SetPlanDescription_Sets_PlanDescription()
        {
            var planId = PowerManager.DuplicatePlan(_balancedPlanGuid, _nonExistentPlanGuid1);
            try
            {
                var expectedDescription = "Description";

                PowerManager.SetPlanDescription(planId, expectedDescription);
                var actualDescription = PowerManager.GetPlanDescription(planId);

                Assert.AreEqual(expectedDescription, actualDescription);
            }
            finally
            {
                PowerManager.DeletePlan(planId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void SetPlanSetting_Given_NonExitentPlan_Throws_Win32Exception()
        {
            PowerManager.SetPlanSetting(_nonExistentPlanGuid1, SettingSubgroup.BUTTONS_SUBGROUP, Setting.LIDACTION, PowerMode.AC, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void SetPlanSetting_Given_ConflictingGroupAndSetting_Throws_Win32Exception()
        {
            var planId = PowerManager.DuplicatePlan(_balancedPlanGuid, _nonExistentPlanGuid1);
            try
            {
                PowerManager.SetPlanSetting(planId, SettingSubgroup.BATTERY_SUBGROUP, Setting.LIDACTION, PowerMode.AC, 0);
            }
            finally
            {
                PowerManager.DeletePlan(planId);
            }
        }

        [TestMethod]
        public void SetPlanSetting_Sets_Setting()
        {
            var planId = PowerManager.DuplicatePlan(_balancedPlanGuid, _nonExistentPlanGuid1);
            try
            {
                var subgroup = SettingSubgroup.BUTTONS_SUBGROUP;
                var setting = Setting.PBUTTONACTION;


                uint expectedValue = 0;

                PowerManager.SetPlanSetting(planId, subgroup, setting, PowerMode.AC, expectedValue);

                var actualValue = PowerManager.GetPlanSetting(planId, subgroup, setting, PowerMode.AC);

                Assert.AreEqual(expectedValue, actualValue);
            }
            finally
            {
                PowerManager.DeletePlan(planId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPlanSetting_Given_AcAndDc_Throws_ArgumentException()
        {
            PowerManager.GetPlanSetting(_balancedPlanGuid, SettingSubgroup.BUTTONS_SUBGROUP, Setting.LIDACTION, PowerMode.AC | PowerMode.DC);
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void DeletePlan_Given_NonExistentPlanId_Throws_Win32Exception()
        {
            PowerManager.DeletePlan(_nonExistentPlanGuid1);
        }

        [TestMethod]
        public void DeletePlanIfExist_Given_ValidPlanId_Deletes_Plan()
        {
            var planId = PowerManager.DuplicatePlan(_balancedPlanGuid, _nonExistentPlanGuid1);
            PowerManager.DeletePlanIfExists(planId);
            var stillExists = PowerManager.PlanExists(planId);

            Assert.IsFalse(stillExists);
        }

        [TestMethod]
        public void DeletePlanIfExists_Given_NonExistentPlanId_Throws_Nothing()
        {
            PowerManager.DeletePlanIfExists(_nonExistentPlanGuid1);
            Assert.IsTrue(true); // Just checking that we didn't throw an error
        }

        [TestMethod]
        public void ListPlans_Returns_ListWithGuids()
        {
            var list = PowerManager.ListPlans();

            Assert.AreNotEqual(0, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(Guid));
        }

        [TestMethod]
        public void PlanExists_Given_BalancedPlanId_Returns_True()
        {
            var exists = PowerManager.PlanExists(_balancedPlanGuid);

            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void PlanExists_Given_NonExistentPlanId_Returns_False()
        {
            var exists = PowerManager.PlanExists(_nonExistentPlanGuid1);

            Assert.IsFalse(exists);
        }

    }
}
